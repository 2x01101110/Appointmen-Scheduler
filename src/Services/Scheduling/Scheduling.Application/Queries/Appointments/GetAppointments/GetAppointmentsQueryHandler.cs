using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Data.Queries;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Queries.Appointments.GetAppointments
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

            var query = PagedQuery.Create(request,
                "SELECT " +
                    $"[Appointments].[Id] as [{nameof(AppointmentDto.Id)}], " +
                    $"[Appointments].[FirstName] as [{nameof(AppointmentDto.FirstName)}], " +
                    $"[Appointments].[LastName] as [{nameof(AppointmentDto.LastName)}], " +
                    $"[Appointments].[Email] as [{nameof(AppointmentDto.Email)}], " +
                    $"[Appointments].[Phone] as [{nameof(AppointmentDto.Phone)}] " +
                    //$"[Appointments].[Phone] as [{nameof(AppointmentDto.Phone)}], " +
                    //$"[Appointments].[Status] as [{nameof(AppointmentDto.Status)}] " +
                "FROM " +
                    "[Appointments] " +
                "order by " +
                    "[Appointments].[Id] desc"
                );

            var appointments = await connection.QueryAsync<AppointmentDto>(query.ToString());

            return appointments.AsList();
        }
    }
}
