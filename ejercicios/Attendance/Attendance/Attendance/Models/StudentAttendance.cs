using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace Attendance.Models
{
    [Table("StudentsAttendance")]
   public class StudentAttendance
    {
        [ForeignKey(typeof(Student))]
        public int StudentId { get; set; }

        [ForeignKey(typeof(Attendance))]
        public int AttendancesId { get; set; }
    }
}
