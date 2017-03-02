using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Lab.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        [Required(ErrorMessage = "Please provide a first name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 25 characters long")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide a last name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 25 characters long")]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get
            {
                int years = DateTime.Now.Year - BirthDate.Year;
                if ((BirthDate.Month > DateTime.Now.Month) || (BirthDate.Month == DateTime.Now.Month && BirthDate.Day > DateTime.Now.Day))
                {
                    years--;
                }
                return years;
            }
        }

    }
}
