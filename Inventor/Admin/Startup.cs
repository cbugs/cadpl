using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Web;
using Hangfire.Dashboard;
using Hangfire.SqlServer;

[assembly: OwinStartup(typeof(Admin.Startup))]
namespace Admin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            // Map Dashboard to the `http://<your-app>/hangfire` URL.

            //Stat pool every 5 minutes
            var statInterval = new TimeSpan(0, 5, 0);
            var dashboardOptions = new DashboardOptions()
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() },
                AppPath = VirtualPathUtility.ToAbsolute("~"),
                StatsPollingInterval = (int)statInterval.TotalMilliseconds
            };

//            var localStorage = new SqlServerStorage("DefaultHangfire");
//            var prodStorage = new SqlServerStorage("ProdHangfire");
//
//            app.UseHangfireDashboard("/hangfirelocal", dashboardOptions, localStorage);
//            app.UseHangfireDashboard("/hangfireprod", dashboardOptions, prodStorage);
        }

        public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                return true;
                // In case you need an OWIN context, use the next line,
                // `OwinContext` class is the part of the `Microsoft.Owin` package.
                //var context = new OwinContext(owinEnvironment);

                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                //return context.Authentication.User.Identity.IsAuthenticated;
            }
        }
    }
}