using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Groupe
    {

        [Key]
        public int Id { get; set; }
        
        public string Libelle { get; set; }
       


        public virtual ICollection<Groupe> SousGroupes { get; set; }
        public virtual Groupe GroupePere { get; set; }
        public virtual ICollection<Enseignement> Enseignements { get; set; }
       


    }
}