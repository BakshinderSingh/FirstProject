using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>();
            //modelBuilder.Entity<Person>().HasKey(x => x.PersonKey);
            //more than one primary key
            //modelBuilder.Entity<Person>().HasKey(x =>new { x.PersonKey, x.FirstName });
            //Required
            modelBuilder.Entity<Address>().Property(x => x.City).IsRequired();
            // modelBuilder.Ignore<Address>();
            // modelBuilder.Entity<Person>().ToTable("Person");
            //modelBuilder.Entity<Address>().ToTable("Address");


            //Index on person first name
            modelBuilder.Entity<Person>().HasIndex(x => x.FirstName);
            modelBuilder.Entity<Person>().HasIndex(x => x.PersonKey).IsUnique();

            //Multiple columns Index
            modelBuilder.Entity<Person>().HasIndex(x => new { x.PersonKey,x.FirstName})

        }
    }
}
