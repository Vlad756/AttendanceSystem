using AttendanceSystemApp.AttendanceSystemExceptions;
using AttendanceSystemApp.Interfaces;
using AttendanceSystemApp.Models;
using AttendanceSystemPersistence;
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
    public class DetailsStudent
    {
        public class Query : IRequest<Student>
        {
            public Guid Id { get; set; }
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, Student>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Student> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _context.Students.FindAsync(request.Id);

                if (student == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(student));

                    LogMethods.LogError("Student not found", ex, request.Logger);

                    throw ex;
                }

                return student;
            }
        }
    }
}
