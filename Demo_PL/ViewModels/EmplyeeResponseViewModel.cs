using Demo_DAL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo_PL.ViewModels
{
    public class EmplyeeResponseViewModel
    {
        //public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        //public DateTime CreatioDate { get; set; } = DateTime.Now;
        //public bool IsDeleted { get; set; } = false;


        //public IFormFile image { get; set; }
        public string ImageName { get; set; }
        public string Gender { get; set; }
        //public EmployeeType EmpType { get; set; }

        public Department Department_Nav { get; set; }
        public int? Department_ID { get; set; }
    }
}
