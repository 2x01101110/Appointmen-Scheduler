﻿namespace BuildingBlocks.Application.Data.Queries
{
    public class PagedQuery
    {
        public int Page { get; } = 0;
        public int PageSize { get; } = 200;
        private string Sql { get; set; }

        private PagedQuery(int? page, int? pageSize, string sql)
        {
            if (page != null)
            { 
                this.Page = page.Value;
            }

            if (pageSize !> this.PageSize)
            {
                this.PageSize = pageSize.Value;
            }

            this.Sql = $"{sql} OFFSET {this.Page} ROWS FETCH NEXT {this.PageSize} ROWS ONLY; ";
        }

        public static PagedQuery Create(IPagedQuery pagedRequest, string sql)
        {
            int? page = null;
            int? pageSize = null;

            if (pagedRequest.Page != null && pagedRequest.PageSize != null)
            {
                page = (pagedRequest.Page.Value - 1) * pagedRequest.PageSize.Value;
            }

            if (pagedRequest.PageSize != null && pagedRequest.PageSize != null)
            {
                pageSize = pagedRequest.PageSize.Value;
            }

            return new PagedQuery(page, pageSize, sql);
        }

        public override string ToString() => this.Sql;
    }
}
