using AttendanceSystemApp.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.LecturerManagers
{
    public class LecturerValidator : AbstractValidator<Lecturer>
    {
        public LecturerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
        }
    }
}
