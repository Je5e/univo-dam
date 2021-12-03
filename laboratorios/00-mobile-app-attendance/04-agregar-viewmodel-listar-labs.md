# En este ejercicio vamoa a agregar un ViewModel para la page Lista de laboratorios.

1. En la aplicación agrega una nueva carpeta llamada **ViewModels**

![ViewModel folder](https://user-images.githubusercontent.com/45072377/144541963-ba417bdd-8ab0-4984-9180-1b55e0ef81c8.png)

2. En la carpeta **ViewModels** agrega una clase y nombrala **LaboratoryListViewModel**

```c#
public class LaboratoryListViewModel
    {
        private readonly LabRepository repository;

        public LaboratoryListViewModel(LabRepository repository)
        {
            this.repository = repository;
            Laboratories = new List<Laboratory>
                (repository.GetLaboratoriesWithStudents());

            ShowStudentsOfLaboratorySelectedCommand = new Command(ShowStudents);
            Students = new ObservableCollection<Student>();
        }

        #region Properties
        public List<Laboratory> Laboratories { get; set; }
        public Laboratory SelectedLaboratory { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        #endregion

        #region Commands
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
        #endregion
    }
```

3. Vamos a agregar la vista a la que da soporte el viewmodel anterior.
4. En la carpeta **Views** agrega una **página de contenido** (Content Page), y asignale el nombre de **LaboratoryListPage**
5. Agrega el siguiente paquete Nuget **Xamarin.Forms.BehaviorsPack version, la que se observa en la imagen

![behaviorsPack](https://user-images.githubusercontent.com/45072377/144543286-1b852822-81d4-42f7-a5a1-ad0f7bd55f04.png)

6. En el archivo **LaboratoryListPage.xaml** agrega el siguiente marcado xaml.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:behaviors="clr-namespace:Xamarin.Forms.BehaviorsPack;assembly=Xamarin.Forms.BehaviorsPack"
             x:Class="Attendance.Views.LaboratoriesPage">
    <ContentPage.Content>
        <StackLayout>
            <Label Text="LISTA DE LABORATORIOS:"
                   FontSize="Large"
                   FontAttributes="Bold"/>
            <ListView ItemsSource="{Binding Laboratories}"
                      SelectedItem="{Binding SelectedLaboratory}">
                <ListView.Behaviors>
                    <behaviors:EventToCommandBehavior 
                        EventName="ItemSelected" 
                        Command="{Binding ShowStudentsOfLaboratorySelectedCommand}"/>
                </ListView.Behaviors>
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <TextCell Text="{Binding LabName}"
                                  Detail="{Binding Id}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
            <Label Text="Students:"
                   FontSize="Large"
                   FontAttributes="Bold"/>
            <ListView ItemsSource="{Binding Students}">

                <ListView.ItemTemplate>
                    <DataTemplate>
                        <TextCell Text="{Binding StudentName}"
                                  Detail="{Binding Id}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

