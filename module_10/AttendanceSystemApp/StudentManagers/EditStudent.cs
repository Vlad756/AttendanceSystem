using AttendanceSystemApp.AttendanceSystemExceptions;
using AttendanceSystemApp.Interfaces;
using AttendanceSystemApp.Models;
using AttendanceSystemApp.StudentManagers;
using AttendanceSystemPersistence;
using AutoMapper;
using FluentValidation;
using LogAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceSystemApp.Managers
{
    public class EditStudent
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
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var student = await _context.Students.FindAsync(request.Student.Id);

                if (student == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(student));

                    LogMethods.LogError("Student not found", ex, request.Logger);

                    throw ex;
                }

                // activity.Title = request.Activity.Title ?? activity.Title;
                _mapper.Map(request.Student, student);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }

    }
}
