using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationFinal.MVC.UI.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Unresolved()
        {
            return View();
        }
    }
}