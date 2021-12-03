using Attendance.Data;
using Attendance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Attendance.ViewModels
{
    public class LaboratoryListViewModel
    {
        private readonly LabRepository repository;

        public LaboratoryListViewModel(LabRepository repository)
        {
            try
            {
                this.repository = repository;
                Laboratories = new ObservableCollection<Laboratory>
                    (repository.GetLaboratoriesWithStudents());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error");
                throw;
            }
           

            ShowStudentsOfLaboratorySelectedCommand = new Command(ShowStudents);
            Students = new ObservableCollection<Student>();
            RefreshLaboratoriesCommand = new Command(()=>RefreshLabs() , CanExecute());

           
        }

        private Func<bool> CanExecute()
        {
            return new Func<bool>(() => !IsRefreshing);
        }



        #region Properties
        public bool IsRefreshing { get; set; }
        public ObservableCollection<Laboratory> Laboratories { get; set; }
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
            Laboratories = new ObservableCollection<Laboratory>
                    (repository.GetLaboratoriesWithStudents());
            IsRefreshing = false;
        }
        #endregion
    }
}
