using AttendanceSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemPersistence
{
    public class StudentGenerator
    {
        public static ICollection<Student> GenerateStudents()
        {
            var students = new List<Student>
            {
                new Student
                {
                    Name = "Student 1"
                },
                new Student
                {
                    Name = "Student 2"
                },
                new Student
                {
                    Name = "Student 3"
                },
                new Student
                {
                    Name = "Student 4"
                },
                new Student
                {
                    Name = "Student 5"
                },
                new Student
                {
                    Name = "Student 6"
                },
                new Student
                {
                    Name = "Student 7"
                },
                new Student
                {
                    Name = "Student 8"
                },
                new Student
                {
                    Name = "Student 9"
                },          
                new Student 
                {           
                    Name = "Student 10"
                }
            };

            return students;
        }
    }
}
