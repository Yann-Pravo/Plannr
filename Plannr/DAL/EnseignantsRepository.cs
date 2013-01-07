using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class EnseignantsRepository : IEnseignantsRepository
    {

        private PlannrContext context;

        public EnseignantsRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Enseignant Get(int id) {
                
    
            return this.context.Enseignants.Find(id);
        }

        public Enseignant GetEager(int id)
        {
            var e = context.Enseignants.Where(x => x.UserId == id).First();
            
            return e;
        }

        public void Insert(Models.Enseignant e)
        {
            this.context.Enseignants.Add(e);
        }

        public void Edit(Enseignant e)
        {
           this.context.Entry(e).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var e = this.context.Enseignants.Find(id);
            this.context.Enseignants.Remove(e);
        }

        public void Entry(Enseignant e)
        {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

        public IEnumerable<Enseignant> GetAll()
        {
            return this.context.Enseignants.ToList();
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

        public int Count()
        {
            int cpt = 0;
            cpt = context.Enseignants.Count() - 1;
            return cpt;
        }
        public IEnumerable<Enseignant> GetList()
        {
            return this.context.Enseignants.Where(x => x.UserName!="Admin").ToList();
        }

    }

}