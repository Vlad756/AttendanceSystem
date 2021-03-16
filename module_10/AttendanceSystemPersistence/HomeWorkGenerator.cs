using AttendanceSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemPersistence
{
    public class HomeWorkGenerator
    {
        public static ICollection<HomeWork> GenerateHomeWorks()
        {
            var homeWorks = new List<HomeWork>
            {
                new HomeWork
                {
                    Title = "HomeWork 1"
                },
                new HomeWork
                {
                    Title = "HomeWork 2"
                },
                new HomeWork
                {
                    Title = "HomeWork 3"
                },
                new HomeWork
                {
                    Title = "HomeWork 4"
                },
                new HomeWork
                {
                    Title = "HomeWork 5"
                },
                new HomeWork
                {
                    Title = "HomeWork 6"
                },
                new HomeWork
                {
                    Title = "HomeWork 7"
                },
                new HomeWork
                {
                    Title = "HomeWork 8"
                },
                new HomeWork
                {
                    Title = "HomeWork 9"
                },
                new HomeWork
                {
                    Title = "HomeWork 10"
                }
            };

            return homeWorks;
        }
    }
}
