using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Salle
    {
        [Key]
        public int Id {get;set;}

   
        public string Libelle { get; set; }
  
        public bool AProjecteur { get; set; }
  
        public int Capacite { get; set; }
     
        public bool APrises { get; set; }



        public virtual Batiment Batiment { get; set; }
    }
}