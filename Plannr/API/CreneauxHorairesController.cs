using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Plannr.Models;
using Plannr.DAL;
namespace Plannr.API
{
    public class CreneauxHorairesController : ApiController
    {
        private ICreneauxHorairesRepository repo;

        public CreneauxHorairesController()
        {
            var context = new PlannrContext();
            this.repo = new CreneauxHorairesRepository(context);
        }

        // GET api/CreneauxHoraires
        public IEnumerable<CreneauHoraire> GetCreneauHoraires()
        {
            return this.repo.getCreneauxHoraires();
        }

        public IEnumerable<CreneauHoraire> GetCreneauxHorairesFree(DateTime id)
        {
            
            return this.repo.getCreneauxHorairesForDate(id);

        }

       
    }
}