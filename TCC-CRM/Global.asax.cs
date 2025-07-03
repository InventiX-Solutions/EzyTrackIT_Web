using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TCC_CRM;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using TCC_CRM.ClassModules;

namespace TCC_CRM
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            ////// Code that runs on application startup
            ////BundleConfig.RegisterBundles(BundleTable.Bundles);
            ////AuthConfig.RegisterOpenAuth();

            ////HangfireAspNet.Use(GetHangfireServers);

            //////GetHangfireServers();

            //HangfireBootstrapper.Instance.Start();

            //// Let's also create a sample background job
            //BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));
            ////RecurringJob.AddOrUpdate(HangfireHelper.SendAttendanceStatus(), Cron.HourInterval(12));
            //RecurringJob.AddOrUpdate("HangfireHelper.SendAttendanceStatus", () => HangfireHelper.SendAttendanceStatus(), "52 21 * * *");

            ////RecurringJob.AddOrUpdate("myrecurringjob" () => Console.WriteLine("Recurring!"),Cron.Daily);
            ///

            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            //HangfireAspNet.Use(GetHangfireServers);

            //// Let's also create a sample background job
            //BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            //HangfireBootstrapper.Instance.Stop();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Server=SYEDARIF\\SQLEXPRESS17; Database=HangfireTest; uid=sa; password=Peritus123; Integrated Security=True;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }
    }
}
