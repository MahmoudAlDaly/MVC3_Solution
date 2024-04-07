using Demo_DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo_PL.ViewModels
{
    public enum Sex : byte
    {
        [EnumMember(Value = "Male")]
        Male = 1,

        [EnumMember(Value = "Female")]
        Female = 2,
    }

    public enum EmployeeType : byte
    {
        [EnumMember(Value = "Full Time")]
        FullTime = 1,

        [EnumMember(Value = "Part Time")]
        PartTime = 2,
    }
    public class EmployeeViewModel
    {

        public int ID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max Length 100 Char")]
        [MinLength(3, ErrorMessage = "min Length 2 Char")]

        public string Name { get; set; }


        [Range(22, 30)]
        public int Age { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        //public DateTime CreatioDate { get; set; } = DateTime.Now;
        //public bool IsDeleted { get; set; } = false;

        public IFormFile image { get; set; }
        public string ImageName { get; set; }
        public Sex Gender { get; set; }
        public EmployeeType EmpType { get; set; }

        public Department Department_Nav { get; set; }
        public int? Department_ID { get; set; }


    }
}
