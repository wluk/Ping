using System.Collections.Generic;
using Quartz;
using Quartz.Impl;
using Services.DTO;
using Services.JOB;

namespace Services
{
    public class ScheduledTasks
    {
        private static IScheduler _sched;

        public ScheduledTasks()
        {
            var schedFact = new StdSchedulerFactory();
            _sched = schedFact.GetScheduler();
            _sched.Start();
        }

        private static void ScheduledTasksStart()
        {
            var schedFact = new StdSchedulerFactory();
            _sched = schedFact.GetScheduler();
            _sched.Start();
        }

        public static void TasksRegister(List<Host> hosts)
        {
            foreach (var h in hosts)
            {
                TaskRegister(h);
            }
        }

        public static void TaskRegister(Host host)
        {
            var job = CreateJob(host);
            var trigger = CreateTrigger(host.HostName, host.FrequencyRequest);

            if (host.HostStatus != StatusFlag.Work) return;
            ScheduledTasksStart();
            _sched.ScheduleJob(job, trigger);
        }

        private static IJobDetail CreateJob(Host host)
        {
            return JobBuilder.Create<HostWebResponse>()
                .WithIdentity("Task for " + host.HostName)
                .UsingJobData("address", host.HostAddress)
                .UsingJobData("mailTo", host.AdminEmail)
                .UsingJobData("reqInterval", host.IntervalRequest)
                .UsingJobData("name", host.HostName)
                .Build();
        }

        private static ITrigger CreateTrigger(string hostName, int triggerInterval)
        {
            return TriggerBuilder.Create()
              .WithIdentity("Trigger for " + hostName)
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(triggerInterval)
                  .RepeatForever())
              .Build();
        }




        public static void StopJob(string hostName)
        {
            var triggerName = "Trigger for " + hostName;
            ScheduledTasksStart();
            _sched.UnscheduleJob(new TriggerKey(triggerName));
        }

        public static void PauseTrigger(string hostName)
        {
            var triggerName = "Trigger for " + hostName;
            _sched.PauseTrigger(new TriggerKey(triggerName));
        }

        public static void StartTrigger(string hostName)
        {
            var triggerName = "Trigger for " + hostName;
            _sched.ResumeTrigger(new TriggerKey(triggerName));
        }
    }
}
