using CourseManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CourseManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();
            
            var exists = db.Database.GetPendingMigrations().Any();

            if (exists)
            {

            };
            
            // InsertData();
            // InsertMassiveData();
            // GetData();
            UpdateData();
            CreateHostBuilder(args).Build().Run();
        }

        private static void InsertMassiveData()
        {
            var courseList = new[]
            {
                new Course
                {
                    Title = ".NET Core",
                    Duration = 10,
                    Status = "Active"
                },
                new Course
                {
                    Title = ".NET Core",
                    Duration = 10,
                    Status = "Active"
                }
            };

            using var db = new Data.ApplicationContext();
            db.Set<Course>().AddRange(courseList);

            var registries = db.SaveChanges();
            Console.WriteLine($"All Registry(s): {registries}");
        }
 
        private static void GetData()
        {
            using var db = new Data.ApplicationContext();
            // var getBySyntax = (from c in db.Courses where c.Id > 0 select c).ToList();
            var getByMethod = db.Courses
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();

            foreach(var course in getByMethod)
            {
                Console.WriteLine($"Getting course: {course.Id}");
                db.Courses.FirstOrDefault(p => p.Id == course.Id);
            }
        }

        private static void UpdateData()
        {
            using var db = new Data.ApplicationContext();
            var course = db.Courses.FirstOrDefault(p => p.Id == 1);
            course.Name = "Course Changed Step 1";

            db.Courses.Update(course);
            db.SaveChanges();
        }

        private static void InsertData()
        {
            var course = new Course
            {
                Title = "Entity Framework Core",
                Duration = 10,
                Status = "Active"
            };

            using var db = new Data.ApplicationContext();
            db.Courses.Add(course);
            db.Set<Course>().Add(course);
            db.Entry(course).State = EntityState.Added;
            db.Add(course);

            var registries = db.SaveChanges();
            Console.WriteLine($"All registry(s): {registries}");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
