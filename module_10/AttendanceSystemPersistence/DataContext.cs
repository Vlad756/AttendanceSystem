using AttendanceSystemApp.Interfaces;
using AttendanceSystemApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemPersistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
    }
}