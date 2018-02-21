using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonProject.Models
{
    public class PersonModel
    {

        

        public int person_id { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z]+)[-' ]?([a-zA-Z]+)", ErrorMessage = "Please enter valid First Name")]
        public string first_name { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression("([a-zA-Z]+)[-' ]?([a-zA-Z]+)", ErrorMessage = "Please enter valid Last Name")]
        public string last_name { get; set; }
        [Required]
        public System.DateTime dob { get; set; }
        public string single_married { get; set; }
        public int? spouse_id { get; set; }
        //[Required (ErrorMessage = "Primary address can not be empty!")]
        public string primary_address { get; set; }
        
        [Required]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid Number")]
        public string phone_number { get; set; }
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid Number")]
        public string phone_number_2 { get; set; }


        public int age
        {
            get
            {

                DateTime now = DateTime.Today;
                int age = now.Year - dob.Year;
                return age;

            }
        }
               
    
        //[ForeignKey("spouse_id")]
        public Person Spouse { get; set; }
        public string full_name { get
            {
               return first_name + " " + last_name + " (" + age + ")";
            } }
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Primary address must be filled!")]
        //public bool isPrimany { get; set; }

        public Person person { get; set; }
        public Address address { get; set; }
        public List<Address> allAddresses { get; set; }
    }
}