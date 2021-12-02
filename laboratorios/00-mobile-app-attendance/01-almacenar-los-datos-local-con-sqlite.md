# Almacenar localmente los datos con Sqlite.NET

Vamos a crear un **repositorio con SQlite** que nos permitirá registrar a los estudiantes, laboratorios y la asistencia.

## Definiendo una entidad SQLite.NET
Vamos a crear una clase modelo que es usada para definir el esquema de la base de datos.

1. Agregar un folder llamado **Models** en el proyecto *Attendance*
2. En el folder **Models** creareamos una **clase** llamada **Student**. Nos aseguramos qe sea marcada como *public*

```c#
namespace Attendance.Models
{
    public class Student
    {
    }
}
```

3. Agrega las siguientes **propiedades** a la clase *Student*

```c#
public class Student
{
     public int Id { get; set; }
     public string StudentName { get; set; }
     public string LastName { get; set; }
     public string StudentCode { get; set; }
}
```
4. En el folder **Models** crea una clase llamada **Laboratory**. Asegúrate de marcar la clase como *public*
```c#
namespace Attendance.Models
{
    public class Laboratory
    {
    }
}
```

5. Agrega las siguientes propiedades a la clase **Laboratory**
```c#
 public class Laboratory
    {
        public int Id { get; set; }
        public string LabName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
```
6. En el folder **Models** crea la clase **Attendance**. Asegúrate que esté marcada como *public*

```c#
namespace Attendance.Models
{
    public class Attendance
    {
    }
}
```
7. Define las siguientes propiedades a la clase **Attendance**

```c#
public int Id { get; set; }
public Student Student { get; set; }
public int StudentId { get; set; }
public Laboratory Lab { get; set; }
public int LaboratoryId { get; set; }
public DateTime AttendanceDate { get; set; }
public bool Attendanced { get; set; }
```
