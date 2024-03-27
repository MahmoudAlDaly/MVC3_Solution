using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DAL.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="code is reqyired !!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "name is reqyired !!")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

    }
}
