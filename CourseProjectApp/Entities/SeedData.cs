using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace CourseProjectApp.Entities
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if(context.Persons.Any())
            {
                return;
            }
            using (context)
            {                
                context.AddRange(new Person
                {
                    FirstName = "Bakhsi",
                    LastName = "Singh"
                }
                , new Person
                {
                    FirstName = "Johnny",
                    LastName = "Methew"
                });
                context.SaveChanges();
                context.AddRange(new Address
                {
                    PersonId = 1,
                    City = "Plano",
                    StreetName = "21Street",
                    ZipCode = 4646,
                    State = "TX"
                },
                new Address
                {
                    PersonId = 2,
                    City = "Allen",
                    StreetName = "21Street",
                    ZipCode = 4646,
                    State = "TX"
                });
                context.SaveChanges();
                context.Dispose();
            }
            
            //context.Dispose();

        }

    }
}
