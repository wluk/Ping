using System.ComponentModel.DataAnnotations;

namespace UI.WEB.ViewModel
{
    public class HostVm
    {
        public int HostId { get; set; }

        [Required]
        [Display(Name = "Nazwa hosta")]
        public string HostName { get; set; }

        [Required]
        [Display(Name = "Adress WWW hosta")]
        //[DataType(DataType.Url)]
        public string HostAddress { get; set; }

        [Required]
        [Display(Name = "Adress e-mail administratora")]
        [DataType(DataType.EmailAddress)]
        public string AdminEmail { get; set; }

        [Required]
        [Display(Name = "Częstotliwość odpytania hostów (w minutach)")]
        [Range(typeof(int), "1", "720", ErrorMessage = "{0} wymagana jest liczba całkowita, z przedziału od {1} do {2}.")]
        public int FrequencyRequest { get; set; }

        [Required]
        [Display(Name = "Odstęp pomiędzy powtórzeniem zapytania hosta (w sekundach)")]
        [Range(typeof(int), "1", "720", ErrorMessage = "{0} wymagana jest liczba całkowita, z przedziału od {1} do {2}.")]
        public int IntervalRequest { get; set; }

        [Display(Name = "Status")]
        public StatusFlag Status { get; set; }
    }

    public enum StatusFlag
    {
        Work = 0,
        Stop = 1
    }
}