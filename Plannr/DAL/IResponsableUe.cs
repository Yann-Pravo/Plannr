using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public interface IResponsableUe : IDisposable
    {
        ResponsableUE Get(int id);
        void Insert(ResponsableUE e);
        void Delete(int id);
        void Edit(ResponsableUE e);
        void Save();
        IEnumerable<ResponsableUE> GetAll();
        void Entry(ResponsableUE e);
        int Count();
        IEnumerable<ResponsableUE> GetList();
        ResponsableUE GetEns(int id);
        Enseignant GetResp(int id);
    }
}