using ExpenseTrackerWebApplication.Classes;
using ExpenseTrackerWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace ExpenseTrackerWebApplication.Common
{
    public class Utility
    {
        public IDbConnection connection { get; private set; }

        public List<TransactionHistory> UseTransactionHistories(string dbType, string connectionString)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            return new SqlServerDbClient().ViewTransactionRecords(connection);
        }
        public TransactionHistory UseTransactionHistory(string dbType, string connectionString, int id)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            return new SqlServerDbClient().ViewTransactionRecord(connection,id);
        }

        public void TransactionHistoryUpdate(string dbType, string connectionString,
            TransactionHistory history)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            new SqlServerDbClient().UpdateDataFromTransaction(connection, history);
        }

        public void TransactionHistoryDelete(string dbType, string connectionString,
            int primaryKey)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            new SqlServerDbClient().DeleteDataFromTransaction(connection, primaryKey);
        }

        public void TransactionHistoryCreate(string dbType, string connectionString,
            TransactionHistory transaction)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            new SqlServerDbClient().InsertDataToTransaction(connection, transaction);
        }

        // Establishments
        public List<Establishment> UseEstablishment(string dbType, string connectionString)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            return new SqlServerDbClient().ViewEstablishments(connection);
        }

        public void CreateNewEstablishment(string dbType, string connectionString, string paramOne, string paramTwo)
        {
            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            new SqlServerDbClient().InsertDataToEstablishment(connection, paramOne, paramTwo);
        }

    }
}