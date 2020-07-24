using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Application.Commands.AssignStaffToService;
using Services.Application.Commands.CreateService;
using Services.Application.Queries.GetService;
using Services.Application.Queries.GetServices;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await this._mediator.Send(new GetServicesQuery());
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServices([FromRoute]Guid id)
        {
            var services = await this._mediator.Send(new GetServiceQuery(id));
            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody]CreateServiceCommand createServiceCommand)
        {
            await this._mediator.Send(createServiceCommand);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AssignStaffToService([FromBody] AssignStaffToServiceCommand assignStaffToServiceCommand)
        {
            await this._mediator.Send(assignStaffToServiceCommand);
            return NoContent();
        }
    }
}
