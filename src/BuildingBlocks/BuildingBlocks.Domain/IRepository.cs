using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
