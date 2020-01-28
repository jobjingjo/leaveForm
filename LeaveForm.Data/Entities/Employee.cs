using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveForm.Data.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required, Display(Name = "Line User Id")]
        public string LineUserId { get; set; }
    }
}