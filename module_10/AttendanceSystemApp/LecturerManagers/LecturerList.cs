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
    public class LecturerList
    {
        public class Query : IRequest<List<Lecturer>> 
        {
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Lecturer>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Lecturer>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Lecturers.ToListAsync();
            }
        }
    }
}
