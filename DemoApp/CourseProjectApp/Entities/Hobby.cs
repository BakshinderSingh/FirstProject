using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Entities
{
    public class Hobby
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Guid HobbyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Rating { get; set; }
        
        [ForeignKey("AspNetUsers")]
        public string AspNetUserId { get; set; }
    }
}
