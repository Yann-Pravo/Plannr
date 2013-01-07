using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class BatimentsRepository : IBatimentsRepository
    {

        private PlannrContext context;

        public BatimentsRepository(PlannrContext context)
        {
            this.context = context;
        }
        public IEnumerable<Batiment> GetList()
        {

            return this.context.Batiments.ToList();
        }
        public Batiment Get(int id)
        {
            return this.context.Batiments.Find(id);
        }

      /*  public Batiment Get(String name)
        {
            return this.context.Batiments.;
        }*/


        public void Insert(Models.Batiment e)
        {
            IEnumerable<Batiment> batiments = GetAll();
            IEnumerable<Batiment> test = batiments.Where(x => x.Nom == e.Nom);
            if (test.Count() == 0)
            {
                this.context.Batiments.Add(e);
            }
        }

        public void Delete(int id)
        {
            var e = this.context.Batiments.Find(id);
            this.context.Batiments.Remove(e);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public IEnumerable<Batiment> GetAll()
        {
            return this.context.Batiments.ToList();
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

        
         public void Entry(Batiment e)
         {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

         public int Count()
         {
             return context.Batiments.Count();
         }

    }

}