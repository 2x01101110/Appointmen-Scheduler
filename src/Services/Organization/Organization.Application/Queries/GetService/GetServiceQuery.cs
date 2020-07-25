using MediatR;
using System;

namespace Organization.Application.Queries.GetService
{
    public class GetServiceQuery : IRequest<ServiceDto>
    {
        public Guid Id { get; private set; }

        public GetServiceQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
