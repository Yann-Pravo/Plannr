using System.Web;
using System.Web.Optimization;

namespace Plannr
{
    public class BundleConfig
    {
        // Pour plus d’informations sur le Bundling, accédez à l’adresse http://go.microsoft.com/fwlink/?LinkId=254725 (en anglais)
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/Plannr/underscore.js",
                        "~/Scripts/Plannr/backbone.js",
                        "~/Scripts/Plannr/handlebars.js",
                        "~/Scripts/Plannr/generic.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       
                        "~/Scripts/jquery-ui-{version}.js",
                         "~/Scripts/Plannr/date.js",
                        "~/Scripts/Plannr/jquery.weekcalendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/plannrSinglePage").Include(
                "~/Scripts/Plannr/LayoutManager.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/unicorn").Include(
                "~/Scripts/Plannr/unicorn/unicorn.js",
                "~/Scripts/Plannr/unicorn/unicorn.dashboard.js"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/bootstrap").Include(
                "~/Content/themes/bootstrap/bootstrap.css",
                "~/Content/themes/bootstrap/bootstrap-responsive.css",
 
                "~/Content/themes/bootstrap/unicorn.main.css",
                "~/Content/themes/bootstrap/unicorn.grey.css"));
            bundles.Add(new StyleBundle("~/Content/themes/fullcalendar").Include(

                "~/Content/themes/bootstrap/jquery-ui-1.8.16.custom.css",
                "~/Content/themes/bootstrap/jquery.weekcalendar.css"
                ));
            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l’outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}