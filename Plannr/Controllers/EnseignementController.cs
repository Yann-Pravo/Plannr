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

namespace Plannr.Controllers
{
    public class EnseignementController : Controller
    {
        
        private PlannrContext db = new PlannrContext();
        private IEnseignantsRepository enseignantRepository;
        private ICoursRepository coursRepository;
        private IEnseignementsRepository enseignementRepository;
        //private IGroupeRepository coursRepository;

        public EnseignementController()
        {
            var context = new PlannrContext();
            enseignantRepository = new EnseignantsRepository(context);
            coursRepository = new CoursRepository(context);
            enseignementRepository = new EnseignementsRepository(context);
        }
        //
        // GET: /Enseignement/

        public ActionResult Index()
           {

               var id = (int)Membership.GetUser().ProviderUserKey;
               if (!Request.IsAjaxRequest())
               {
                   return View(this.enseignementRepository.GetEnseignementsForTeacher(id));
               }
               else
               {

                   return PartialView("_Index", this.enseignementRepository.GetEnseignementsForTeacher(id));
               } 
        }

        //
        // GET: /Enseignement/Details/5

        public ActionResult Details(int id = 0)
        {
            Enseignement enseignement = db.Enseignements.Find(id);
            if (enseignement == null)
            {
                return HttpNotFound();
            }
            return View(enseignement);
        }

        //
        // GET: /Enseignement/Create

        public ActionResult Create()
        {
            ViewBag.ListeCours = this.coursRepository.GetList();
            ViewBag.ListeEnseignants = this.enseignantRepository.GetList();
            ViewBag.ListeGroupes = db.Groupes.ToList();

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
        // POST: /Enseignement/Create

        [HttpPost]
        public ActionResult Create(Enseignement enseignement)
        {
             enseignement.Enseignant = this.enseignantRepository.Get(enseignement.Enseignant.UserId);
            enseignement.Cours = this.coursRepository.Find(enseignement.Cours.Id);
           // enseignement.Cours = null;
            //enseignement.Groupe = db.Groupes.Find(enseignement.Groupe.Id);
            enseignement.Groupe.Libelle = "IG5";
            


            if (ModelState.IsValid)
            {
                this.enseignementRepository.Insert(enseignement);
                this.enseignementRepository.Save();
                return RedirectToAction("Index");
            }

            return View(enseignement);
        }

        //
        // GET: /Enseignement/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Enseignement enseignement = db.Enseignements.Find(id);
            if (enseignement == null)
            {
                return HttpNotFound();
            }
            return View(enseignement);
        }

        //
        // POST: /Enseignement/Edit/5

        [HttpPost]
        public ActionResult Edit(Enseignement enseignement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enseignement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enseignement);
        }

        //
        // GET: /Enseignement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Enseignement enseignement = db.Enseignements.Find(id);
            if (enseignement == null)
            {
                return HttpNotFound();
            }
            return View(enseignement);
        }

        //
        // POST: /Enseignement/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Enseignement enseignement = db.Enseignements.Find(id);
            db.Enseignements.Remove(enseignement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}