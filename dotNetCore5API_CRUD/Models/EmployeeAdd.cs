using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCore5API_CRUD.Models
{
    public class EmployeeAdd : Employee
    {
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public int Gender { get; set; }
    }
}
