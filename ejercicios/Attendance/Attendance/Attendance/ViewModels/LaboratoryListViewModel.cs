using Attendance.Data;
using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Attendance.ViewModels
{
    public class LaboratoryListViewModel : INotifyPropertyChanged
    {
        private readonly LabRepository repository;

        public LaboratoryListViewModel(LabRepository repository)
        {
            try
            {
                this.repository = repository;
                Laboratories = new List<Laboratory>
                    (repository.GetLaboratoriesWithStudents());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error");
                throw;
            }


            ShowStudentsOfLaboratorySelectedCommand = new Command(ShowStudents);
            Students = new ObservableCollection<Student>();
            RefreshLaboratoriesCommand = new Command(() => RefreshLabs(), CanExecute());

        }


        #region Properties
        private bool IsRefreshing_BF;

        public bool IsRefreshing
        {
            get { return IsRefreshing_BF; }
            set
            {
                IsRefreshing_BF = value;
                OnPropertyChanged();
            }
        }

        private List<Laboratory> Laboratories_BF;

        public List<Laboratory> Laboratories
        {
            get { return Laboratories_BF; }
            set
            {
                Laboratories_BF = value;
                OnPropertyChanged();
            }
        }

        public Laboratory SelectedLaboratory { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        #endregion

        #region Commands
        public Command RefreshLaboratoriesCommand { get; set; }
        public Command ShowStudentsOfLaboratorySelectedCommand { get; set; }
        #endregion

        #region Private Methods
        private void ShowStudents()
        {
            Students.Clear();

            foreach (Student s in SelectedLaboratory.Students)
            {
                Students.Add(s);
            }
        }

        private void RefreshLabs()
        {
            IsRefreshing = true;
            Laboratories = repository.GetLaboratoriesWithStudents();
            IsRefreshing = false;
        }
        private Func<bool> CanExecute()
        {
            return new Func<bool>(() => !IsRefreshing);
        }
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
