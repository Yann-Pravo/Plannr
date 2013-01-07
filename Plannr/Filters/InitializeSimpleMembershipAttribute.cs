using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using Plannr.Models;
using System.Web.Security;

namespace Plannr.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                
                // Little hack, to be fixed LATER
                
                try
                {
                    using (var context = new PlannrContext())
                    {
                        

                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }

                        context.Enseignants.Find(1);
                    }
                    

                    WebSecurity.InitializeDatabaseConnection("PlannrContext", "Personne", "UserId", "UserName", autoCreateTables: true);
       
                    // Add ResponsableUE role

                    const string respRole = "ResponsableUE";
                    const string enseignantRole = "Enseignant";
                    const string adminRole = "Administrateur";

                    if (!Roles.RoleExists(adminRole))
                    {
                        Roles.CreateRole(adminRole);
                    }

                    if (!Roles.RoleExists(respRole))
                    {
                        Roles.CreateRole(respRole);

                    }

                    if (!Roles.RoleExists(enseignantRole))
                    {
                        Roles.CreateRole(enseignantRole);
                    }

                    if (!Roles.IsUserInRole("Admin",adminRole))
                    {
                        WebSecurity.CreateAccount("Admin", "Admin");

                        WebSecurity.CreateAccount("AnneLaurent", "AnneLaurent");

                        WebSecurity.CreateAccount("LysianneBuisson", "LysianneBuisson");

                        WebSecurity.CreateAccount("ClementJonquet", "ClementJonquet");

                        WebSecurity.CreateAccount("TiberiuStratulat", "TiberiuStratulat");


                        Roles.AddUserToRole("Admin", adminRole);

                        Roles.AddUserToRole("AnneLaurent", respRole);
                        Roles.AddUserToRole("AnneLaurent", enseignantRole);
                        Roles.AddUserToRole("ClementJonquet", respRole);
                        Roles.AddUserToRole("ClementJonquet", enseignantRole);
                        Roles.AddUserToRole("LysianneBuisson", respRole);
                        Roles.AddUserToRole("LysianneBuisson", enseignantRole);
                        Roles.AddUserToRole("TiberiuStratulat", enseignantRole);
                    }

                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
