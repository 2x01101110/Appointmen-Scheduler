using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Application.Queries.GetServices
{
    public class GetServicesQuery : IRequest<List<ServiceDto>>
    {
    }
}
