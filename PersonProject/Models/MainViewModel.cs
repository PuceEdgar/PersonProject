using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonProject.Models
{
    public class MainViewModel
    {

        public List<Person> allPersons { get; set; }
        public List<Address> allAddresses { get; set; }
        public List<Person_Address> allPerson_Adress { get; set; }


        public Person person { get; set; }
        public Address address { get; set; }
        public Person_Address person_Address { get; set; }


        public PersonModel personModel { get; set; }
        public IEnumerable<PersonProject.Models.Person> iePerson { get; set; }
        public SpouseViewModel spouseModel { get; set; }

        //public static implicit operator MainViewModel(SpouseViewModel v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}