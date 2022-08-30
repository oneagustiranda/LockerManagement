using System.ComponentModel.DataAnnotations;

namespace Locker.API.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}
