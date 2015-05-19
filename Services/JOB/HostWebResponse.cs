using Quartz;
using Services.DTO;

namespace Services.JOB
{
    public class HostWebResponse : IJob
    {
        //Zadanie cyklicznego pingowania
        public void Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var address = dataMap.GetString("address");
            var mailTo = dataMap.GetString("mailTo");
            var reqInterval = dataMap.GetInt("reqInterval");
            var name = dataMap.GetString("name");

            var checkHost = WebResponse.CheckHost(address, reqInterval);
            if (checkHost == null) return;
            ScheduledTasks.StopJob(address);
            HostFileOperation.UpdateStatus(name, StatusFlag.Stop);
            var mail = new MailMessage(address, mailTo, name, checkHost.StatusCode, checkHost.StatusDescription);
            Emailer.SendAllert(mail);
        }
    }
}
