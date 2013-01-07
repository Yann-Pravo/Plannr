using System;
using System.Collections.Generic;
using System.Linq;
using Plannr.Models;

namespace Plannr.DAL
{
    public class CoursRepository : ICoursRepository
    {
        private PlannrContext context;


        public CoursRepository(PlannrContext context)
        {
            this.context = context;
        }



        public Cours FindEager(int id)
        {
            return this.context.Cours.Include("ReservationAssociee").Single(s => s.Id == id);
        }

        public void Edit(Cours cours)
        {
            this.context.Entry(cours).State = System.Data.EntityState.Modified;
        }

        public void Insert(Cours cours)
        {
            this.context.Cours.Add(cours);
        }

        public void Delete(int id)
        {
            var e = this.context.Cours.Find(id);
            this.context.Cours.Remove(e);
        }

        public void Delete(Cours cours)
        {
            this.context.Cours.Remove(cours);
        }

        public Cours Find(int id)
        {
            return this.context.Cours.Find(id);
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
            cpt = context.Cours.Count();
            return cpt;
        }
        public IEnumerable<Cours> GetList()
        {
            return this.context.Cours.ToList();
        }
    }
}