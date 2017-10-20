using ExpenseTrackerWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerWebApplication.Interfaces
{
    public interface IDbClient
    {
        IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString);
        List<TransactionHistory> ViewTransactionRecords(IDbConnection database);
    }
}
