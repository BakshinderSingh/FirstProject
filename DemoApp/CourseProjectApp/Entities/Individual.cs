using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProjectApp.Entities
{
    public class Individual
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid IndividualId { get; set; }

        [StringLength(50)]
        [Required]
        public string FullName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd",ApplyFormatInEditMode=true)]
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey("AspNetUsers")]
        public string AspNetUserId { get; set; }

    }
}
