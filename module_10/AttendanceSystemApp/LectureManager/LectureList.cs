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
    public class LectureList
    {
        public class Query : IRequest<List<Lecture>> 
        {
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Lecture>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Lecture>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Lectures.ToListAsync();
            }
        }
    }
}
