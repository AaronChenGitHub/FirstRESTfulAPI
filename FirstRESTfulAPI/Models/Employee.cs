using System;
using System.Collections.Generic;

namespace FirstRESTfulAPI.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
