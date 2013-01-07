using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plannr.Models;
namespace Plannr.DAL
{
    public interface ICoursRepository : IDisposable
    {


        Cours Find(int id);
        Cours FindEager(int id);
        void Edit(Cours cours);
        void Insert(Cours cours);
        void Delete(int id);
        void Delete(Cours cours);
        void Save();
        int Count();
        IEnumerable<Cours> GetList();

    }
}