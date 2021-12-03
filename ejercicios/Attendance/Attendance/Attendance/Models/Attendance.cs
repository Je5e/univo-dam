using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Attendance.Models
{
    [Table("Attendances")]
    public class Attendance
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public DateTime AttendanceDate { get; set; }

        [ForeignKey(typeof(Laboratory))]
        public int LaboratoryId { get; set; }

        [ManyToOne]
        public Laboratory Laboratory { get; set; }

        [ManyToMany(typeof(StudentAttendance))]
        public List<Student> Students { get; set; } 
    }
}
