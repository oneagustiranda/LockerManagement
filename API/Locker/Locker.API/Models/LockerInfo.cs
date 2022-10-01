using System.ComponentModel.DataAnnotations;

namespace Locker.API.Models
{
    public class LockerInfo
    {
        [Key]
        public string LockerNo { get; set; }
        public string? EmployeeNumber { get; set; }        
        public int Size { get; set; }
        public string Location { get; set; }
        public bool IsEmpty { get; set; }
    }
}
