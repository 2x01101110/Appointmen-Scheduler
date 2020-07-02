using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Commands;

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
            await _mediator.Send(new TestCommand());
            return Ok("Command Works");
        }
    }
}
