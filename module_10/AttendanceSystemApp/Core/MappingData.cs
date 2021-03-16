using AttendanceSystemApp.Interfaces;
using AttendanceSystemApp.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.Core
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<Student, Student>();
            CreateMap<Lecturer, Lecturer>();
            CreateMap<Lecture, Lecture>();
            CreateMap<HomeWork, HomeWork>();
        }
    }
}
