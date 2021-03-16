using AttendanceSystemApp.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.StudentManagers
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
        }
    }
}
