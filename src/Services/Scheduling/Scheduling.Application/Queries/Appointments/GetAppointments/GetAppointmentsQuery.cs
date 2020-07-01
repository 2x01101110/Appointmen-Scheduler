using BuildingBlocks.Application.Data.Queries;
using MediatR;
using System.Collections.Generic;

namespace Scheduling.Application.Queries.Appointments.GetAppointments
{
    public class GetAppointmentsQuery : IRequest<List<AppointmentDto>>, IPagedQuery
    {
        public GetAppointmentsQuery(int? page = null, int? pageSize = null)
        {
            this.Page = page;
            this.PageSize = pageSize;
        }

        public int? Page { get; }
        public int? PageSize { get; }
    }
}
