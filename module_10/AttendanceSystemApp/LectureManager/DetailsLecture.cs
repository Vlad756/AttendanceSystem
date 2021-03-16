using AttendanceSystemApp.AttendanceSystemExceptions;
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

namespace AttendanceSystemApp.LectureManager
{
    public class DetailsLecture
    {
        public class Query : IRequest<Lecture>
        {
            public Guid Id { get; set; }
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, Lecture>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Lecture> Handle(Query request, CancellationToken cancellationToken)
            {
                var lecture = await _context.Lectures.FindAsync(request.Id);

                if (lecture == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecture));

                    LogMethods.LogError("Lecture not found", ex, request.Logger);

                    throw ex;
                }

                return lecture;
            }
        }
    }
}
