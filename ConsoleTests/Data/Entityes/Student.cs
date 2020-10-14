using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleTests.Data.Entityes
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }

    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }

    public class Student : NamedEntity
    {
        [Required, MaxLength(20)]
        public string Surname { get; set; }

        [MaxLength(20)]
        public string Patronymic { get; set; }

        public virtual Group Group { get; set; }

        //public string Description { get; set; }
    }

    public class Group:NamedEntity
    {
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
