using ConsoleTests.Data;
using ConsoleTests.Data.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentsDb;Integrated Security=True;";

            //var services_collection = new ServiceCollection();
            //services_collection.AddDbContext<StudentsDb>(options => options.UseSqlServer(connection_str));

            //var services = services_collection.BuildServiceProvider();
            //using (var db = services.GetRequiredService<StudentsDb>())
            //{

            //}

            using(var db = new StudentsDb(new DbContextOptionsBuilder<StudentsDb>().UseSqlServer(connection_str).Options))
            {
                await db.Database.EnsureCreatedAsync();

                var students_count = await db.Students.CountAsync();

                Console.WriteLine($"Число студентов в базе  = {students_count}");              
            }

            using (var db = new StudentsDb(new DbContextOptionsBuilder<StudentsDb>().UseSqlServer(connection_str).Options))
            {
                int k = 0;
                if (await db.Students.CountAsync() == 0)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        var group = new Group
                        {
                            Name = $"Группа {i}",
                            Description = $"Описание группы {i}",
                            Students = new List<Student>()
                        };

                        for (var j = 0; j < 10; j++)
                        {
                            var student = new Student
                            {
                                Name = $"Студент{k}",
                                Surname = $"Фамилия {k}",
                                Patronymic = $"Отчество{k}",
                                //Description = $"Описание студента{k}"
                            };
                            k++;
                            group.Students.Add(student);
                        }
                        db.Groups.Add(group);
                    }
                    await db.SaveChangesAsync();
                }
            }

            using (var db = new StudentsDb(new DbContextOptionsBuilder<StudentsDb>().UseSqlServer(connection_str).Options))
            {
                var students = await db.Students
                    .Include(s => s.Group)
                    .Where(s => s.Group.Name == "Группа 5").ToArrayAsync();
                foreach (var student in students)
                    Console.WriteLine($"[{student.Id}] {student.Name} {student.Group.Name}");
            }
                Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }
    }
}
