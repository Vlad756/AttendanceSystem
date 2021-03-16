using AttendanceSystemApp.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.LectureManager
{
    public class LectureValidator : AbstractValidator<Lecture>
    {
        public LectureValidator()
        {
            RuleFor(x => x.CourseName).NotNull();
            RuleFor(x => x.CourseName).NotEmpty();
        }
    }
}
