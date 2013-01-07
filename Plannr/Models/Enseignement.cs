using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Enseignement
    {
        [Key]
        public int Id {get;set;}

        // Navigators
        [JsonIgnore]
        public virtual Cours Cours { get; set; }
        [JsonIgnore]
        public virtual Enseignant Enseignant { get; set; }
        [JsonIgnore]
        public virtual Groupe Groupe { get; set; }
        // Helper method
        public string Libelle
        {
            get
            {
                if (this.Cours != null)
                {
                    return this.Cours.Libelle + " - " + this.Groupe.Libelle;
                }
                else
                {
                    return "";
                }
          
            }
        }

        

    }
}