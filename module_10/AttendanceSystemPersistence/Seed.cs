using AttendanceSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemPersistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            var lectures = LectureGenerator.GenerateLectures();
            var homeWorks = HomeWorkGenerator.GenerateHomeWorks();
            var lecturers = LecturerGenerator.GenerateLecturers();
            var students = StudentGenerator.GenerateStudents();

            foreach (var item in students)
            {
                item.HomeWork = homeWorks.ElementAt(0);
            }

            foreach (var item in lectures)
            {
                item.Students = students;
            }

            foreach (var item in lecturers)
            {
                item.Lectures = lectures;
            }

            var testLecturer = new Lecturer { Lectures = new List<Lecture> { new Lecture { Students = new List<Student> { new Student { HomeWork = new HomeWork() } } } } } ;

            lecturers.Add(testLecturer);

            await context.HomeWorks.AddRangeAsync(homeWorks);
            await context.Lecturers.AddRangeAsync(lecturers);
            await context.Students.AddRangeAsync(students);
            await context.Lectures.AddRangeAsync(lectures);

            await context.SaveChangesAsync();
        }

        
    }
}
