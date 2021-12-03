using Attendance.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Data
{
    public class LabRepository
    {
        public string MessageStatus { get; set; }
        SQLiteConnection conn;

        public LabRepository(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Student>();
            conn.CreateTable<Laboratory>();
        }

        // TODO: Este método debe guardar objetos con hijos
        public void AddNewLab(Laboratory newLab)
        {
            try
            {

                conn.InsertWithChildren(newLab); //
                MessageStatus =
                    $"Registro ingresado. Lab Id: {newLab.Id}, Name: {newLab.LabName}";
            }

            catch (Exception ex)
            {

                MessageStatus = $"Error al guardar registro. Error: {ex.Message}";
            }
        }

        public List<Laboratory> GetAll()
        {
            return conn.Table<Laboratory>().ToList();
        }

        public List<Laboratory> GetLaboratoriesWithStudents()
        {
            return conn.GetAllWithChildren<Laboratory>();
        }
    }
}

