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
            conn.CreateTable<Models.Student>();
            conn.CreateTable<Models.Laboratory>();
            conn.CreateTable<Models.Attendance>();
            conn.CreateTable<Models.StudentAttendance>();
        }

        // TODO: Este método debe guardar objetos con hijos
        public void AddNewLab(Laboratory newLab)
        {
            try
            {

                conn.InsertWithChildren(newLab, recursive: true); //
                MessageStatus =
                    $"Registro ingresado. Lab Id: {newLab.Id}, Name: {newLab.LabName}";
            }

            catch (Exception ex)
            {

                MessageStatus = $"Error al guardar registro. Error: {ex.Message}";
            }
        }

        //Crear una nueva asistencia.
        public void AddNewAttendance(Models.Attendance attendance)
        {
            try
            {

                conn.InsertWithChildren(attendance, recursive: true); //
                MessageStatus =
                    $"Registro ingresado. Attendance Id: {attendance.Id}, Date: {attendance.AttendanceDate}";
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void UpdateLaboratoryWithChildren(Laboratory toUpdate)
        {
            try
            {

                conn.UpdateWithChildren(toUpdate); //
                MessageStatus =
                    $"Registro Actualizado. Laboratory Id: {toUpdate.Id}, Date: {toUpdate.LabName}";
            }

            catch (Exception ex)
            {

                MessageStatus = $"Error al guardar registro. Error: {ex.Message}";
            }
        }

        //Registrar la asistencia.
        public void UpdateAttendanceWithChildren(Models.Attendance a)
        {
            try
            {

                conn.UpdateWithChildren(a); //
                MessageStatus =
                    $"Registro Actualizado. Attendance Id: {a.Id}, Date: {a.AttendanceDate}";
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

        public Models.Attendance GetAttendanceWithChildren(int id)
        {
            return conn.GetWithChildren<Models.Attendance>(id);
        }

        public List<Laboratory> GetLaboratoriesWithStudents()
        {
            return conn.GetAllWithChildren<Laboratory>();
        }
    }
}

