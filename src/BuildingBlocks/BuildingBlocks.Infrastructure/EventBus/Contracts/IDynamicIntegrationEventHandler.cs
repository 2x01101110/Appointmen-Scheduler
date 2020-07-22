using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.EventBus.Contracts
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Hande(dynamic eventData);
    }
}
