using ExpenseTrackerWebApplication.Common;
using ExpenseTrackerWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackerWebApplication.Controllers
{
    public class EstablishmentsController : Controller
    {
        // GET: Establishments
        public ActionResult Index()
        {
            return View(new Utility()
                .UseEstablishment(Constants.SqlServerClient.ToString(),
                 Constants.SqlServerConnection));
        }

        public ActionResult Create(Establishment establishment = null)
        {

            if (establishment.EstablishmentName != null)
            {
                new Utility().CreateNewEstablishment(Constants.SqlServerClient.ToString(),
                     Constants.SqlServerConnection, establishment.EstablishmentName,establishment.Description);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}