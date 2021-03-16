using AttendanceSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemPersistence
{
    public class LecturerGenerator
    {
        public static ICollection<Lecturer> GenerateLecturers()
        {
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Name = "Lecturer 1"
                },
                new Lecturer
                {
                    Name = "Lecturer 2"
                },
                new Lecturer
                {
                    Name = "Lecturer 3"
                }
            };

            return lecturers;
        }
    }
}
