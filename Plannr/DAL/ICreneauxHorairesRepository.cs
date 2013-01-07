using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plannr.Models;
namespace Plannr.DAL
{
    public interface ICreneauxHorairesRepository : IDisposable
    {

        IEnumerable<CreneauHoraire> getCreneauxHoraires();
        IEnumerable<CreneauHoraire> getCreneauxHorairesForDate(DateTime date);

        CreneauHoraire Find(int id);

    }
}