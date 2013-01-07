using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Ue
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }

        // Navigators
        public virtual ICollection<Matiere> Matieres { get; set; }
        public virtual ResponsableUE ResponsableUe { get; set; }


    }
}