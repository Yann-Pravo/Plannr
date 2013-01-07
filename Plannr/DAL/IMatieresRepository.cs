using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    public interface IMatieresRepository : IDisposable
    {
        Matiere Get(int id);
        Matiere GetEager(int id);
        void Insert(Matiere m);
        void Delete(int id);
        void Edit(Matiere m);
        void Save();
        IEnumerable<Matiere> GetAll();
        int Count();
    }
}
