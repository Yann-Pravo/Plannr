using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using System.Web.Security;
using Plannr.Filters;

namespace Plannr.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private PlannrContext db = new PlannrContext();
        public ActionResult Index()
        {




            if (Membership.GetUser() != null)
            {
                return RedirectToAction("Index", "Manager");
            }

            return View();
        }

      
    }
}
