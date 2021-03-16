using AttendanceSystemApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.Models
{
    public class Lecturer : IDataObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
    }
}
 