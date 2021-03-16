using AttendanceSystemApp.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.HomeWorkManager
{
    public class HomeWorkValidator : AbstractValidator<HomeWork>
    {
        public HomeWorkValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).NotNull();
        }
    }
}
