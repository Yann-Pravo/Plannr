using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    public interface ISallesRepository : IDisposable
    {
        Salle Get(int id);
        IEnumerable<Salle> GetSallesCriteres(int capacite, bool projo, DateTime date);
        void Insert(Salle e);
        void Delete(int id);
        IEnumerable<Salle> GetList();
        void Entry(Salle e);
        void Save();
        Salle GetEager(int id);
        int Count();
    }
}
