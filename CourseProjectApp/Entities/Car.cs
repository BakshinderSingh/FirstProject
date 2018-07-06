using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Entities
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CarId { get; set; }
        public string CarColor { get; set; }
        public int Year { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime MyDate { get; set; }
    }
}
