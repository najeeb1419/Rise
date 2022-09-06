using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace Risen.Models
{
    public class JobScheduler
    {
        public static void Start()
        {
            Quartz.IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<Jobclass>().Build();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(30)
            .RepeatForever())
            .Build();

            scheduler.ScheduleJob(job, trigger);

        }
    }
}