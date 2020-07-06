using System;
using System.Threading.Tasks;
using BuildingBlocks.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Commands.Scheduling;

namespace Scheduling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery]int? page, [FromQuery]int? pageSize)
        {
            var testCommand = new TestCommand();
            var command = new IdempotentCommand<TestCommand>(testCommand, Guid.Parse("c8d114de-1fdb-4c1e-8fbc-ada33dbc2129"));

            await _mediator.Send(command);

            return Ok("Command Works");
        }
    }
}
