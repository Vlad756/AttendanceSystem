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

namespace AttendanceSystemApp.Managers
{
    public class DeleteStudent
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
                var student = await _context.Students.FindAsync(request.Id);

                if (student == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(student));

                    LogMethods.LogError("Student not found", ex, request.Logger);

                    throw ex;
                }

                _context.Remove(student);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }

    }
}
