using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Entities
{
    public static class Initializer
    {
        public static void InitializeContext(ProfileContext context)
        {
            context.Database.EnsureCreated();

            //if (context.Individuals.Any())
            //{
            //    return;
            //}

            var individual = new Individual
            {
                FullName = "Carson1",
                DateOfBirth = DateTime.Now.AddYears(-23),
                Address = "4437 Texas Street",
                AspNetUserId = "9e40938f-6269-459a-b3ef-d4cd4a318a3c",
                State = "TX",
                City = "Allen",
                ZipCode = "75372"
            };
            context.Individuals.Add(individual);


            var organization = new Organisation
            {
                BuisnessName = "IT Empire",
                HireDate = DateTime.Now,
                Address = "2232 Dllaa Street",
                AspNetUserId = "9e40938f - 6269 - 459a - b3ef - d4cd4a318a3c",
                State = "TX",
                Profession = "Developer",
                City = "Allen",
                ZipCode = "28629"
            };
            context.Organisations.Add(organization);

            var hobby = new Hobby
            {
                HobbyId = Guid.NewGuid(),
                Name = "Excercise",
                Rating = 5,
                AspNetUserId = "9e40938f-6269-459a-b3ef-d4cd4a318a3c"
            };

            context.Hobbies.Add(hobby);
            context.SaveChanges();

        }
    }
}
