using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plannr.Models;
namespace Plannr.DAL
{
    public interface IEnseignementsRepository : IDisposable
    {
        IEnumerable<Enseignement> GetEnseignementsForTeacher(int id);
        Enseignement Get(int id);
        void Insert(Enseignement e);
        void Delete(int id);
        void Save();
        IEnumerable<Enseignement> GetList();

    }
}