using AttendanceSystemApp.AttendanceSystemExceptions;
using AttendanceSystemApp.Models;
using AttendanceSystemPersistence;
using AutoMapper;
using FluentValidation;
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
    public class EditHomeWork
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
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var homeWork = await _context.HomeWorks.FindAsync(request.HomeWork.Id);

                if (homeWork == null)
                {
                    var ex = new DataObjectNotFoundException(nameof(homeWork));

                    LogMethods.LogError("HomeWork not found", ex, request.Logger);

                    throw ex;
                }

                _mapper.Map(request.HomeWork, homeWork);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
