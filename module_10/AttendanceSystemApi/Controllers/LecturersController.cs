using AttendanceSystemApp.LecturerManagers;
using AttendanceSystemApp.Lists;
using AttendanceSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemApi.Controllers
{
    public class LecturersController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Lecturer>>> GetLecturers()
        {
            return await Mediator.Send(new LecturerList.Query { Logger = _logger });
        }

        [HttpGet("lecturer/{id}")]
        public async Task<ActionResult<Lecturer>> GetLecturer(Guid id)
        {
            return await Mediator.Send(new DetailsLecturer.Query { Id = id, Logger = _logger });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLecturer(Lecturer lecturer)
        {
            return Ok(await Mediator.Send(new CreateLecturer.Command { Lecturer = lecturer, Logger = _logger }));
        }

        [HttpPut("lecturer/{id}")]
        public async Task<IActionResult> EditLecturer(Guid id, Lecturer lecturer)
        {
            lecturer.Id = id;
            return Ok(await Mediator.Send(new EditLecturer.Command { Lecturer = lecturer, Logger = _logger }));
        }

        [HttpDelete("lecturer/{id}")]
        public async Task<IActionResult> DeleteLecturer(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteLecturer.Command { Id = id, Logger = _logger }));
        }
    }
}
