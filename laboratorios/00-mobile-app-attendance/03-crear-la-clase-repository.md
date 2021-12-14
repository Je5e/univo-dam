
# Agregar las clases Repository para nuestra aplicación

El patrón de diseño **Repository** nos permite abstraer la lógica para acceder a los datos, de tal manera que nuestra aplicación sea ignorante y desconozca el origen de los datos con los que trabaja.

Para implementar el patrón Repository realiza lo siguiente:

1. Agrega un nuevo folder en el proyecto, nombralo **Data**.
2. Dentro de **Data** agrega una nueva clase, nombrala **StudentRepository**,esta clase repository encapsulará la lógica de acceso a datos de la clase modelo *Student*

```c#
 public class StudentRepository
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public StudentRepository(string dbPath)
        {
            /*
             * Creamos las tablas de la base de datos
             */
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Models.Student>();
            conn.CreateTable<Models.Laboratory>();
            conn.CreateTable<Models.Attendance>();
            conn.CreateTable<Models.StudentAttendance>();
        }

        public void AddNewStudent(Student newStudent)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(newStudent.StudentName))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Models.Student
                {
                    StudentName = newStudent.StudentName,
                    LastName = newStudent.LastName,
                    StudentCode = newStudent.StudentCode,

                });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, newStudent.StudentName);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", newStudent.StudentName, ex.Message);
            }
        }

        public List<Models.Student> GetAllStudents()
        {
            try
            {
                return conn.Table<Models.Student>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Student>();
        }
    }
```

3. Agregaremos otro repository **LabRepository.cs**

```c#
 public class LabRepository
    {
        public string MessageStatus { get; set; }
        SQLiteConnection conn;

        public LabRepository(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Student>();
            conn.CreateTable<Laboratory>();
        }

        // TODO: Este método debe guardar objetos con hijos
        public void AddNewLab(Laboratory newLab)
        {
            try
            {

                conn.InsertWithChildren(newLab); //
                MessageStatus =
                    $"Registro ingresado. Lab Id: {newLab.Id}, Name: {newLab.LabName}";
            }

            catch (Exception ex)
            {

                MessageStatus = $"Error al guardar registro. Error: {ex.Message}";
            }
        }

        public List<Laboratory> GetAll()
        {
            return conn.Table<Laboratory>().ToList();
        }

        public List<Laboratory> GetLaboratoriesWithStudents()
        {
            return conn.GetAllWithChildren<Laboratory>();
        }
    }
 
```    
