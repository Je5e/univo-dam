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
    public class AddNewLaboratoryViewModel : INotifyPropertyChanged
    {
        LabRepository Repository;
        public AddNewLaboratoryViewModel(LabRepository repository)
        {
            Repository = repository;

            AddStudentCommand = new Command(AddStudentToList);
            SaveLaboratoryCommand = new Command(SaveNewLaboratory, canExecute);

            Students = new ObservableCollection<Student>();
        }

        private bool canExecute(object obj) => !string.IsNullOrEmpty(LabName);


        private void SaveNewLaboratory(object obj)
        {
            Laboratory C = new Laboratory
            {
                LabName = LabName,
                CreatedAt = DateTime.Now.ToShortDateString(),
            };
            C.Students = new List<Student>(Students);

            Repository.AddNewLab(C);
            Status = Repository.MessageStatus;
            CleanValues();

        }

        private void AddStudentToList()
        {
            Students.Add(new Student // TODO: detalle.
            {
                StudentName = TextStudentName,
                LastName = TextLastName,
                StudentCode = TextStudentCode
            });
        }

        #region Properties

        private string Status_BF;


        private void CleanValues()
        {
            LabName = string.Empty;
            TextLastName = string.Empty;
            TextLastName = string.Empty;
            TextStudentCode = string.Empty;
        }
        public string Status
        {
            get { return Status_BF; }
            set
            {
                Status_BF = value;
                OnPropertyChanged();

            }
        }

        public ObservableCollection<Student> Students { get; set; }
        private string LabName_BF;

        public string LabName
        {
            get { return LabName_BF; }
            set { LabName_BF = value; OnPropertyChanged(); SaveLaboratoryCommand.ChangeCanExecute(); }
        }

        public string TextStudentName { get; set; }
        public string TextLastName { get; set; }

        public string TextStudentCode { get; set; }
        #endregion

        #region Commands
        public Command AddStudentCommand { get; set; }
        public Command SaveLaboratoryCommand { get; }
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
