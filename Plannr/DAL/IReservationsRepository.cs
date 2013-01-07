using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    public interface IReservationsRepository : IDisposable
    {
        Reservation Get(int id);
        Reservation GetEager(int id);
        IEnumerable<Reservation> GetAll();
        // Obtient les reservations assignées a un enseignant id
        IEnumerable<Reservation> GetReservationsFor(int id);
        // Obtietn les réservations associéeas a une promotion
        IEnumerable<Reservation> GetReservationsForGroupe(int id);
        void Insert(Reservation e);
        void Delete(int id);
        void Save();

    }
}
