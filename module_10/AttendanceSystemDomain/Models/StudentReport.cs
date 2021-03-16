using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemDomain.Models
{
    [Serializable]
    public class StudentReport
    {
        public string StudentName { get; set; }
        public double YearGrade { get; set; }
    }
}
