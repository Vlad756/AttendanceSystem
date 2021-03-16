using AttendanceSystemApp.Models;
using AttendanceSystemPersistence;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceSystemApp.HomeWorkManager
{
    public class CreateHomeWork
    {
        public class Command : IRequest
        {
            public HomeWork HomeWork { get; set; }
            public ILogger Logger { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.HomeWork).SetValidator(new HomeWorkValidator());
            }
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
                _context.HomeWorks.Add(request.HomeWork);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
