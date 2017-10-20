using ExpenseTrackerWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTrackerWebApplication.Classes
{
    public class DbSelectorFactory
    {
        public IDbClient CreateDbClasses(string DbType)
        {
            return new SqlServerDbClient();
        }
    }
}