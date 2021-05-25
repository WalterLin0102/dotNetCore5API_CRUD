using System;
using System.Collections.Generic;

#nullable disable

namespace dotNetCore5API_CRUD.Models
{
    public partial class Job
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
        }

        public short JobId { get; set; }
        public string JobDesc { get; set; }
        public byte MinLvl { get; set; }
        public byte MaxLvl { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
