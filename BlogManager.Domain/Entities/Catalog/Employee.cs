﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManager.Domain.Entities.Catalog
{
    public class Employee
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
