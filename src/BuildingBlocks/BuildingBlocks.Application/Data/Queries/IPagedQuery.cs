using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.Data.Queries
{
    public interface IPagedQuery
    {
        int? Page { get; }
        int? PageSize { get; }
    }
}
