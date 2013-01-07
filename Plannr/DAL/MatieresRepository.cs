using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace Plannr.DAL
{
    public class MatieresRepository : IMatieresRepository
    {

        private PlannrContext context;

        public MatieresRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Matiere Get(int id)
        {

            return this.context.Matieres.Find(id);
        }

        public Matiere GetEager(int id)
        {
            var a = this.context.Matieres.Include("Ue").Single(s => s.Id == id);
            //var m = db.Matieres.Include(p => p.Ue).Single(s => s.Id == matiere.Id);
            
            return a;
        }


        public void Insert(Models.Matiere m)
        {
            IEnumerable<Matiere> matieres = GetAll();
            IEnumerable<Matiere> test = matieres.Where(x => x.Libelle == m.Libelle && x.Ue == m.Ue);
            if (test.Count() == 0)
            {
                this.context.Matieres.Add(m);
            }
        }

        public void Edit(Matiere m)
        {
           //this.context.Matieres.Attach(m);
            //var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            //var u = m.Ue;
           // m.Ue = null;
            this.context.Entry(m).State = EntityState.Modified;

          //  objectContext.ObjectStateManager.ChangeObjectState(m, EntityState.Modified);
           
          //  m.Ue = u;
        //    this.context.SaveChanges();
         /*  this.context.Entry(m).Reference(p => p.Ue).Load();
           var u = m.Ue;
           //this.context.Entry(m).State = EntityState.Modified;
           this.context.SaveChanges();*/
        }

        public void Delete(int id)
        {
            var m = this.context.Matieres.Find(id);
            m.Ue = null;
            this.context.Matieres.Remove(m);
        }

        public IEnumerable<Matiere> GetAll()
        {
            return this.context.Matieres.ToList();
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
            return context.Matieres.Count();
        }
    }

}