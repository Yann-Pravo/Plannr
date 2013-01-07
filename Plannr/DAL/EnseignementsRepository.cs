using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Plannr.DAL
{
    public class EnseignementsRepository : IEnseignementsRepository
    {

        private PlannrContext context;

        public EnseignementsRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Enseignement Get(int id) {
            return this.context.Enseignements.Find(id);
        }

        public IEnumerable<Models.Enseignement> GetEnseignementsForTeacher(int id)
        {
            // LINQ, cast to List<Enseignement> and BRA
            return this.context.Enseignements.Where(x => x.Enseignant.UserId == id).ToList();
        }

        public void Insert(Models.Enseignement e)
        {
            this.context.Enseignements.Add(e);
        }

        public void Delete(int id)
        {
            var e = this.context.Enseignements.Find(id);
            this.context.Enseignements.Remove(e);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        // DIspose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Enseignement> GetList()
        {
            return this.context.Enseignements.ToList();
        }

        
    }
    
}