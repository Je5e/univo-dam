# Agregar El viewModel AddNewLaboratoryViewModel

1. Crear la clase Viewmodel
2. 
```c#
 public class AddNewLaboratoryViewModel
    {
        LaboratoryRepository Repository;
        public AddNewLaboratoryViewModel(LaboratoryRepository repository)
        {
            Repository = repository;

            AddStudentCommand = new Command(AddStudentToList);
            SaveLaboratoryCommand = new Command(SaveNewLaboratory);

            Students = new ObservableCollection<Student>();
        }

        private void SaveNewLaboratory()
        {
            Laboratory C = new Laboratory
            {
                LabName = LabName,
                CreatedAt = DateTime.Now.ToShortDateString(),
            };
            C.Students = new List<Student>(Students);

            Repository.InsertNewLab(C);
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
        public ObservableCollection<Student> Students { get; set; }
        public string LabName { get; set; }
        public string TextStudentName { get; set; }
        public string TextLastName { get; set; }

        public string TextStudentCode { get; set; }
        #endregion

        #region Commands
        public Command AddStudentCommand { get; set; }
        public Command SaveLaboratoryCommand { get; set; }
        #endregion
    }
```    

2. La vista del viewModel

```xaml
<StackLayout>
            <Label Text="DATOS DE Laboratory:"
                   FontSize="Large"
                   FontAttributes="Bold"/>
            <StackLayout>
                <Label Text="Name:"/>
                <Entry Placeholder="Intro Laboratory name"
                       Text="{Binding LabName}"/>

                <!--DATOS DE LOS ESTUDIANTES-->
                <Label Text="DATOS DE ESTUDIANTE:"
                        FontSize="Large"
                   FontAttributes="Bold"/>
                <StackLayout Orientation="Horizontal">
                    <Label Text="Name:"/>
                    <Entry Placeholder="Name"
                           Text="{Binding TextStudentName}"/>
                </StackLayout>
                <StackLayout Orientation="Horizontal">
                    <Label Text="LastName:"/>
                    <Entry Placeholder="Apellido"
                           Text="{Binding TextLastName}"/>
                </StackLayout>
                <StackLayout Orientation="Horizontal">
                    <Label Text="Code:"/>
                    <Entry Placeholder="code"
                           Text="{Binding TextStudentCode}"/>
                </StackLayout>
                <StackLayout Orientation="Horizontal">
                    <Button Text="Add"
                            Command="{Binding AddStudentCommand}"/>
                </StackLayout>
            </StackLayout>
            <ListView ItemsSource="{Binding Students}">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <TextCell Text="{Binding StudentName}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>

            <Label Text="{Binding MessageStatus}"/>
            <Button Text="Guardar"
                    Command="{Binding SaveLaboratoryCommand}"/>
        </StackLayout>
```

-----------
```c#
  BindingContext =
            new ViewModels.AddNewLaboratoryViewModel
            (new Data.LaboratoryRepository
            (Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.LocalApplicationData), "AttendanceDb.db")));
```            
