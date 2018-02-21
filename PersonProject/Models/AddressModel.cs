using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonProject.Models
{
    public class AddressModel
    {
        public long Id { get; set; }
        public string AddressString { get; set; }
        public bool IsPrimary { get; set; }
    }
}