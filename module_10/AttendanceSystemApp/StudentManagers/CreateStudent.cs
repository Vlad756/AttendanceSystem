using AttendanceSystemApp.Interfaces;
using AttendanceSystemApp.Models;
using AttendanceSystemApp.StudentManagers;
using AttendanceSystemPersistence;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceSystemApp.Managers
{
    public class CreateStudent
    {
        public class Command : IRequest
        {
            public Student Student { get; set; }
            public ILogger Logger { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Student).SetValidator(new StudentValidator());
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Students.Add(request.Student);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
