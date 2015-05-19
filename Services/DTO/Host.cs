namespace Services
{
    public class Host
    {
        public string HostName { get; set; }
        public string HostAddress { get; set; }
        public string AdminEmail { get; set; }
        public int FrequencyRequest { get; set; }
        public int IntervalRequest { get; set; }
        public StatusFlag HostStatus { get; set; }

    }

    public enum StatusFlag
    {
        Work = 0,
        Stop = 1
    }
}

