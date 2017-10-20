using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTrackerWebApplication.Models
{
    public class TransactionHistory
    {
        public int PrimaryKey { get; set; } // was string before
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public double Cash { get; set; }
        public double Total { get; set; }
        public double Change { get; set; }
        public double Tax { get; set; }
        public string TransactionDate { get; set; }
        public string EstablishmentName { get; set; }
        //public string EstablishmentName { get; set; }
        public int? EstablishmentId { get; set; }
    }
}