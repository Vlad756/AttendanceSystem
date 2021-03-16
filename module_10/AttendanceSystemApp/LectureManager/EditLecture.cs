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

namespace AttendanceSystemApp.LectureManager
{
    public class EditLecture
    {
        public class Command : IRequest
        {
            public Lecture Lecture { get; set; }
            public ILogger Logger { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Lecture).SetValidator(new LectureValidator());
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
                var lecture = await _context.Lectures.FindAsync(request.Lecture.Id);

                if (lecture == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecture));

                    LogMethods.LogError("Lecture not found", ex, request.Logger);

                    throw ex;
                }

                // homework check
                foreach (var student in request.Lecture.Students)
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

                foreach (var student in _context.Students)
                {
                    if (!request.Lecture.Students.Contains(student))
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

                _mapper.Map(request.Lecture, lecture);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
