using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Entities
{
    public class ProfileContext:IdentityDbContext<ApplicationUser>
    {

        public ProfileContext(DbContextOptions<ProfileContext> options):base(options)
        {
                
        }

        public DbSet<Individual> Individuals { set; get; }

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }


    }
}
