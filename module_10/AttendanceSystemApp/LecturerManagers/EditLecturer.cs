using AttendanceSystemApp.AttendanceSystemExceptions;
using AttendanceSystemApp.Models;
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

namespace AttendanceSystemApp.LecturerManagers
{
    public class EditLecturer
    {
        public class Command : IRequest
        {
            public Lecturer Lecturer { get; set; }
            public ILogger Logger { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Lecturer).SetValidator(new LecturerValidator());
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
                var lecturer = await _context.Lecturers.FindAsync(request.Lecturer.Id);

                if (lecturer == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecturer));

                    LogMethods.LogError("Lecturer not found", ex, request.Logger);

                    throw ex;
                }

                // homework check
                foreach (var lecture in request.Lecturer.Lectures)
                {
                    foreach (var student in lecture.Students)
                    {
                        student.Grade *= student.LecturesCount;

                        student.LecturesCount++;

                        if (student.HomeWork != null)
                        {
                            student.Grade += new Random().Next(1, 5);
                        }

                        student.Grade /= student.LecturesCount;

                        if (student.Grade < 4)
                        {
                            // Send sms to student
                        }
                    }
                }

                foreach (var lecture in request.Lecturer.Lectures)
                {
                    foreach (var student in _context.Students)
                    {
                        if (!lecture.Students.Contains(student))
                        {
                            student.SkipCount++;
                        }

                        if (student.SkipCount >= 3)
                        {
                            // Send email to student and lecturer
                        }

                        student.Grade *= student.LecturesCount;

                        student.LecturesCount++;

                        student.Grade /= student.LecturesCount;

                        if (student.Grade < 4)
                        {
                            // Send sms to student
                        }
                    }
                }

                // activity.Title = request.Activity.Title ?? activity.Title;
                _mapper.Map(request.Lecturer, lecturer);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
