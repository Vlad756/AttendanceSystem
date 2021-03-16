using AttendanceSystemApp.Interfaces;
using System;
using System.Text;

namespace AttendanceSystemApp.Models
{
    public class HomeWork : IDataObject
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
