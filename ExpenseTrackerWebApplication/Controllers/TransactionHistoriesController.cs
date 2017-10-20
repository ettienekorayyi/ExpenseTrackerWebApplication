using ExpenseTrackerWebApplication.Common;
using ExpenseTrackerWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackerWebApplication.Controllers
{
    public class TransactionHistoriesController : Controller
    {
        private static Nullable<int> establishmentId { get; set; }
        
        public ActionResult Index()
        {
            return View(new Utility().UseTransactionHistories(Constants.SqlServerClient.ToString(),Constants.SqlServerConnection));
        }

        public ActionResult Create(TransactionHistory history = null)
        {

            if (history.ItemName != null)
            {
                new Utility().TransactionHistoryCreate(Constants.SqlServerClient.ToString(), Constants.SqlServerConnection, history);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        { 
            return View(new Utility().UseTransactionHistory(Constants.SqlServerClient.ToString(),Constants.SqlServerConnection,id));
        }

        public ActionResult Edit(int id, TransactionHistory history = null)
        {
            if (history.ItemName != null)
            {
                history = new Utility().UseTransactionHistory(Constants.SqlServerClient.ToString(),Constants.SqlServerConnection, id);
                establishmentId = history.EstablishmentId;
                return View(history);
            }
            history.EstablishmentId = establishmentId;
            new Utility().TransactionHistoryUpdate(Constants.SqlServerClient.ToString(),Constants.SqlServerConnection, history);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            new Utility().TransactionHistoryDelete(Constants.SqlServerClient.ToString(),Constants.SqlServerConnection, id);
            return RedirectToAction("Index");
        }
    }
}