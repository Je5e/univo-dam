using Attendance.Data;
using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Attendance.ViewModels
{
    public class RegisterAttendanceViewModel: INotifyPropertyChanged
    {
        public RegisterAttendanceViewModel()
        {

        }
        public RegisterAttendanceViewModel(LabRepository repository)
        {
            this.repository = repository;

            Laboratories = new List<Laboratory>(repository.GetLaboratoriesWithStudents());
            ShowStudentsOfLaboratorySelectedCommand = new Command(ShowStudents);
            RegisterAttendanceCommand = new Command(RegisterAttendance);
            Students = new ObservableCollection<RegisterAttendanceViewModel>();

        }

        private void RegisterAttendance(object obj)
        {
            // Crear el objeto Attendance
            Models.Attendance NewAttendance = new Models.Attendance
            {
                AttendanceDate = DateTime.Now.ToShortDateString()
            };
            try
            {
                repository.AddNewAttendance(NewAttendance);
                SelectedLaboratory.Attendances.Add(NewAttendance);
                repository.UpdateLaboratoryWithChildren(SelectedLaboratory);
                // Registro la asistencia.
                foreach (var attendacedStudent in Students)
                {
                    if (attendacedStudent.On)
                    {
                        NewAttendance.Students.Add(attendacedStudent.Student);
                    }
                }
                repository.UpdateAttendanceWithChildren(NewAttendance);
                StatusMessage = repository.MessageStatus;
            }
            catch (Exception ex)
            {

                StatusMessage = $"Error: {ex.Message}";

            }

          
           
        }

        private void ShowStudents(object obj)
        {
            Students.Clear();
            foreach (var student in SelectedLaboratory.Students)
            {
                Students.Add(
                    new RegisterAttendanceViewModel
                    {
                        Student = student
                    }); ;
            }
        }
        #region Properties
        public bool On { get; set; }
        public Student   Student { get; set; }

        public List<Laboratory> Laboratories { get; set; }

        private string StatusMessage_BF;

        public string StatusMessage
        {
            get { return StatusMessage_BF; }
            set { StatusMessage_BF = value;OnPropertyChanged(); }
        }



        private readonly LabRepository repository;

        public string FullName
        {
            get { return $"{Student.StudentName} {Student.LastName}"; }
           
        }


        public ObservableCollection<RegisterAttendanceViewModel> Students { get; set; }

        public Laboratory SelectedLaboratory { get; set; }
        public Command ShowStudentsOfLaboratorySelectedCommand { get; set; }
        public Command RegisterAttendanceCommand { get; set; }
        #endregion

        #region INotifyPropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
