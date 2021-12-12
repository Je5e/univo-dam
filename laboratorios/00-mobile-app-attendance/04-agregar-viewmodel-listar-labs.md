# En este ejercicio vamos a agregar un ViewModel para la view Lista de laboratorios.

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


