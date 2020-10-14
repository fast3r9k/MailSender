using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTests.Data
{
    class StudentsDbContextFactory : IDesignTimeDbContextFactory<StudentsDb>
    {
        public StudentsDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentsDb>();
            const string connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentsDb;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connection_str);

            return new StudentsDb(optionsBuilder.Options);
        }
        
    }
}
