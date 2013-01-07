using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    public interface IEnseignantsRepository : IDisposable
    {
        Enseignant Get(int id);
        Enseignant GetEager(int id);
        void Insert(Enseignant e);
        void Delete(int id);
        void Edit(Enseignant e);
        void Save();
        IEnumerable<Enseignant> GetAll();
        IEnumerable<Enseignant> GetList();
        void Entry(Enseignant e);
        int Count();
    }
}
