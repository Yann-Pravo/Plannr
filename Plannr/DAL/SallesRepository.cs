using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class SallesRepository : ISallesRepository
    {

        private PlannrContext context;

        public SallesRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Salle Get(int id)
        {
            return this.context.Salles.Find(id);
        }

        public Salle GetEager(int id)
        {
            var a = this.context.Salles.Include("Batiment").Single(s => s.Id == id);
            //var m = db.Matieres.Include(p => p.Ue).Single(s => s.Id == matiere.Id);

            return a;
        }

        // Retourne les salles  libres pour une date, et correspondanets aux criteres capacite/projo nécessaire
        public IEnumerable<Salle> GetSallesCriteres(int capacite, bool projo, DateTime date)
        {

            return (from salle in this.context.Salles.Where(x => x.Capacite >= capacite && x.AProjecteur == projo && (projo == false || x.AProjecteur == projo)).ToList()
                    where !(from resa in this.context.Reservations where resa.Date == date select resa.Salle.Id).Contains(salle.Id)
                    select salle).ToList();
       
        }


        public void Insert(Models.Salle e)
        {
            IEnumerable<Salle> salles = GetList();
            IEnumerable<Salle> test = salles.Where(x => x.Libelle == e.Libelle && x.Batiment == e.Batiment);
            if (test.Count() == 0)
            {
                this.context.Salles.Add(e);
            }
        }

        public void Delete(int id)
        {
            var e = this.context.Salles.Find(id);
            this.context.Salles.Remove(e);
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

        public IEnumerable<Salle> GetList() {

         return this.context.Salles.ToList();
        }

     
        public void Entry(Salle e) {
            this.context.Entry(e).State = System.Data.EntityState.Modified;
        }

        public int Count()
        {
            return context.Salles.Count();
        }
    }

}