using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonProject.Models
{
    public class PersonModel2
    {

        public PersonModel2(Person person)
        {
            // TODO: fill
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<string> PhoneNumbers { get; set; }
        public IEnumerable<AddressModel> Addresses { get; set; }
    }
}