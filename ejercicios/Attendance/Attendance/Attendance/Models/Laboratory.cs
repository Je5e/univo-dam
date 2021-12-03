using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace Attendance.Models
{
    [Table("Laboratories")]
    public class Laboratory
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string LabName { get; set; }

        public string CreatedAt { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert |
            CascadeOperation.CascadeDelete)]
        public List<Student> Students { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert |
            CascadeOperation.CascadeDelete)]
        public List<Attendance> Attendances { get; set; } 
    }
}
