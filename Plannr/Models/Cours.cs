using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Cours
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }

        // Entity Framework will know how to map foreign keys
               
        public virtual TypeCours TypeCours { get; set; }
        public virtual Matiere Matiere { get; set; }


    }
}