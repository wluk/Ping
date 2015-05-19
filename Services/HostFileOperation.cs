using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xaml;

namespace Services
{
    public static class HostFileOperation
    {
        private static string _path = @"/Hosts/";

        public static void Create(Host host)
        {
            var hostFileName = _path + host.HostName + ".xaml";
            using (TextWriter writer = File.CreateText(hostFileName))
            {
                XamlServices.Save(writer, host);
            }
            ScheduledTasks.TaskRegister(host);
        }

        public static void Delete(string hostName)
        {
            var fileName = hostName + "*.xaml";

            ScheduledTasks.StopJob(hostName);
            var file = Directory.GetFiles(_path, fileName);
            File.Delete(file[0]);
        }

        public static void Update(Host host)
        {
            Delete(host.HostName);
            Create(host);
        }

        public static void UpdateStatus(string nameHost, StatusFlag status)
        {
            var host = GetByNameHost(nameHost);
            Delete(nameHost);
            host.HostStatus = status;
            Create(host);
        }

        public static List<Host> GetAllHosts()
        {
            var fileList = Directory.GetFiles(_path, "*.xaml");
            if (fileList.Count() == 0) return null;
            var hosts = new List<Host>();
            foreach (var name in fileList)
            {
                using (TextReader reader = File.OpenText(name))
                {
                    var host = (Host)XamlServices.Load(reader);
                    hosts.Add(host);
                }
            }
            return hosts;
        }

        public static Host GetByNameHost(string hostName)
        {
            var file = Directory.GetFiles(_path, hostName + "*.xaml");
            if (file.Count() == 0) return null;
            using (TextReader reader = File.OpenText(file[0]))
            {
                return (Host)XamlServices.Load(reader);
            }
        }
    }
}
