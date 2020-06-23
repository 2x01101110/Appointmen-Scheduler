using BuildingBlocks.Application.Data;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Appointment.Application.Queries.Appointments.GetAppointments
{
    class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<AppointmentDto>>
    {
        private readonly ISqlConnectionFactory connectionFactory;

        public GetAppointmentsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<List<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var connection = connectionFactory.GetOpenConnection();

            var query = "SELECT " +
                "[Appointments].[Id], " +
                "[Appointments].[FirstName], " +
                "[Appointments].[LastName], " +
                "[Appointments].[Email], " +
                "[Appointments].[Phone] from [Appointments]";

            var appointments = await connection.QueryAsync<AppointmentDto>(query);

            return appointments.AsList();
        }
    }
}
