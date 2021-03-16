using AttendanceSystemApp.Interfaces;
using AttendanceSystemApp.Lists;
using AttendanceSystemApp.Managers;
using AttendanceSystemApp.Models;
using AttendanceSystemApp.StudentManagers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemApi.Controllers
{
    public class StudentsController : BaseApiController
    {
        [HttpGet("student/report/json/{name}")]
        public async Task<ActionResult<string>> GetJsonSudentReport(string name)
        {
            return await Mediator.Send(new JsonStudentReportGenerator.Query { Name = name, Logger = _logger });
        }

        [HttpGet("student/report/xml/{name}")]
        public async Task<ActionResult<string>> GetXmlSudentReport(string name)
        {
            return await Mediator.Send(new XmlStudentReportGenerator.Query { Name = name, Logger = _logger });
        }

        [HttpGet("student")]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return await Mediator.Send(new StudentList.Query { Logger = _logger });
        }

        [HttpGet("student/{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            return await Mediator.Send(new DetailsStudent.Query { Id = id, Logger = _logger });
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            return Ok(await Mediator.Send(new CreateStudent.Command { Student = student, Logger = _logger }));
        }

        [HttpPut("student/{id}")]
        public async Task<IActionResult> EditStudent(Guid id, Student student)
        {
            student.Id = id;
            return Ok(await Mediator.Send(new EditStudent.Command { Student = student, Logger = _logger }));
        }

        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteStudent.Command { Id = id, Logger = _logger }));
        }
    }
}
