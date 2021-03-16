using AttendanceSystemApp.Models;
using AttendanceSystemPersistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceSystemApp.Lists
{
    public class HomeWorkList
    {
        public class Query : IRequest<List<HomeWork>> 
        {
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<HomeWork>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<HomeWork>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.HomeWorks.ToListAsync();
            }
        }
    }
}
