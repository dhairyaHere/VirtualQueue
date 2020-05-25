using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VirtualQueue.Models;

namespace VirtualQueue
{
    public class MvcApplication : System.Web.HttpApplication
    {
        VirtualQueue.Models.VQContext ctx = new Models.VQContext();
        List<ProjectConfig> l = new List<ProjectConfig>();
        protected void Application_Start()
        {
            
            l =ctx.ProjectConfigs.ToList();
            foreach(ProjectConfig pc in l)
            {
                Application[pc.att_key] = pc.att_val;
            }
            Application["MessageCount"] = Convert.ToInt32(Application["MessageCount"]);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        
    }
}
