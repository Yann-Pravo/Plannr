using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class ResponsableUE : Enseignant
    {

        // Juste un attribut "test" pour voir comment EF réagit a l'héritage et custom attributes sur les fils
        public DateTime ResponsableDepuis { get; set; }

        public virtual ICollection<Ue> Ues { get; set; }
    }
}