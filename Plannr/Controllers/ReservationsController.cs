using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using Plannr.DAL;
using System.Web.Security;
using Plannr.Filters;

namespace Plannr.Controllers
{
    [Authorize(Roles="ResponsableUE")]
    [InitializeSimpleMembership]

    public class ReservationsController : Controller
    {
    
        private IReservationsRepository repository;
        private IDemandesRepository demandesRepository;
        private ISallesRepository sallesRepository;
        private ICreneauxHorairesRepository creneauxRepository;
        private IEnseignementsRepository ensRepository;
          // Constructor
        public ReservationsController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.repository = new ReservationsRepository(context);
            this.demandesRepository = new DemandesRepository(context);
            this.sallesRepository = new SallesRepository(context);
            this.creneauxRepository = new CreneauxHorairesRepository(context);
            this.ensRepository = new EnseignementsRepository(context);

        }

        // Give it as a parameter aswel for unit testing
        public ReservationsController(IReservationsRepository repo, IDemandesRepository demandesRepo, ISallesRepository sallesRepo, ICreneauxHorairesRepository creneauxRepo)
        {
            this.repository = repo;
            this.demandesRepository = demandesRepo;
            this.sallesRepository = sallesRepo;
            this.creneauxRepository = creneauxRepo;

        }


        // GET: /Reservations/

        public ActionResult Index() 
        {
            var id = (int)Membership.GetUser().ProviderUserKey;
            if (!Request.IsAjaxRequest())
            {
                return View(this.demandesRepository.GetReservationTo(id));
            }
            else
            {
                return PartialView("Index", this.demandesRepository.GetReservationTo(id));
            }
        }


        //
        // GET: /Reservations/Create/id_demande

        public ActionResult Create(int id)
        {
            var demandeAssociee = this.demandesRepository.Find(id);
           List<Salle> salles = this.sallesRepository.GetSallesCriteres(demandeAssociee.CapaciteNecessaire, demandeAssociee.BesoinProjecteur, demandeAssociee.DateVoulue).ToList();
           List<CreneauHoraire> creneaux = this.creneauxRepository.getCreneauxHorairesForDate(demandeAssociee.DateVoulue).ToList();



               

            // Print a nice calendar.
            List<Reservation> reservationsGroupe = this.repository.GetReservationsForGroupe(demandeAssociee.Enseignement.Groupe.Id).ToList();
            ViewBag.calendarId = "CalendarCreateReservation_" + id;
            ViewBag.calendarJSON = ReservationCalendar.ReservationsToJson(reservationsGroupe);



            ViewBag.demandeAssociee = demandeAssociee;
            ViewBag.creneaux = creneaux;
            ViewBag.salles = salles;
            ViewBag.DemandeId = id;
            return View(new Reservation() { Date = demandeAssociee.DateVoulue, Enseignement = new Enseignement() { Id = demandeAssociee.Enseignement.Id } });
        }

        //
        // POST: /Reservations/Create

        [HttpPost]
        public ActionResult Create(Reservation reservation, int DemandeId)
        {

            var origEnsId = reservation.Enseignement.Id;

            reservation.Creneau = this.creneauxRepository.Find(reservation.Creneau.Id);
            reservation.Salle = this.sallesRepository.Get(reservation.Salle.Id);

            System.Diagnostics.Debug.WriteLine(origEnsId);
            reservation.Enseignement = this.ensRepository.Get(origEnsId);

            


            if (ModelState.IsValid)
            {
                this.repository.Insert(reservation);
                this.repository.Save();

                // Edit associated dmeande
             
                var d = this.demandesRepository.FindEager(DemandeId);
                d.ReservationAssociee = reservation;
                this.demandesRepository.Edit(d);
                this.demandesRepository.Save();
                

                return RedirectToAction("Index");
            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError modelError in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(modelError.ErrorMessage);
                }
            }

            var demandeAssociee = this.demandesRepository.Find(1);
            List<Salle> salles = this.sallesRepository.GetSallesCriteres(demandeAssociee.CapaciteNecessaire, demandeAssociee.BesoinProjecteur, demandeAssociee.DateVoulue).ToList();
            List<CreneauHoraire> creneaux = this.creneauxRepository.getCreneauxHorairesForDate(demandeAssociee.DateVoulue).ToList();


            ViewBag.demandeAssociee = demandeAssociee;
            ViewBag.salles = salles;
            ViewBag.creneaux = creneaux;

            return View(reservation);
        }

        //
        // GET: /Reservations/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reservation reservation = this.repository.Get(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        //
        // POST: /Reservations/Edit/5

        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(reservation).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        //
        // POST: /Reservations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = this.repository.Get(id);
            //db.Reservations.Remove(reservation);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.repository.Dispose();
            base.Dispose(disposing);
        }
    }
}