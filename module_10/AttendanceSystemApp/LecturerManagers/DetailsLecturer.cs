using AttendanceSystemApp.AttendanceSystemExceptions;
using AttendanceSystemApp.Models;
using AttendanceSystemPersistence;
using LogAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceSystemApp.LecturerManagers
{
    public class DetailsLecturer
    {
        public class Query : IRequest<Lecturer>
        {
            public Guid Id { get; set; }
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, Lecturer>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Lecturer> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecturer = await _context.Lecturers.FindAsync(request.Id);

                if (lecturer == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecturer));

                    LogMethods.LogError("Lecturer not found", ex, request.Logger);

                    throw ex;
                }

                return lecturer;
            }
        }
    }
}
