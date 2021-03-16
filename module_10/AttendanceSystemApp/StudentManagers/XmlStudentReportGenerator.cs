using AttendanceSystemApp.AttendanceSystemExceptions;
using AttendanceSystemApp.Models;
using AttendanceSystemDomain.Models;
using AttendanceSystemPersistence;
using LogAdapter;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AttendanceSystemApp.StudentManagers
{
    public class XmlStudentReportGenerator
    {
        public class Query : IRequest<string>
        {
            public string Name { get; set; }
            public ILogger Logger { get; set; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = _context.Students.Where(x => x.Name.Equals(request.Name));

                if (student.Count() <= 0)
                {
                    var err = new DataObjectNotFoundException(nameof(student));

                    LogMethods.LogError("Student not found", err, request.Logger);

                    throw err;
                }

                var report = new StudentReport()
                {
                    StudentName = student.First().Name,
                    YearGrade = student.First().Grade
                };

                var xmlSer = new XmlSerializer(report.GetType());

                using (var stream = new MemoryStream())
                {
                    xmlSer.Serialize(stream, report);
                    stream.Position = 0;
                    using var reader = new StreamReader(stream);
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
