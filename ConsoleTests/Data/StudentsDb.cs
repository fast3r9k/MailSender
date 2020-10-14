using ConsoleTests.Data.Entityes;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTests.Data
{
    public class StudentsDb : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }
        public StudentsDb(DbContextOptions<StudentsDb> options) : base(options)
        {

        }
    }
}
