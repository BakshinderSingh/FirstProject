using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Entities
{
    public class Person
    {
        [Key]
        public int PersonKey { get; set; }


        public string FirstName { get; set; }

        [ConcurrencyCheck ]
        public string LastName { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [NotMapped]
        public List<Address> Address { get; set; }
    }
}
