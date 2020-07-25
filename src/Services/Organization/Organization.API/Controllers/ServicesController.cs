using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Organization.Application.Commands.AssignEmployeeToService;
using Organization.Application.Commands.CreateService;
using Organization.Application.Queries.GetService;
using Organization.Application.Queries.GetServices;

namespace Organization.API.Controllers
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
        public async Task<IActionResult> AssignStaffToService([FromBody]AssignEmployeeToServiceCommand assignStaffToServiceCommand)
        {
            await this._mediator.Send(assignStaffToServiceCommand);
            return NoContent();
        }
    }
}
