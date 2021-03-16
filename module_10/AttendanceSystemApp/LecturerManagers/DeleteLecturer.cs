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

namespace AttendanceSystemApp.LecturerManagers
{
    public class DeleteLecturer
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
                var lecturer = await _context.Lecturers.FindAsync(request.Id);

                if (lecturer == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecturer));

                    LogMethods.LogError("Lecturer not found", ex, request.Logger);

                    throw ex;
                }

                _context.Remove(lecturer);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
