using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Matiere
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Libelle { get; set; }

        
        public virtual ICollection<Cours> Cours { get; set; }
        public virtual Ue Ue { get; set; }
    }
}