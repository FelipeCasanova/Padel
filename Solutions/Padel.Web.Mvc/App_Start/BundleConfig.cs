using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Padel.Web.Mvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/base").Include(
                         "~/" + Links.Scripts.jquery_1_9_1_min_js,
                         "~/" + Links.Scripts.jquery_validate_min_js,
                         "~/" + Links.Scripts.jquery_validate_unobtrusive_min_js,
                         "~/" + Links.Scripts.additional_methods_min_js,
                         "~/" + Links.Scripts.jquery_unobtrusive_ajax_js,
                         "~/" + Links.Scripts.bootstrap.bootstrap_min_js,
                         "~/" + Links.Scripts.bootstrap.bootstrap_validation_js,
                         "~/" + Links.Scripts.bootstrap.holder_js,
                         "~/" + Links.Scripts.angularjs.angular_min_js,
                         "~/" + Links.Scripts.angularjs.angular_route_min_js,
                         "~/" + Links.Scripts.padel.padel_js
                         ));

            bundles.Add(new ScriptBundle("~/bundles/js/adminbase").Include(
                         "~/" + Links.Scripts.angularjs.grid.ng_grid_min_js,
                         "~/" + Links.Scripts.slimscroll.jquery_slimscroll_min_js,
                         "~/" + Links.Scripts.padel.admin.admin_js
                         ));

            bundles.Add(new ScriptBundle("~/bundles/js/torneo").Include(
                         "~/" + Links.Scripts.jqtimeto.jquery_timeTo_min_js,
                         "~/" + Links.Scripts.fuelux.wizard_js,
                         "~/" + Links.Scripts.padel.torneos.apuntate_js
                         ));

            bundles.Add(new ScriptBundle("~/bundles/js/useradmin").Include(
                         "~/" + Links.Scripts.padel.jugador.plugins.metisMenu.jquery_metisMenu_js,
                         "~/" + Links.Scripts.padel.jugador.sb_admin_js
                         ));

            bundles.Add(new StyleBundle("~/Content/css/base").Include(
                        "~/" + Links.Content.bootstrap.css.bootstrap_min_css,
                        "~/" + Links.Content.bootstrap.css.bootstrap_theme_css,
                        "~/" + Links.Content.fontawesome.css.font_awesome_css,
                        "~/" + Links.Content.ionicons.css.ionicons_min_css,
                        "~/" + Links.Content.bootstrap.carousel.carousel_css,
                        "~/" + Links.Content.Site_css
                        ));

            bundles.Add(new StyleBundle("~/Content/css/adminbase").Include(
                        "~/" + Links.Content.bootstrap.css.bootstrap_min_css,
                        "~/" + Links.Content.fontawesome.css.font_awesome_css,
                        "~/" + Links.Content.admin.grid.ng_grid_css,
                        "~/" + Links.Content.admin.style_css
                        ));

            bundles.Add(new StyleBundle("~/Content/css/torneo").Include(
                        "~/" + Links.Content.jqtimeto.timeTo_css,
                        "~/" + Links.Content.fuelux.css.fuelux_min_css,
                        "~/" + Links.Content.fuelux.css.padel_fuelux_css
                        ));

            // Code removed for clarity.
            // BundleTable.EnableOptimizations = true;
        }
    }
}