using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using WebMatrix.WebData;
using System.Web.Security;
using Plannr.DAL;
using Plannr.Filters;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Plannr.Controllers
{
    [Authorize(Roles = "Enseignant")]
    [InitializeSimpleMembership]
    public class BookController : Controller
    {   
        // Repository as private member, generic interface type
        private IDemandesRepository repository;
        private IEnseignementsRepository enseignementsRepository;
        private ICreneauxHorairesRepository creneauxHorairesRepository;

        // Constructor
        public BookController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.repository = new DemandesRepository(context);
            this.enseignementsRepository = new EnseignementsRepository(context);
            this.creneauxHorairesRepository = new CreneauxHorairesRepository(context);

        }

        // Give it as a parameter aswel for unit testing
        public BookController(IDemandesRepository repo, IEnseignementsRepository ensRepo, ICreneauxHorairesRepository crRepo)
        {
            this.repository = repo;
            this.enseignementsRepository = ensRepo;
            this.creneauxHorairesRepository = crRepo;
        }


        // GET: /Book/

      
        public ActionResult Index() {

            var id = (int) Membership.GetUser().ProviderUserKey;
            IEnumerable<DemandeReservation> demandes = this.repository.GetReservationsBy(id).ToList();


            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ViewBag.JSONModel = JsonConvert.SerializeObject(demandes, Formatting.None, jsSettings);


            // Render full or partial.
            if (!Request.IsAjaxRequest())
            {
                return View(demandes);
            }
            else
            {
                return PartialView("_Index", demandes);
            }
        }


        //
        // GET: /Book/Create


        public ActionResult Create()
        {
            // Get Session from Current Logged User
            var id = (int)Membership.GetUser().ProviderUserKey;
            // On va chercher les enseignements qui sont attribués à l'enseignant pour les lister dans la DropList en sélectionner un, on le passe à la vue pour génrer la ListBox
            ViewBag.listEnseignements = this.enseignementsRepository.GetEnseignementsForTeacher(id);
            ViewBag.listCreneauxHoraires = this.creneauxHorairesRepository.getCreneauxHoraires();

            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                return PartialView("_Create");
            }
        }

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(DemandeReservation demandereservation) {
            
            demandereservation.DateDemande = DateTime.Now;
            demandereservation.Checked = false;
            demandereservation.CheckedByTeacher = false;
            // Mapping
            demandereservation.Enseignement = this.enseignementsRepository.Get(demandereservation.Enseignement.Id);
            demandereservation.CreneauSouhaite = this.creneauxHorairesRepository.Find(demandereservation.CreneauSouhaite.Id);
            

            if (ModelState.IsValid)
            {
                repository.Insert(demandereservation);
                repository.Save();

                return RedirectToAction("Index");
            }

            //ModelState.Values.Where(modelState => modelState.Errors.Count > 0).ToList().ForEach(x => x.Errors.ToList().ForEach(y => System.Diagnostics.Debug.WriteLine(y.ErrorMessage)));


            // Since it's returing view, we need the listEnseignements in the ViewBag aswell
            var id = (int)Membership.GetUser().ProviderUserKey;
            ViewBag.listEnseignements = this.enseignementsRepository.GetEnseignementsForTeacher(id);
            ViewBag.listCreneauxHoraires = this.creneauxHorairesRepository.getCreneauxHoraires();

 

            return View(demandereservation);
        }

  
        //
        // GET: /Book/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DemandeReservation demandereservation = this.repository.Find(id);
            if (demandereservation == null)
            {
                return HttpNotFound();
            }
            return View(demandereservation);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var demande = this.repository.Find(id);

            if (demande.ReservationAssociee != null)
            {
                // On ne supprime pas une demande qui a déjà été validée.
                return new HttpStatusCodeResult(403);
            }

            // On vérifie que la demande existe.
            if (demande == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var SessionId = (int)Membership.GetUser().ProviderUserKey;
            if (demande.Enseignement.Enseignant.UserId != SessionId)
            {
                return new HttpStatusCodeResult(403);
            }

            this.repository.Delete(id);
            this.repository.Save();
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(200);
            }
        }

    }
}