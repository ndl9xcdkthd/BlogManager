using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BlogManager.Web.Areas.Office.Models
{
    public class EmployeeViewModel
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
        public byte[] Image { get; set; }
        public SelectList Departments { get; set; }
    }
}
