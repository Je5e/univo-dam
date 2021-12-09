using Attendance.Data;
using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Attendance.ViewModels
{
    public class HistocialViewModel
    {
        private readonly LabRepository repository;

        public HistocialViewModel(LabRepository repository)
        {
            this.repository = repository;

            Labs = new List<Laboratory>(repository.GetLaboratoriesWithStudents());
            Attendances = new ObservableCollection<Models.Attendance>();
            ShowAttendancesOfLaboratorySelectedCommand = new Command(ShowAttendances);
            ShowStudentsCommand = new Command(ShowStudents);
            Students = new ObservableCollection<RegisterAttendanceViewModel>();
        }

        private void ShowStudents(object obj)
        {
            Students.Clear();
            var Attendance = repository.GetAttendanceWithChildren(SelectedAttendance.Id);
            foreach (var student in SelectedLaboratory.Students)
            {
                var r = Attendance.Students.Exists(s => s.Id == student.Id);
                Students.Add(new RegisterAttendanceViewModel
                {
                    Student = student,
                    On = r
                });
            }
        }

        private void ShowAttendances(object obj)
        {
            Attendances.Clear();
            foreach (var at in SelectedLaboratory.Attendances)
            {
                Attendances.Add(at);
            }
        }

        public Laboratory SelectedLaboratory { get; set; }
        public Models.Attendance SelectedAttendance { get; set; }
        public Command ShowStudentsCommand { get; set; }
        public List<Laboratory> Labs { get; set; }

        public ObservableCollection<RegisterAttendanceViewModel> Students { get; set; }
        public ObservableCollection<Models.Attendance> Attendances { get; set; }
        public Command ShowAttendancesOfLaboratorySelectedCommand { get; set; }
    }
}
