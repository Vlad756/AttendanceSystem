using AttendanceSystemApp.Models;
using AttendanceSystemPersistence;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceSystemApp.LecturerManagers
{
    public class CreateLecturer
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

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

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

                _context.Lecturers.Add(request.Lecturer);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
