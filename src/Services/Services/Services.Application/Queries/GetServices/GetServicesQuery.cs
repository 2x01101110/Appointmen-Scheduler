using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application.Queries.GetServices
{
    public class GetServicesQuery : IRequest<List<ServiceDto>>
    {
    }
}
