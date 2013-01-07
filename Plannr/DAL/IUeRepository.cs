using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plannr.DAL
{
    public interface IUeRepository : IDisposable
    {
        Ue Get(int id);
        void Insert(Ue u);
        void Delete(int id);
        IEnumerable<Ue> GetList();
        void Entry(Ue u);
        void Save();
        int Count();
        Ue GetEager(int id);
        void Edit(Ue m);
    }
}
