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

namespace AttendanceSystemApp.HomeWorkManager
{
    public class DetailsHomeWork
    {
        public class Query : IRequest<HomeWork>
        {
            public Guid Id { get; set; }
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, HomeWork>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<HomeWork> Handle(Query request, CancellationToken cancellationToken)
            {
                var homeWork = await _context.HomeWorks.FindAsync(request.Id);

                if (homeWork == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(homeWork));

                    LogMethods.LogError("HomeWork not found", ex, request.Logger);

                    throw ex;
                }

                return homeWork;
            }
        }
    }
}
