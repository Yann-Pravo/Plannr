using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using Plannr.DAL;

namespace Plannr.Controllers
{
    public class CoursController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IMatieresRepository matiereRepository;
        private ICoursRepository coursRepository;

        public CoursController()
        {
            var context = new PlannrContext();
            matiereRepository = new MatieresRepository(context);
            coursRepository = new CoursRepository(context);
        }
        //
        // GET: /Cours/

        public ActionResult Index()
        {
          // ViewBag.count = this.coursRepository.Count();
            if (!Request.IsAjaxRequest())
            {
                return View(this.coursRepository.GetList());
            }
            else
            {

                return PartialView("_Index", this.coursRepository.GetList());
            } 
        }

        //
        // GET: /Cours/Details/5

        public ActionResult Details(int id = 0)
        {
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        //
        // GET: /Cours/Create

        public ActionResult Create()
        {
            ViewBag.ListeMatieres = this.matiereRepository.GetAll().ToList();
            ViewBag.ListeTypeCours = db.TypesCours.ToList();

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
        // POST: /Cours/Create

        [HttpPost]
        public ActionResult Create(Cours cours)
        {

        
           cours.Matiere = this.matiereRepository.Get(cours.Matiere.Id);
            cours.TypeCours = db.TypesCours.Find(cours.TypeCours.Id);


            
           foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                
                foreach (ModelError error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }
            
            if (ModelState.IsValid)
           {
                
                    this.coursRepository.Insert(cours);
                    this.coursRepository.Save();
                    return RedirectToAction("Index");
                
            }

            return View(cours);

        }

        //
        // GET: /Cours/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        //
        // POST: /Cours/Edit/5

        [HttpPost]
        public ActionResult Edit(Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cours);
        }

        //
        // GET: /Cours/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        //
        // POST: /Cours/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Cours.Find(id);
            db.Cours.Remove(cours);
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