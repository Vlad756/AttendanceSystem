using AttendanceSystemApp.LectureManager;
using AttendanceSystemApp.Lists;
using AttendanceSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemApi.Controllers
{
    public class LecturesController : BaseApiController
    {
        [HttpGet("lectures/report/json/{title}")]
        public async Task<ActionResult<string>> GetJsonLectureReport(string title)
        {
            return await Mediator.Send(new JsonLectureReportGenerator.Query { Title = title, Logger = _logger });
        }

        [HttpGet("lectures/report/xml/{title}")]
        public async Task<ActionResult<string>> GetXmlLectureReport(string title)
        {
            return await Mediator.Send(new XmlLectureReportGenerator.Query { Title = title, Logger = _logger });
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<List<Lecture>>> GetLectures()
        {
            return await Mediator.Send(new LectureList.Query { Logger = _logger });
        }

        [HttpGet("lectures/{id}")]
        public async Task<ActionResult<Lecture>> GetLecture(Guid id)
        {
            return await Mediator.Send(new DetailsLecture.Query { Id = id, Logger = _logger });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLecture(Lecture lecture)
        {
            return Ok(await Mediator.Send(new CreateLecture.Command { Lecture = lecture, Logger = _logger }));
        }

        [HttpPut("lectures/{id}")]
        public async Task<IActionResult> EditLecture(Guid id, Lecture lecture)
        {
            lecture.Id = id;
            return Ok(await Mediator.Send(new EditLecture.Command { Lecture = lecture, Logger = _logger }));
        }

        [HttpDelete("lectures/{id}")]
        public async Task<IActionResult> DeleteLecture(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteLecture.Command { Id = id, Logger = _logger }));
        }
    }
}
