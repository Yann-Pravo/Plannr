using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Plannr.Models
{

    public class PlannrInitializer : DropCreateDatabaseIfModelChanges<PlannrContext>
    {
        protected override void Seed(PlannrContext context)
        {



            // Creneau génération
            var borneMax = 22;
            for (int i = 8; i <= 20; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (i + j <= borneMax)
                    {
                        var cur_Creneau = new CreneauHoraire()
                        {
                            HeureDebut = i,
                            HeureFin = (i + j)
                        };
                        context.CreneauxHoraires.Add(cur_Creneau);
                        context.SaveChanges();
                    }
                }

            }

            var batiment = new Batiment()
            {
                Id = 1,
                Nom = "Polytech",
                carac="ta mere"

               
            };

            context.Batiments.Add(batiment);
            context.SaveChanges();



            var salle = new List<Salle>()
            {
                new Salle() {
                    Id = 1,
                    Libelle = "202",
                 
                    APrises = true,
                AProjecteur = true,
                Capacite = 100,
                Batiment = batiment
                },
                new Salle() {
                    Id = 2,
                    Libelle = "201",
                      APrises = true,
                AProjecteur = false,
                Capacite = 50,
                Batiment = batiment
                }
            };

            salle.ForEach(p => context.Salles.Add(p));
            context.SaveChanges();

            // Add Creneaux Horaires disponibles

            


            var typeCours = new TypeCours()
            {
                Id = 1,
                Type = "CM"

            };

            context.TypesCours.Add(typeCours);
            context.SaveChanges();

            var td = new TypeCours()
            {
                Id = 3,
                Type = "TD"

            };

            context.TypesCours.Add(td);
            context.SaveChanges();

            var tp = new TypeCours()
            {
                Id = 2,
                Type = "TP"

            };

            context.TypesCours.Add(tp);
            context.SaveChanges();

            var admin = new Administrateur()
            {
                UserId = 1,
                UserName = "Admin",
                Name = "Admin",
                FirstName = "Admin",
                Tel = "0601010100",
                AdminDepuis = DateTime.Parse("10/01/2009")
                
            };

            context.Administrateurs.Add(admin);
            context.SaveChanges();

            

            var responsable = new ResponsableUE()
            {
                UserId = 3,
                UserName = "ClementJonquet",
                Name = "Jonquet",
                FirstName = "Clement",
                Tel = "0601010102",
                ResponsableDepuis = DateTime.Parse("10/01/2009")
            };
            context.ResponsablesUE.Add(responsable);
            context.SaveChanges();

            var responsable2 = new ResponsableUE()
            {
                UserId = 2,
                UserName = "AnneLaurent",
                Name = "Laurent",
                FirstName = "Anne",
                Tel = "0601010101",
                ResponsableDepuis = DateTime.Parse("10/01/2009")
            };
            context.ResponsablesUE.Add(responsable2);
            context.SaveChanges();

            var responsable3 = new ResponsableUE()
            {
                UserId = 5,
                UserName = "LysianneBuisson",
                Name = "Lysianne",
                FirstName = "Buisson",
                Tel = "0601010103",
                ResponsableDepuis = DateTime.Parse("10/01/2009")
            };


            context.ResponsablesUE.Add(responsable3);
            context.SaveChanges();

            var enseignant = new Enseignant()
            {
                UserId = 4,
                UserName = "TiberiuStratulat",
                Name = "Stratulat",
                FirstName = "Tiberiu",
                Tel = "0601010102"
            };
            context.Enseignants.Add(enseignant);
            context.SaveChanges();

          

            var ue1 = new Ue()
            {
                Id = 1,
                ResponsableUe = responsable,
                Libelle = "UE Architecture des Systèmes d'Information & Internet",
                Description = "Architecture des Systèmes d'Information & Internet"
            };
            context.Ues.Add(ue1);
            context.SaveChanges();

            var ue2 = new Ue()
            {
                Id = 2,
                ResponsableUe = responsable2,
                Libelle = "UE Système d’Information",
                Description = "Système d’Information"
            };
            context.Ues.Add(ue2);
            context.SaveChanges();

            var ue3 = new Ue()
            {
                Id = 3,
                ResponsableUe = responsable3,
                Libelle = " UE Entreprise et Droit",
                Description = "Entreprise et Droit"
            };
            context.Ues.Add(ue3);
            context.SaveChanges();
            


            var matiere = new List<Matiere>() {
                new Matiere() {
                    Id = 1,
                    Ue = ue3,
                    Libelle = "Droit"
                },
                new Matiere() {

                    Id = 2,
                    Ue = ue1,
                    Libelle = "AIOP"
                }
             
              };

            matiere.ForEach(x => context.Matieres.Add(x));
            context.SaveChanges();

             var cours = new List<Cours>()
            {
                new Cours() {
                    Id = 1,
                    Libelle = "Initiation au droit - Marques et Brevets",
                    TypeCours = typeCours,
                    Matiere = matiere[0]
                },
                new Cours() {
                    Id = 2,
                    Libelle = "Prog Web",
                    TypeCours = typeCours,
                    Matiere = matiere[1]
                }
            };
             cours.ForEach(x => context.Cours.Add(x));
            context.SaveChanges();

       
                var groupe = new Groupe() {
                    Id = 1,
                    Libelle = "IG5"
                };

               /* var groupe1 = new Groupe()
                {
                    Id = 2,
                    Libelle = "IG5 groupe 1"
                };


                var groupe2 = new Groupe()
                {
                    Id = 3,
                    Libelle = "IG5 groupe 2"
                };*/
                var sous_groupe = new Groupe()
                {
                    Id = 2,
                    Libelle = "IG5 Groupe 1",
                    GroupePere = groupe
                };

                var sous_groupe2 = new Groupe()
                {
                    Id = 3,
                    Libelle = "IG5 Groupe 2",
                    GroupePere = groupe
                };

            context.Groupes.Add(groupe);
            
            context.SaveChanges();

            context.Groupes.Add(sous_groupe);
            context.SaveChanges();

            context.Groupes.Add(sous_groupe2);
            context.SaveChanges();


            var enseignement = new List<Enseignement>()
            {
                new Enseignement() {
                    Id = 1,
                    Cours = cours[1],
                    Enseignant = responsable,
                    Groupe = groupe
                },
                new Enseignement() {
                    Id = 2,
                    Cours = cours[0],
                    Enseignant = enseignant,
                    Groupe = sous_groupe
                }
            };

            enseignement.ForEach(x => context.Enseignements.Add(x));
            context.SaveChanges();

            var creneau1 = context.CreneauxHoraires.Find(1);
  

            var reservation = new Reservation()
            {
                Id = 1,
                Date = DateTime.Parse("25/10/2012"),
                Creneau = creneau1,
                Enseignement = enseignement[0],
     
                Salle = salle[0]
            };

            context.Reservations.Add(reservation);
            context.SaveChanges();

                var demande = new List<DemandeReservation>() {
               new DemandeReservation() {
                   Id = 1,
                   Checked = false,
                  Enseignement = enseignement[0],
                  CapaciteNecessaire = 50,
                  BesoinPrises = false,
                  BesoinProjecteur = true,
                  DateVoulue = DateTime.Parse("25/10/2012"),
                  CreneauSouhaite = creneau1,
                  DateDemande = DateTime.Parse("22/10/2012"),
                  ReservationAssociee = reservation,
                  CheckedByTeacher = false
               },
               new DemandeReservation() {

                   Id = 2,
                   Checked = true,
                   Enseignement = enseignement[1],
                   CapaciteNecessaire = 100,
                   BesoinProjecteur = false,
                   BesoinPrises =false,
                   CreneauSouhaite = creneau1,
                   DateVoulue = DateTime.Parse("25/10/2012"),
                   CheckedByTeacher = false,
                   DateDemande = DateTime.Parse("24/10/2012")
               }
            };

            demande.ForEach(x => context.DemandesReservation.Add(x));
            context.SaveChanges();

           

        
            
            
        }

            
    }
}