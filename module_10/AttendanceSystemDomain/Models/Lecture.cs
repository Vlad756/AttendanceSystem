using AttendanceSystemApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceSystemApp.Models
{
    public class Lecture : IDataObject
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
