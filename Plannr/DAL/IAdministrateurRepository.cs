using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    interface IAdministrateurRepository : IDisposable
    {
        Administrateur Get(int id);
        void Insert(Administrateur e);
        void Delete(int id);
        void Edit(Administrateur e);
        void Save();
        IEnumerable<Administrateur> GetAll();
        void Entry(Administrateur e);
        int Count();
        IEnumerable<Administrateur> GetList();
    }
}
