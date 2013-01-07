using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;

namespace Plannr.Controllers
{
    public class AddController : Controller
    {
        private PlannrContext db = new PlannrContext();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.DemandesReservation.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            if (demandereservation == null)
            {
                return HttpNotFound();
            }
            return View(demandereservation);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(DemandeReservation demandereservation)
        {
            if (ModelState.IsValid)
            {
                db.DemandesReservation.Add(demandereservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(demandereservation);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            if (demandereservation == null)
            {
                return HttpNotFound();
            }
            return View(demandereservation);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(DemandeReservation demandereservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demandereservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(demandereservation);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            if (demandereservation == null)
            {
                return HttpNotFound();
            }
            return View(demandereservation);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            db.DemandesReservation.Remove(demandereservation);
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