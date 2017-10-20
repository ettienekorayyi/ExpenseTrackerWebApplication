using ExpenseTrackerWebApplication.Common;
using ExpenseTrackerWebApplication.Interfaces;
using ExpenseTrackerWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExpenseTrackerWebApplication.Classes
{
    public class SqlServerDbClient : IDbClient
    {
        public IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString)
        {
            DbProviderFactory providerFactory =
                DbProviderFactories.GetFactory(connectionString.ProviderName);
            return providerFactory.CreateConnection();
        }

        public List<TransactionHistory> ViewTransactionRecords(IDbConnection database)
        {
            try
            {
                List<TransactionHistory> list = new List<TransactionHistory>();
                using (IDbCommand command = database.CreateCommand())
                {
                    database.Open();

                    command.CommandType = CommandType.StoredProcedure; ;
                    command.CommandText = Constants.ShowTransactionHistories;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(new TransactionHistory()
                            {
                                PrimaryKey = Int16.Parse(reader[0].ToString()),
                                ItemName = reader[1].ToString(),
                                Quantity = int.Parse(reader[2].ToString()),
                                
                                Amount = Double.Parse(reader[3].ToString()),
                                Total = Double.Parse(reader[4].ToString()),
                                Cash = Double.Parse(reader[5].ToString()),
                                Change = Double.Parse(reader[6].ToString()),
                                Tax = Double.Parse(reader[7].ToString()),
                                TransactionDate = DateTime.Parse(reader[8].ToString()).ToShortDateString(),
                                EstablishmentName = reader[9].ToString(),
                                EstablishmentId = Int16.Parse(reader[10].ToString())
                            });
                    }
                }
                return list;
            }
            catch (NullReferenceException nullException)
            {
                //MessageBox.Show(nullException.Message);
            }
            return null;
        }

        public TransactionHistory ViewTransactionRecord(IDbConnection database, int id)
        {
            try
            {
                TransactionHistory transactionHistory = null;
                using (SqlCommand command = new SqlCommand(Constants.UpdateTransactionRecord,
                    new SqlConnection(database.ConnectionString)))
                {
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure; ;
                    command.CommandText = Constants.ShowTransactionHistory;
                    command.Parameters.Add("@pk", SqlDbType.Int).Value = id;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactionHistory = new TransactionHistory()
                            {
                                PrimaryKey = Int16.Parse(reader[0].ToString()),
                                ItemName = reader[1].ToString(),
                                Quantity = int.Parse(reader[2].ToString()),

                                Amount = Double.Parse(reader[3].ToString()),
                                Total = Double.Parse(reader[4].ToString()),
                                Cash = Double.Parse(reader[5].ToString()),
                                Change = Double.Parse(reader[6].ToString()),
                                Tax = Double.Parse(reader[7].ToString()),
                                TransactionDate = DateTime.Parse(reader[8].ToString()).ToShortDateString(),
                                EstablishmentName = reader[9].ToString(),
                                EstablishmentId = Int16.Parse(reader[10].ToString())
                            };
                        }
                    }
                }
                return transactionHistory;
            }
            catch (NullReferenceException nullException)
            {
                //MessageBox.Show(nullException.Message);
            }
            return null;
        }

        public void UpdateDataFromTransaction(IDbConnection database, TransactionHistory transactionHistory)
        {
            using (SqlCommand command = new SqlCommand(Constants.UpdateTransactionRecord,
                new SqlConnection(database.ConnectionString)))
            {
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@pk", SqlDbType.NVarChar, 30).Value = transactionHistory.PrimaryKey;
                command.Parameters.Add("@item", SqlDbType.NVarChar, 30).Value = transactionHistory.ItemName;
                command.Parameters.Add("@qty", SqlDbType.Int).Value = transactionHistory.Quantity;
                command.Parameters.Add("@amount", SqlDbType.Decimal, 18).Value = transactionHistory.Amount;
                command.Parameters.Add("@total", SqlDbType.Decimal, 18).Value = transactionHistory.Total;
                command.Parameters.Add("@cash", SqlDbType.Decimal, 18).Value = transactionHistory.Cash;
                command.Parameters.Add("@change", SqlDbType.Decimal, 18).Value = transactionHistory.Change;
                command.Parameters.Add("@tax", SqlDbType.Decimal, 4).Value = transactionHistory.Tax;
                command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = transactionHistory.TransactionDate;
                command.Parameters.Add("@establishment_id", SqlDbType.Int).Value = transactionHistory.EstablishmentId;

                command.ExecuteNonQuery();

            }
        }
        //total,change
        public void InsertDataToTransaction(
            IDbConnection database, TransactionHistory transaction)
        {
            using (SqlCommand command = new SqlCommand(Constants.CreateTransactionRecord,
                new SqlConnection(database.ConnectionString)))
            {
                try
                {
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    // This implements parameterized query
                    command.Parameters.Add("@item", SqlDbType.NVarChar, 30).Value = transaction.ItemName;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = transaction.Quantity;
                    command.Parameters.Add("@amount", SqlDbType.Decimal, 18).Value = transaction.Amount;

                    command.Parameters.Add("@total", SqlDbType.Decimal, 18).Value = transaction.Total;

                    command.Parameters.Add("@cash", SqlDbType.Decimal, 18).Value = transaction.Cash;

                    command.Parameters.Add("@change", SqlDbType.Decimal, 18).Value = transaction.Change;

                    command.Parameters.Add("@tax", SqlDbType.Decimal, 4).Value = transaction.Tax;
                    command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = transaction.TransactionDate;
                    command.Parameters.Add("@establishment_id", SqlDbType.Int).Value = transaction.EstablishmentId;

                    command.ExecuteNonQuery();

                }
                catch (FormatException formatException)
                {
                    //MessageBox.Show(formatException.Message, "Record Creation Failed!");
                }
            }
        }

        public void DeleteDataFromTransaction(IDbConnection database, int primaryKey)
        {
            using (SqlCommand command = new SqlCommand(Constants.DeleteTransactionRecord,
               new SqlConnection(database.ConnectionString)))
            {
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@trans_id", SqlDbType.NVarChar, 30).Value = primaryKey;
                command.ExecuteNonQuery();
            }
        }

        // Establishments
        public List<Establishment> ViewEstablishments(IDbConnection database)
        {
            using (IDbCommand command = database.CreateCommand())
            {
                List<Establishment> list = new List<Establishment>();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Constants.ShowEstablishmentList;
                    database.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add
                            (
                                new Establishment(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString())
                            );
                        }
                        return list;
                    }
                }
                catch (SqlException sql)
                {
                    if (sql.Number == 2)
                    {
                        //MessageBox.Show("An error has occurred while establishing a connection to the server. \n" +
                        //            "Please check the SQL Service from Windows Services.");
                    }
                }
                return null;
            }
        }

        public void InsertDataToEstablishment(IDbConnection database, string paramOne, string paramTwo)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = new SqlConnection(database.ConnectionString);
                command.Connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Constants.CreateEstablishment;

                command.Parameters.Add("@establishment_name", SqlDbType.VarChar, 20).Value = paramOne;
                command.Parameters.Add("@establishment_description", SqlDbType.VarChar, 90).Value = paramTwo;

                command.ExecuteNonQuery();
            }
        }

    }
}