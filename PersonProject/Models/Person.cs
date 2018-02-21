//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.Person_Address = new HashSet<Person_Address>();
            this.Persons1 = new HashSet<Person>();
        }
    
        public int person_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public System.DateTime dob { get; set; }
        public string single_married { get; set; }
        public Nullable<int> spouse_id { get; set; }
        public string primary_address { get; set; }
        public string phone_number { get; set; }
        public string phone_number_2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person_Address> Person_Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> Persons1 { get; set; }
        public virtual Person Spouse { get; set; }

        public int age
        {
            get
            {

                DateTime now = DateTime.Today;
                int age = now.Year - dob.Year;
                return age;

            }
        }

        public string full_name
        {
            get
            {
                return first_name + " " + last_name + " (" + age + ")";
            }
        }
    }
}