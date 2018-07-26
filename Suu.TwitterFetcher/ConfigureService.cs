using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Quartz;

namespace Suu.TwitterFetcher
{
   internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(x =>
            {
                x.Service<FetcherService>(s =>
                {
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                    s.ConstructUsing(() => new FetcherService());

                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<MyJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                                .WithSimpleSchedule(b => b
                                    .WithIntervalInSeconds(10)
                                    .RepeatForever())
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("My Topshelf Service");
                x.SetDisplayName("My Topshelf Service");
                x.SetDescription("My Topshelf Service's description");
            });
        }


    }

  
}
   