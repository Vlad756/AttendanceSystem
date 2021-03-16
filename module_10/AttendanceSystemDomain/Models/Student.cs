using AttendanceSystemApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.Models
{
    public class Student : IDataObject
    {
        public Guid Id { get ; set ; }
        public string Name { get; set; }
        public HomeWork HomeWork { get; set; }
        public double Grade { get; set; }
        public int LecturesCount { get; set; }
        public int SkipCount { get; set; }
    }
}
