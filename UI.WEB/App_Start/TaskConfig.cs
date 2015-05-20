using Services;

namespace UI.WEB
{
    public static class TaskConfig
    {
        public static void TasksStart()
        {
            var hosts = HostFileOperation.GetAllHosts();
            if (hosts == null) return;
            ScheduledTasks.TasksRegister(hosts);
        }
    }
}