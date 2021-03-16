using AttendanceSystemApp.AttendanceSystemExceptions;
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
    public class DeleteLecture
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public ILogger Logger { get; set; }
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
                var lecture = await _context.Lectures.FindAsync(request.Id);

                if (lecture == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecture));

                    LogMethods.LogError("Lecture not found", ex, request.Logger);

                    throw ex;
                }

                _context.Remove(lecture);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
