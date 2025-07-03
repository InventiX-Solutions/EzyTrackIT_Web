using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TCC_CRM.ClassModules;

[assembly: OwinStartup(typeof(TCC_CRM.Startup))]

namespace TCC_CRM
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //Can hide Hangfire Job
            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate("HangfireHelper.HangfireHelper.SendSecurtexJobReminderEmail", () => HangfireHelper.SendSecurtexJobReminderEmail(), "0 6 * * *");
        }

        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                //.UseSqlServerStorage("Server=DESKTOP-5R4J6DG\\SQLEXPRESS; Database=HangfireTest; uid=sa; password=Inventix@123; Integrated Security=True;", new SqlServerStorageOptions
                .UseSqlServerStorage("data source=172.16.104.82;initial catalog=Hangfire;uid=sa;pwd=Pg$@t$A@12#;Integrated Security=False", new SqlServerStorageOptions
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
