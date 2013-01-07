using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Plannr.Models;

namespace Plannr.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Creneau_Libelle {
            get{
                return this.Creneau.HeureConcat;
            }
        }

        public string Enseignement_Libelle
        {
            get
            {
                return this.Enseignement.Libelle;
            }
        }

        public string Salle_Libelle
        {
            get
            {
                return this.Salle.Libelle;
            }
        }

        public ReservationCalendar ConvertObject()
        {

            var title = this.Enseignement.Libelle+" ("+this.Salle.Libelle+")";
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var baseDate = this.Date;
            TimeSpan diff = baseDate - origin;
            var seconds = (int) Math.Floor(diff.TotalSeconds);

            // Add first creneau
            int creneauDepartSeconds = this.Creneau.HeureDebut * 3600;
            int creneauEndSeconds = this.Creneau.HeureFin * 3600;

            int start = seconds + creneauDepartSeconds;
            int end = seconds + creneauEndSeconds;

            ReservationCalendar newObject = new ReservationCalendar()
            {
                id = this.Id,
                title = title,
                start = start,
                end = end
            };

            return newObject;

        }

        [JsonIgnore]
        public virtual CreneauHoraire Creneau { get; set; }
        [JsonIgnore]
        public virtual Enseignement Enseignement { get; set; }
        [JsonIgnore]
        public virtual Salle Salle { get; set; }
      

    }
}