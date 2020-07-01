using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointment.Application.Queries.Appointments.GetAppointments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppointmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery]int? page, [FromQuery]int? pageSize)
        {
            var query = new GetAppointmentsQuery(page, pageSize);
            var appointments = await mediator.Send(query);

            return Ok(appointments);
        }
    }
}
