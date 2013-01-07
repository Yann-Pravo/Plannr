using Microsoft.Web.WebPages.OAuth;
using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Plannr.Controllers
{
    public class OptionController : Controller
    {

        private PlannrContext db = new PlannrContext();
        private Plannr.DAL.IEnseignantsRepository enseignantRepository;

        public OptionController()
        {
            var context = new PlannrContext();
            enseignantRepository = new Plannr.DAL.EnseignantsRepository(context);
        }
        //
        // GET: /Option/

        


        public ActionResult ChangeData()
        {
            var ensId = WebSecurity.CurrentUserId;
            Enseignant e = enseignantRepository.Get(ensId);
            return View(e);
        }

        [HttpPost]
        public ActionResult ChangeData(Enseignant enseignant)
        {
            Enseignant e = enseignantRepository.Get(enseignant.UserId);
            e.FirstName = enseignant.FirstName;
            e.Name = enseignant.Name;
            e.Tel = enseignant.Tel;
            if (ModelState.IsValid)
            {
               this.enseignantRepository.Edit(e);

               this.enseignantRepository.Save();
                return RedirectToAction("ChangeData");
            }
            return View(enseignant);

        }


        public ActionResult ChangePwd()
        {

          
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Option");
            return View();
        }



    }
}
