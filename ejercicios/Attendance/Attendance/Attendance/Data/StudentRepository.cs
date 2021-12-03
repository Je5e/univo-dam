using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using Attendance.Models;

namespace Attendance.Data
{
    public class StudentRepository
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public StudentRepository(string dbPath)
        {
            /*
             * Creamos las tablas de la base de datos
             */
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Models.Student>();
            conn.CreateTable<Models.Laboratory>();
            conn.CreateTable<Models.Attendance>();
            conn.CreateTable<Models.StudentAttendance>();
        }

        public void AddNewStudent(Student newStudent)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(newStudent.StudentName))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Models.Student
                {
                    StudentName = newStudent.StudentName,
                    LastName = newStudent.LastName,
                    StudentCode = newStudent.StudentCode,

                });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, newStudent.StudentName);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", newStudent.StudentName, ex.Message);
            }
        }

        public List<Models.Student> GetAllStudents()
        {
            try
            {
                return conn.Table<Models.Student>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Student>();
        }
    }
}
