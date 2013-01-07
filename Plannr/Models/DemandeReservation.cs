using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Plannr.Models
{
  
    public class DemandeReservation
    {
        [Key]
        public int Id { get; set; }
        
        // Flag pour notifications pour Responsables UE
      
        public DateTime DateDemande { get; set; }
        
        public bool Checked { get; set; }

        public bool CheckedByTeacher { get; set; }

        public bool BesoinProjecteur { get; set; }
        public bool BesoinPrises { get; set; }
     
        public int CapaciteNecessaire { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateVoulue { get; set; }

       
        public virtual CreneauHoraire CreneauSouhaite { get; set; }

        public int DateDemandeTimestamp
        {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var baseDate = this.DateDemande;
                TimeSpan diff = baseDate - origin;
                return (int)Math.Floor(diff.TotalSeconds);
            }
        }

        public int DateVoulueTimestamp
        {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var baseDate = this.DateVoulue;
                TimeSpan diff = baseDate - origin;
                return (int)Math.Floor(diff.TotalSeconds);
            }

        }


        // Return "simple" object for API
        public string Enseignement_Libelle
        {
            get
            {
                if (this.Enseignement != null)
                {
                    return this.Enseignement.Libelle;
                }
                else
                {
                    return "";
                }
            }
        }

        public int Enseignement_Id
        {
            get
            {
                if (this.Enseignement != null)
                {
                    return this.Enseignement.Id;
                }
                else
                {
                    return 0;
                }
            }
        
        }

        public string CreneauSouhaite_Libelle {
            get {
                if (this.CreneauSouhaite != null)
                {
                    return this.CreneauSouhaite.HeureConcat;
                }
                else
                {
                    return "";
                }
            }
        }


        public int ReservationAssociee_Id
        {
            get
            {
                if (this.ReservationAssociee != null)
                {
                    return this.ReservationAssociee.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        // Navigators only - table de jointure

        public virtual Enseignement Enseignement { get; set; }
 

        public virtual Reservation ReservationAssociee { get; set; }

       
    }
}