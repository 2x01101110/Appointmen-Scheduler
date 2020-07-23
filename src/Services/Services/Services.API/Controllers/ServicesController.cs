using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var command = new GetServiceQuery() { Id = id };
            var services = await this._mediator.Send(command);
            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody]CreateServiceCommand createServiceCommand)
        {
            await this._mediator.Send(createServiceCommand);
            return NoContent();
        }
    }
}
