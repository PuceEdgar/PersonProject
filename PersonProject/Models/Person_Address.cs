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
    
    public partial class Person_Address
    {
        public int person_id { get; set; }
        public int address_id { get; set; }
        public int record_id { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
