using System;
using System.Collections.Generic;
using System.Linq;
using Plannr.Models;

namespace Plannr.DAL
{
    public class DemandesRepository : IDemandesRepository, IDisposable
    {
        private PlannrContext context;


        public DemandesRepository(PlannrContext context)
        {
            this.context = context;
        }

        // Retourne la liste des reservations effectuées par un utilisateur qui a l'id "id"
        public IEnumerable<DemandeReservation> GetReservationsBy(int id)
        {
            return this.context.DemandesReservation.Where(x => x.Enseignement.Enseignant.UserId == id).OrderByDescending(x =>x.DateDemande).ToList();
        }
        // REtourne la liste des reservations qu'un responsable doit vérifier
        public IEnumerable<DemandeReservation> GetReservationTo(int id)
        {
            return this.context.DemandesReservation.Where(x => x.Enseignement.Cours.Matiere.Ue.ResponsableUe.UserId == id && x.ReservationAssociee == null).ToList();
        }

        public DemandeReservation FindEager(int id)
        {
            return this.context.DemandesReservation.Include("ReservationAssociee").Single(s => s.Id == id);
        }

        public void Edit(DemandeReservation demande)
        {
            this.context.Entry(demande).State = System.Data.EntityState.Modified;
        }

        public void Insert(DemandeReservation demande)
        {
            this.context.DemandesReservation.Add(demande);
        }

        public void Delete(int id)
        {
            var e = this.context.DemandesReservation.Find(id);
            this.context.DemandesReservation.Remove(e);
        }

        public void Delete(DemandeReservation demande)
        {
            this.context.DemandesReservation.Remove(demande);
        }

        public DemandeReservation Find(int id)
        {
            return this.context.DemandesReservation.Find(id);
        }

        public IEnumerable<DemandeReservation> GetUnseenDemandes(int id)
        {

            return this.context.DemandesReservation.Where(x => x.Enseignement.Enseignant.UserId == id && x.CheckedByTeacher == false && x.ReservationAssociee != null).ToList();

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