using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Attendance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewLaboratoryPage : ContentPage
    {
        public AddNewLaboratoryPage()
        {
            InitializeComponent();

            BindingContext =
             new ViewModels.AddNewLaboratoryViewModel
             (new Data.LabRepository
             (Path.Combine(Environment.GetFolderPath
             (Environment.SpecialFolder.LocalApplicationData), "AttendanceDb.db")));
        }
    }
}