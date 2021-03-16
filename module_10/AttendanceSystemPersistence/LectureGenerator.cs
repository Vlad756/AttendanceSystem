using AttendanceSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemPersistence
{
    public class LectureGenerator
    {
        public static ICollection<Lecture> GenerateLectures()
        {
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    CourseName = "Lecture 1"
                },
                new Lecture
                {
                    CourseName = "Lecture 2"
                },
                new Lecture
                {
                    CourseName = "Lecture 3"
                }
            };

            return lectures;
        }
    }
}
