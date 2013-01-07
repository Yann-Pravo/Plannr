using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using System.Web.Security;
using Plannr.DAL;
using Newtonsoft.Json;
using Plannr.Filters;
using WebMatrix.WebData;
using Microsoft.Web.WebPages.OAuth;

namespace Plannr.Controllers
{
    [Authorize(Roles = "Administrateur")]
    [InitializeSimpleMembership]
    public class AdministrationController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IEnseignantsRepository enseignantRepository;
        private ISallesRepository salleRepository;
        private IBatimentsRepository batimentRepository;
        private IMatieresRepository matiereRepository;
        private IUeRepository ueRepository;
        private IResponsableUe respRepository;

        //
        // GET: /Administration/

        public AdministrationController() {
            var context = new PlannrContext();
            enseignantRepository = new EnseignantsRepository(context);
            salleRepository = new SallesRepository(context);
            batimentRepository = new BatimentsRepository(context);
            matiereRepository = new MatieresRepository(context);
            ueRepository = new UeRepository(context);
            respRepository = new ResponsableUERepository(context);
        }

        public AdministrationController(IEnseignantsRepository repo, IBatimentsRepository batrepo, ISallesRepository salrepo, IMatieresRepository matrepo, IUeRepository uerepo, IResponsableUe resprepo)
        {
            this.enseignantRepository = repo;
            this.batimentRepository = batrepo;
            this.salleRepository = salrepo;
            this.matiereRepository = matrepo;
            this.ueRepository = uerepo;
            this.respRepository = resprepo;
        }

        //Administration's Index
        public ActionResult Index()
        {
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                
                return PartialView("_Index");
            }
               // return View();
            
        }

        //- - - - - - - - - - - - - - - - Enseignant - - - - - - - - - - - - - - - - - -

        //Enseignant's Index
        public ActionResult IndexEnseignant()
        {
            ViewBag.count = this.enseignantRepository.Count();
           

            if (!Request.IsAjaxRequest())
            {
                return View(this.enseignantRepository.GetList());
            }
            else
            {

                return PartialView("_IndexEnseignant", this.enseignantRepository.GetList());
            }
            
        }

        // GET: Administration/CreateEnseignant
        public ActionResult CreateEnseignant()
        {

            if (!Request.IsAjaxRequest())
            {
                
                return View();


            }
            else
            {
                System.Diagnostics.Debug.WriteLine("test2");
                return PartialView("_CreateEnseignant");
            }
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult CreateEnseignant(Enseignant enseignant)
        {
            
            if (ModelState.IsValid)
            {

                this.enseignantRepository.Insert(enseignant);
                this.enseignantRepository.Save();
                Roles.AddUserToRole(enseignant.UserName, "Enseignant");
                WebSecurity.CreateAccount(enseignant.UserName, enseignant.UserName);

                
                return RedirectToAction("IndexEnseignant");
            }

            return View(enseignant);

        }

        // GET: /AddEnseignant/Edit

        public ActionResult EditEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            System.Diagnostics.Debug.WriteLine("LALALA" + enseignant.UserId);

            if (enseignant == null)
            {System.Diagnostics.Debug.WriteLine("testedit0");
                return HttpNotFound();
            }
            else if (!Request.IsAjaxRequest())
            {
                System.Diagnostics.Debug.WriteLine("testedit1");
                return View(enseignant);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("testedit");
                return PartialView("_EditEnseignant", enseignant);
            }
            
        }

        //
        // POST: /AddEnseignant/Edit

        [HttpPost]
        public ActionResult EditEnseignant(Enseignant enseignant)
        {

            System.Diagnostics.Debug.WriteLine("LALALA"+enseignant.UserId);
           // Enseignant e = this.enseignantRepository.GetEager(enseignant.UserId);
            //e.UserName = "salut";
            if (ModelState.IsValid)
            {
                this.enseignantRepository.Entry(enseignant);

                this.enseignantRepository.Save();
                return RedirectToAction("IndexEnseignant");
            }
            return View(enseignant);

        }
        
        

        // GET: /AddEnseignant/Delete

        public ActionResult DeleteEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            else
            {
               // Membership.DeleteUser(enseignant.UserName);
               // Roles.RemoveUserFromRole(enseignant.UserName, "Enseignant");
               
                this.enseignantRepository.Delete(id);
                
                this.enseignantRepository.Save();
                return RedirectToAction("IndexEnseignant");
            }
           /* if (!Request.IsAjaxRequest())
            {
                return View(enseignant);
            }
            else
            {

                return PartialView("_DeleteEnseignant", enseignant);
            }*/

        }

        //
        // POST: /AddEnseignant/Delete

        [HttpPost, ActionName("DeleteEnseignant")]
        public ActionResult DeleteConfirmed(int id)
        {
            Enseignant ens = this.enseignantRepository.Get(id);
            this.enseignantRepository.Delete(id);
            
            this.enseignantRepository.Save();
            return RedirectToAction("IndexEnseignant");
        }

        // GET: /Administration/Details

        public ActionResult DetailsEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(enseignant);
            }
            else
            {

                return PartialView("_DetailsEnseignant", enseignant);
            }

        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -



        //- - - - - - - - - - - - - - - - - Batiment - - - - - - - - - - - - - - - 
        //Batiment/Index
        public ActionResult IndexBatiment()
        {
            ViewBag.count = this.batimentRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.batimentRepository.GetAll());
            }
            else
            {
                return PartialView("_IndexBatiment", this.batimentRepository.GetAll());
            } 

        }


        // GET: /Administration/Create

        public ActionResult CreateBatiment(){
  
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateBatiment");
            } 

            }


        [HttpPost]
        public ActionResult CreateBatiment(Batiment batiment)
        {
            if (ModelState.IsValid)
            {
               
                    this.batimentRepository.Insert(batiment);
                    this.batimentRepository.Save();
                    return RedirectToAction("IndexBatiment");
                
            }

            return View(batiment);

        }

        // GET: /Salle/Details/5

        public ActionResult DetailsBatiment(int id = 0)
        {
          
            Batiment batiment = batimentRepository.Get(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(batiment);
            }
            else
            {

                return PartialView("_DetailsBatiment", batiment);
            }

        }

        //Get
        public ActionResult EditBatiment(int id = 0)
        {
   
            Batiment batiment = batimentRepository.Get(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(batiment);
            }
            else
            {

                return PartialView("_EditBatiment", batiment);
            }

        }

        [HttpPost]
        public ActionResult EditBatiment(Batiment batiment)
        {
            if (ModelState.IsValid)
            {

                batimentRepository.Entry(batiment);
                batimentRepository.Save();
                return RedirectToAction("IndexBatiment");
            }
            return View(batiment);
        }

        // GET: /Batiment/Delete/

        public ActionResult DeleteBatiment(int id = 0)
        {
   
            Batiment batiment = batimentRepository.Get(id);
            if (batiment == null)
            {
                return HttpNotFound();
            } 
            else
            {
                this.batimentRepository.Delete(id);
                this.batimentRepository.Save();

                return RedirectToAction("IndexBatiment");
            }
           /* if (!Request.IsAjaxRequest())
            {
                return View(batiment);
            }
            else
            {

                return PartialView("_DeleteBatiment", batiment);
            }*/

        }

        //
         //POST: /Batiment/Delete/

        /*[HttpPost, ActionName("DeleteBatiment")]
        public ActionResult DeleteConfirmed(int id)
        {

            batimentRepository.Delete(id);
            batimentRepository.Save();
            return RedirectToAction("Index");
        }*/


        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // - - - - - - - - - - - - - - - - Salle - - - - - - - - - - - - - - - - 
        //Batiment/Index
        public ActionResult IndexSalle()
        {
            ViewBag.count = this.salleRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.salleRepository.GetList());
            }
            else
            {
                return PartialView("_IndexSalle", this.salleRepository.GetList());
            }

        }

        //Get CreateSalle
       
        public ActionResult CreateSalle()
        {
            IEnumerable<Batiment> batList = this.batimentRepository.GetList();
            ViewBag.batsList = batList;
          
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateSalle");
            }

        }

        
        [HttpPost]
        public ActionResult CreateSalle(Salle salle)
        {

            System.Diagnostics.Debug.WriteLine("test");
            salle.Batiment = this.batimentRepository.Get(salle.Batiment.Id);
            //System.Diagnostics.Debug.WriteLine("test : %d",salle.Batiment.Id);
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                if (salle.Capacite <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("La capacit&eacute; de la salle est incorrecte.");
                }
                else
                {
                    this.salleRepository.Insert(salle);
                    this.salleRepository.Save();
                    return RedirectToAction("IndexSalle");
                }
            }

            return View(salle);
        }

        // GET: /Salle/Edit/

        public ActionResult EditSalle(int id = 0)
        {
            Salle salle = this.salleRepository.Get(id);
            IEnumerable<Batiment> batList = this.batimentRepository.GetList();
            ViewBag.batsList = batList;
            if (salle == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(salle);
            }
            else
            {

                return PartialView("_EditSalle",salle);
            }
            
        }

        //
        // POST: /Salle/Edit

        [HttpPost]
        public ActionResult EditSalle(Salle salle)
        {
            
            Salle s = this.salleRepository.GetEager(salle.Id);
            s.Batiment = this.batimentRepository.Get(salle.Batiment.Id);
            s.Libelle = salle.Libelle;
            s.Capacite = salle.Capacite;
            s.APrises = salle.APrises;
            s.AProjecteur = salle.AProjecteur;

            if (ModelState.IsValid)
            {

                this.salleRepository.Entry(s);
                this.salleRepository.Save();

                return RedirectToAction("IndexSalle");
            }
            return View(s);
        }

        // GET: /Salle/Delete

        public ActionResult DeleteSalle(int id = 0)
        {
            Salle salle = this.salleRepository.Get(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.salleRepository.Delete(id);
                this.salleRepository.Save();

                return RedirectToAction("IndexSalle");
            }
            /*if (!Request.IsAjaxRequest())
            {
                return View(salle);
            }
            else
            {

                return PartialView("_DeleteSalle", salle);
            }*/
            
        }

        public ActionResult DetailsSalle(int id = 0)
        {

            Salle salle = salleRepository.Get(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(salle);
            }
            else
            {

                return PartialView("_DetailsSalle", salle);
            }

        }

        
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //--------------------------------------------------------------
        //--------------------------Matière-------------------------------

        //Enseignant's Index
        public ActionResult IndexMatiere()
        {
            ViewBag.count = this.matiereRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.matiereRepository.GetAll());
            }
            else
            {

                return PartialView("_IndexMatiere", this.matiereRepository.GetAll());
            } 
            
        }


        // GET: Administration/CreateMatiere
        public ActionResult CreateMatiere()
        {

            IEnumerable<Ue> ueList = this.ueRepository.GetList();
            ViewBag.uesList = ueList;
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateMatiere");
            }

           

        }



        
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult CreateMatiere(Matiere matiere)
        {

            matiere.Ue = this.ueRepository.Get(matiere.Ue.Id);
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                this.matiereRepository.Insert(matiere);
                this.matiereRepository.Save();
                return RedirectToAction("IndexMatiere");
            }

            return View(matiere);

        }

        // GET: /AddMatiere/Edit

        public ActionResult EditMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            IEnumerable<Ue> ueList = this.ueRepository.GetList();
            ViewBag.uesList = ueList;
            if (matiere == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(matiere);
            }
            else
            {

                return PartialView("_EditMatiere",matiere);
            } 

        }

        //
        // POST: /AddMatiere/Edit

        [HttpPost]
        public ActionResult EditMatiere(Matiere matiere)
        {
            System.Diagnostics.Debug.WriteLine("LOLO" + matiere.Id);
            Matiere m = this.matiereRepository.GetEager(matiere.Id);
            m.Ue = this.ueRepository.Get(matiere.Ue.Id);
            m.Libelle = matiere.Libelle;
          
            if (ModelState.IsValid)
            {
                this.matiereRepository.Edit(m);       
                this.matiereRepository.Save();
                return RedirectToAction("IndexMatiere");
            }

            return View(m);
        }




        // GET: /AddMatiere/Delete
        public ActionResult DeleteMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.matiereRepository.Delete(id);
                this.matiereRepository.Save();

                return RedirectToAction("IndexMatiere");
            }
        }

        // GET: /Administration/Details

        public ActionResult DetailsMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(matiere);
            }
            else
            {

                return PartialView("_DetailsMatiere", matiere);
            } 

        }

        //--------------------------Responsable-------------------------------

        //Responsables Index
        public ActionResult IndexResponsable()
        {
            ViewBag.count = this.ueRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                
                return View(this.ueRepository.GetList());
            }
            else
            {
                
                return PartialView("_IndexResponsable", this.ueRepository.GetList());
                
            }

        }

        // GET: /AddMatiere/Edit

       public ActionResult EditResponsable(int id = 0)
        {
            Ue ue = this.ueRepository.Get(id);
            if (ue == null)
            {
                return HttpNotFound();
            }
         /* if (ue.ResponsableUe.UserId != 0 && Roles.IsUserInRole(ue.ResponsableUe.UserName, "ResponsableUE"))
            {
                Roles.RemoveUserFromRole(ue.ResponsableUe.UserName, "ResponsableUE");
                Roles.AddUserToRole(ue.ResponsableUe.UserName, "Enseignant");
            }*/
            /*if (ue.ResponsableUe != null)
            {
                Enseignant ens = this.respRepository.GetResp(ue.ResponsableUe.UserId);
            }
            */
            //Roles.RemoveUserFromRole(ens.UserName, "ResponsableUE");
           // Roles.AddUserToRole(ens.UserName, "Enseignant");
           
            IEnumerable<Enseignant> ensList = this.enseignantRepository.GetList();
            ViewBag.enseignantList = ensList;
            
            if (!Request.IsAjaxRequest())
            {
                return View(ue);
            }
            else
            {

                return PartialView("_EditResponsable", ue);
            }

        }

        //
        // POST: /AddMatiere/Edit

        [HttpPost]
        public ActionResult EditResponsable(Ue ue)
        {
            string name = null;

            Ue m = this.ueRepository.Get(ue.Id);
           
            int id = ue.ResponsableUe.UserId;
            
            Enseignant ens = this.enseignantRepository.Get(id);
            
           
           ResponsableUE resp = new ResponsableUE();
            resp.UserName = ens.UserName;
            resp.Name = ens.Name;
            resp.FirstName = ens.FirstName;
            resp.Tel = ens.Tel;
            resp.ResponsableDepuis = DateTime.Parse("10/01/2009");
            resp.Enseignements = ens.Enseignements;
            m.ResponsableUe = resp;


            //ens.UserName = ens.UserName + "_";
           // this.enseignantRepository.Save();
            
           // this.respRepository.Insert(resp);
            
            
          /*  Roles.AddUserToRole(resp.UserName, "ResponsableUE");
            WebSecurity.CreateAccount(resp.UserName, resp.UserName);*/
            
            
           // WebSecurity.CreateAccount(resp.UserName, resp.UserName);

            /*if (m.ResponsableUe != null  )
            {
                System.Diagnostics.Debug.WriteLine("T1");
                name = m.ResponsableUe.UserName;
                m.ResponsableUe = this.respRepository.GetEns(ue.ResponsableUe.UserId);
               /* Roles.AddUserToRole(ue.ResponsableUe.UserName, "ResponsableUE");
                System.Diagnostics.Debug.WriteLine("m.ResponsableUe.UserName" + m.ResponsableUe.UserName);
                Roles.RemoveUserFromRole(ue.ResponsableUe.UserName, "Enseignant");
               
                Roles.AddUserToRole(m.ResponsableUe.UserName, "Enseignant");
                Roles.RemoveUserFromRole(m.ResponsableUe.UserName, "ResponsableUE");
                System.Diagnostics.Debug.WriteLine("ue.ResponsableUe.UserName" + ue.ResponsableUe.UserName);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("T2");
                m.ResponsableUe = this.respRepository.GetEns(ue.ResponsableUe.UserId);
                name = m.ResponsableUe.UserName;
            }*/

           
            
            if (ModelState.IsValid)
            {
                
                this.ueRepository.Edit(m);
                this.ueRepository.Save();
                return RedirectToAction("IndexResponsable");
            }

            return View(ue);
        }

        // GET: /Responsable/Details/5

        public ActionResult DetailsResponsable(int id = 0)
        {

            Ue ue = ueRepository.Get(id);
            if (ue == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(ue);
            }
            else
            {

                return PartialView("_DetailsResponsable", ue);
            }

        }


        protected override void Dispose(bool disposing)
        {
            this.enseignantRepository.Dispose();
            this.batimentRepository.Dispose();
            this.salleRepository.Dispose();
            this.matiereRepository.Dispose();
            this.respRepository.Dispose();
            this.ueRepository.Dispose();
            base.Dispose(disposing);
        }


        //--------------------------------UE---------------------------------------------------------------


        //UE/Index
        public ActionResult IndexUE()
        {
            ViewBag.count = this.ueRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.ueRepository.GetList());
            }
            else
            {
                return PartialView("_IndexUE", this.ueRepository.GetList());
            }

        }


        // GET: /Administration/Create

        public ActionResult CreateUE()
        {

            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateUE");
            }

        }


        [HttpPost]
        public ActionResult CreateUE(Ue ue)
        {
   
            if (ModelState.IsValid)
            {
   
                this.ueRepository.Insert(ue);
                this.ueRepository.Save();
         
                return RedirectToAction("IndexUE");
            }

            return View(ue);
           
        }

        // GET: /Salle/Details/5

        public ActionResult DetailsUE(int id = 0)
        {

            Ue ue = ueRepository.Get(id);
            if (ue == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(ue);
            }
            else
            {

                return PartialView("_DetailsUE", ue);
            }

        }

        //Get
        public ActionResult EditUE(int id = 0)
        {

            Ue ue = ueRepository.Get(id);
            if (ue == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(ue);
            }
            else
            {

                return PartialView("_EditUE", ue);
            }

        }

        [HttpPost]
        public ActionResult EditUE(Ue ue)
        {
            if (ModelState.IsValid)
            {

                ueRepository.Entry(ue);
                ueRepository.Save();
                return RedirectToAction("IndexUE");
            }
            return View(ue);
        }

        // GET: /Batiment/Delete/

        public ActionResult DeleteUE(int id = 0)
        {

            Ue ue = ueRepository.Get(id);
            if (ue == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.ueRepository.Delete(id);
                this.ueRepository.Save();

                return RedirectToAction("IndexUE");
            }
            /* if (!Request.IsAjaxRequest())
             {
                 return View(batiment);
             }
             else
             {

                 return PartialView("_DeleteBatiment", batiment);
             }*/

        }
    }

}