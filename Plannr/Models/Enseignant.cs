using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Enseignant : Personne
    {

        public virtual ICollection<Enseignement> Enseignements { get; set; }
      
    }
}