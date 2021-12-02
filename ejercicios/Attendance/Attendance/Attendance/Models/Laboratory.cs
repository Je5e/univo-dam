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

        public DateTime CreatedAt { get; set; }

        [OneToMany]
        public List<Student> Students { get; set; }

        [OneToMany]
        public List<Attendance> Attendances { get; set; }
    }
}
