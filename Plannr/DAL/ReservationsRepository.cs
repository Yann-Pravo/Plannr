using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class ReservationsRepository : IReservationsRepository
    {

        private PlannrContext context;

        public ReservationsRepository(PlannrContext context)
        {
            this.context = context;
        }

        public Reservation Get(int id)
        {
            return this.context.Reservations.Find(id);
        }

        public Reservation GetEager(int id)
        {
            return this.context.Reservations.Include("Enseignement").FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return this.context.Reservations.ToList();
        }

        public IEnumerable<Reservation> GetReservationsFor(int id)
        {
            return this.context.Reservations.Where(x => x.Enseignement.Enseignant.UserId == id).ToList();
        }

        public IEnumerable<Reservation> GetReservationsForGroupe(int id)
        {

            return this.context.Reservations.Where(x => x.Enseignement.Groupe.Id == id).ToList();

        }

        public void Insert(Reservation e)
        {
            this.context.Reservations.Add(e);
        }

        public void Delete(int id)
        {
            var e = this.Get(id);
           this.context.Reservations.Remove(e);
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
    }
    
}