using System.Linq;
using Services;

namespace UI.WEB
{
    public static class TaskConfig
    {
        public static void TasksStart()
        {
            var hosts = HostFileOperation.GetAllHosts();
            if (hosts == null) return;
            var scheduledTask = new ScheduledTasks();
            scheduledTask.TasksRegister(hosts);
        }
    }
}