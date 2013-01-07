using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class CreneauHoraire
    {
        [Key]
        public int Id { get; set; }
        // Un cours ne peut pas commencer avant 8h et après 20h
        //[Range(8,20)]
        public int HeureDebut { get; set; }
        // Un cours ne peut pas terminer avant 9h et après 22h
        //[Range(9,22)]
        public int HeureFin { get; set; }

        public string HeureConcat
        {
            get
            {
                return this.HeureDebut + "h - " + this.HeureFin + "h";
            }
        }
    }
}