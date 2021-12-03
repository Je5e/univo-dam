using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Attendance.Models
{
    [Table("Students")]
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string StudentName { get; set; }

        [MaxLength(250),Unique]
        public string LastName { get; set; }

        public string StudentCode { get; set; }

        [ForeignKey(typeof(Laboratory))]
        public int LaboratoryId { get; set; }

        [ManyToOne]
        public Laboratory Laboratory { get; set; }

        [ManyToMany(typeof(StudentAttendance))]
        public List<Attendance> AttendancesAt { get; set; }
    }
}
