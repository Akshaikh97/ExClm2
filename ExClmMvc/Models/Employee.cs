using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExClmMvc.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
    }
}