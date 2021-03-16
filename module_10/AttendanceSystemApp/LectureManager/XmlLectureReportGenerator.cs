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
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AttendanceSystemApp.LectureManager
{
    public class XmlLectureReportGenerator
    {
        public class Query : IRequest<string>
        {
            public string Title { get; set; }
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
                var lecture = _context.Lectures.Where(x => x.CourseName.Equals(request.Title));

                if (lecture.Count() <= 0)
                {
                    var ex = new DataObjectNotFoundException(nameof(lecture));

                    LogMethods.LogError("Lecture not found", ex, request.Logger);

                    throw ex;
                }

                var report = new LectureReport()
                {
                    LectureTitle = lecture.First().CourseName,
                    //Students = lecture.Students.Select(x => x.Name).ToList()
                    Students = new List<string> { "Test", "AGA" }
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
