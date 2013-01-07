using Newtonsoft.Json;
using Plannr.DAL;
using Plannr.Filters;
using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Plannr.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ManagerController : Controller
    {
        private IDemandesRepository demandesRepository;
        private IReservationsRepository reservationsRepository;
        private IEnseignantsRepository enseignantsRepository;

        public ManagerController()
        {
            var db = new PlannrContext();
            this.demandesRepository = new DemandesRepository(db);
            this.reservationsRepository = new ReservationsRepository(db);
            this.enseignantsRepository = new EnseignantsRepository(db);
        
        }
        //
        // GET: /Manager/
        
        public ActionResult Index()
        {
            var id = (int) Membership.GetUser().ProviderUserKey;
            ViewBag.unseen = this.demandesRepository.GetUnseenDemandes(id);

            Enseignant ens = this.enseignantsRepository.Get(id);
            ViewBag.login = ens.UserName;

            List<Reservation> resa = this.reservationsRepository.GetReservationsFor(id).ToList();

            // Static method
            ViewBag.calendarJSON = ReservationCalendar.ReservationsToJson(resa);

            ViewBag.calendarId = "CalendarManager";


            if (Roles.IsUserInRole(User.Identity.Name,"ResponsableUE"))
            {
                ViewBag.demandesAVerifCount = this.demandesRepository.GetReservationTo(id).Count();

                if (!Request.IsAjaxRequest())
                {
                    return View("Responsable");
                }
                else
                {
                    return PartialView("_Responsable");
                }
            }

           else if (Roles.IsUserInRole(User.Identity.Name, "Administrateur"))
            {
                if (!Request.IsAjaxRequest())
                {
                    return View("Administrateur");
                }
                else
                {
                    return PartialView("_Administrateur");
                }
            }
            else if (Roles.IsUserInRole(User.Identity.Name,"Enseignant"))
            {
                if (!Request.IsAjaxRequest())
                {
                    return View("Enseignant");
                }
                else
                {
                    return PartialView("_Enseignant");
                }
                
            }
            
            else
            {
                return View();
            }

        }
       

    }
}
