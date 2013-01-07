using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class AdministrateurRepository : IAdministrateurRepository
    {

         private PlannrContext context;

        public AdministrateurRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Administrateur Get(int id) {
                
    
            return this.context.Administrateurs.Find(id);
        }


        public void Insert(Models.Administrateur e)
        {
            this.context.Administrateurs.Add(e);
        }

        public void Edit(Administrateur e)
        {
           this.context.Entry(e).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var e = this.context.Administrateurs.Find(id);
            this.context.Administrateurs.Remove(e);
        }

        public void Entry(Administrateur e)
        {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

        public IEnumerable<Administrateur> GetAll()
        {
            return this.context.Administrateurs.ToList();
        }
        public IEnumerable<Administrateur> GetList()
        {

            return this.context.Administrateurs.ToList();
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
            return context.Administrateurs.Count();
        }
    
    }
}