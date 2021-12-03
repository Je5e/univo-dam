# Agregar El viewModel AddNewLaboratoryViewModel


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
