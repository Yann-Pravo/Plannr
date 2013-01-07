using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plannr.Models;
namespace Plannr.DAL
{
    public interface IDemandesRepository : IDisposable
    {
        // Get reservations faites par une ID
        IEnumerable<DemandeReservation> GetReservationsBy(int id);
        // Get reservations qu'un user ID doit valider (un resp ue)
        IEnumerable<DemandeReservation> GetReservationTo(int id);
        // Obtient les réservations validées non vues par l'enseignant
        IEnumerable<DemandeReservation> GetUnseenDemandes(int id);
        
        DemandeReservation Find(int id);
        DemandeReservation FindEager(int id);
        void Edit(DemandeReservation demande);
        void Insert(DemandeReservation demande);
        void Delete(int id);
        void Delete(DemandeReservation demande);
        void Save();

    }
}