using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonProject.Models
{
    public class SpouseViewModel
    {
        public int person_id { get; set; }
        public int spouse_id { get; set; }
        public string first_name { get; set; }
        public Person person { get; set; }
        

    }
}