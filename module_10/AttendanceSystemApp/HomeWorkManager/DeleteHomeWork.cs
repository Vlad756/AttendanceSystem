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

namespace AttendanceSystemApp.HomeWorkManager
{
    public class DeleteHomeWork
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
                var homeWork = await _context.HomeWorks.FindAsync(request.Id);

                if (homeWork == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(homeWork));

                    LogMethods.LogError("HomeWork not found", ex, request.Logger);

                    throw ex;
                }

                _context.Remove(homeWork);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
