using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourseProjectApp.Entities
{
    public class Organisation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid OrganisationId { get; set; }

        [StringLength(50)]
        [Required]
        public string BuisnessName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Profession { get; set; }
        [ForeignKey("AspNetUsers")]
        public string AspNetUserId { get; set; }
    }
}
