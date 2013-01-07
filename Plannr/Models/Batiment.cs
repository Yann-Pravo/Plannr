using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Batiment
    {
        [Key]
        public int Id { get; set; }

        public string Nom { get; set; }

       [Required]
        public string carac { get; set; }


        public virtual ICollection<Salle> Salles { get; set; }
    }
}