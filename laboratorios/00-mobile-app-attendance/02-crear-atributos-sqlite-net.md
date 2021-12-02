# Agregar atributos SQLite-net a las clases

Ahora que ya hemos creado el modelo, agreguemos algunos atributos para ayudar al **SQLite.NET** a mapear una clase a una tabla

1. Agreguemos una directiva **using** para el namespace **SQLite** en nuestro archivo **Student.cs**. La directiva permite utilizar los atributos de **SQLite.NET**

```c#
using SQLite;
```

2. Anotemos la clase **Student** con el atributo **[Table]**, y epecificamos el nombre como **Students**
3. Especifiquemos la propiedad **Id** como llave primaria. Anotemosla con el atributo **[PrimaryKey]** y el atributo **[AutoIncrement]**
4. Para agregar restricciones a los datos, agreguemos anotaciones a la propiedad **Name**. Especifica su **[MaxLength]** como 250. Especifica que cada valor en la columna debe ser **[Unique]**

```c#
 [Table("Students")]
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string StudentName { get; set; }

        [MaxLength(250),Unique]
        public string LastName { get; set; }

        public string StudentCode { get; set; }

        [ForeignKey(typeof(Laboratory))]
        public int LaboratoryId { get; set; }

        [ManyToOne]
        public Laboratory Laboratory { get; set; }

        [ManyToMany(typeof(StudentAttendance))]
        public List<Attendance> AttendancesAt{ get; set; }
    }
```
5. La tabla **Laboratory** queda de la siguiente manera.

```c#
 [Table("Laboratories")]
    public class Laboratory
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string LabName { get; set; }

        public DateTime CreatedAt { get; set; }

        [OneToMany]
        public List<Student> Students { get; set; }

        [OneToMany]
        public List<Attendance> Attendances { get; set; }
    }
```
6. La tabla **Attendance**

```c#
 [Table("Attendances")]
    public class Attendance
    {
        public int Id { get; set; }

        public DateTime AttendanceDate { get; set; }

        [ForeignKey(typeof(Laboratory))]
        public int LaboratoryId { get; set; }

        [ManyToOne]
        public Laboratory Laboratory { get; set; }

        [ManyToMany(typeof(StudentAttendance))]
        public List<Student> Students { get; set; }
    }
```
7. Por último la tabla **StudentAttendance**

```c#
[Table("StudentsAttendance")]
    public class StudentAttendance
    {
        [ForeignKey(typeof(Student))]
        public int StudentId { get; set; }

        [ForeignKey(typeof(Attendance))]
        public int AttendancesId { get; set; }
    }
```
De esta manera quedarian nuestras entidades decoradas y listas para trabajar y persistir datos a la base de datos.

