using AttendanceSystemApp.HomeWorkManager;
using AttendanceSystemApp.Lists;
using AttendanceSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemApi.Controllers
{
    public class HomeWorksController : BaseApiController
    {
        [HttpGet("homeworks")]
        public async Task<ActionResult<List<HomeWork>>> GetHomeWorks()
        {
            return await Mediator.Send(new HomeWorkList.Query { Logger = _logger });
        }

        [HttpGet("homeworks/{id}")]
        public async Task<ActionResult<HomeWork>> GetHomeWork(Guid id)
        {
            return await Mediator.Send(new DetailsHomeWork.Query { Id = id, Logger = _logger });
        }

        [HttpPost]
        public async Task<IActionResult> CreateHomeWork(HomeWork homeWork)
        {
            return Ok(await Mediator.Send(new CreateHomeWork.Command { HomeWork = homeWork, Logger = _logger }));
        }

        [HttpPut("homeworks/{id}")]
        public async Task<IActionResult> EditHomeWork(Guid id, HomeWork homeWork)
        {
            homeWork.Id = id;
            return Ok(await Mediator.Send(new EditHomeWork.Command { HomeWork = homeWork, Logger = _logger }));
        }

        [HttpDelete("homeworks/{id}")]
        public async Task<IActionResult> DeleteHomeWork(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteHomeWork.Command { Id = id, Logger = _logger }));
        }
    }
}
