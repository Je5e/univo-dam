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

            AddStudentCommand = new Command(AddStudentToList, canExecuteAddStudentToList);
            SaveLaboratoryCommand = new Command(SaveNewLaboratory, canExecute);

            Students = new ObservableCollection<Student>();
        }

        private bool canExecuteAddStudentToList(object arg) => !string.IsNullOrEmpty(TextStudentName) && !string.IsNullOrEmpty(TextLastName) && !string.IsNullOrEmpty(TextStudentCode);

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

        private void AddStudentToList(object obj)
        {
            Students.Add(new Student // TODO: detalle.
            {
                StudentName = TextStudentName,
                LastName = TextLastName,
                StudentCode = TextStudentCode
            });
            TextStudentName = string.Empty;
            TextLastName = string.Empty;
            TextStudentCode = string.Empty;

        }

        #region Properties

        private string Status_BF;


        private void CleanValues()
        {
            LabName = string.Empty;
            TextStudentName = string.Empty;
            TextLastName = string.Empty;
            TextStudentCode = string.Empty;
            Students.Clear();
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

        private ObservableCollection<Student> Students_BF;

        public ObservableCollection<Student> Students
        {
            get { return Students_BF; }
            set { Students_BF = value;OnPropertyChanged(); }
        }

        private string LabName_BF;

        public string LabName
        {
            get { return LabName_BF; }
            set { LabName_BF = value; OnPropertyChanged(); SaveLaboratoryCommand.ChangeCanExecute(); }
        }

        private string TextStudentName_BF;

        public string TextStudentName
        {
            get { return TextStudentName_BF; }
            set { TextStudentName_BF = value;OnPropertyChanged(); AddStudentCommand.ChangeCanExecute(); }
        }

        private string TextLastName_BF;

        public string TextLastName
        {
            get { return TextLastName_BF; }
            set { TextLastName_BF = value; OnPropertyChanged(); AddStudentCommand.ChangeCanExecute(); }
        }


        private string TextStudentCode_BF;

        public string TextStudentCode
        {
            get { return TextStudentCode_BF; }
            set { TextStudentCode_BF = value; OnPropertyChanged(); AddStudentCommand.ChangeCanExecute(); }
        }

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
