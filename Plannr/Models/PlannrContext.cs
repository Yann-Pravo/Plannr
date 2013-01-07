using System.Data.Entity;
using System.Data.Objects;

namespace Plannr.Models
{
    public class PlannrContext : DbContext
    {
        // Vous pouvez ajouter du code personnalisé à ce fichier. Les modifications ne seront pas remplacées.
        // 
        // Si vous voulez qu'Entity Framework abandonne et régénère la base de données
        // automatiquement à chaque fois que vous modifiez le schéma du modèle, ajoutez le code
        // suivant à la méthode Application_Start dans le fichier Global.asax.
        // Remarque : cette opération supprime et recrée la base de données à chaque modification du modèle.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Plannr.Models.PlannrContext>());

        public PlannrContext() : base("name=PlannrContext")
        {
        }

        public DbSet<Salle> Salles { get; set; }
        public DbSet<Batiment> Batiments { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Enseignement> Enseignements { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Personne> Personnes { get; set; }
        public DbSet<ResponsableUE> ResponsablesUE { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<TypeCours> TypesCours { get; set; }
        public DbSet<Ue> Ues { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<DemandeReservation> DemandesReservation { get; set; }
        public DbSet<CreneauHoraire> CreneauxHoraires { get; set; }

       


    }
}
