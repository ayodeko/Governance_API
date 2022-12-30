using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovernancePortal.Service.ClientModels.General
{
    public class PageQuery
    {
        [FromQuery(Name = "PageNumber")]
        public int PageNumber { get; set; }
        [FromQuery(Name = "PageSize")]
        public int PageSize { get; set; }
        public PageQuery()
        {
            this.PageNumber = this.PageNumber != 0 ? this.PageNumber : 1;
            this.PageSize = this.PageSize != 0 ? this.PageSize : 50;
        }
        public PageQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 50 ? 50 : pageSize;
        }
        
        public static ValueTask<PageQuery> BindAsync(HttpContext context)
            => new ValueTask<PageQuery>(new PageQuery(
                pageNumber: int.TryParse(context.Request.Query["pageNumber"], out var skip) ? skip : 1,
                pageSize: int.TryParse(context.Request.Query["pageSize"], out var take) ? take : 50));
    }

    public class Pagination<T> where T: class
    {
        public IEnumerable<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRecords { get; internal set; }
        public bool IsSuccessful { get; set; }
        public string StatusCode { get; set; }

        public string Message { get; set; }

    }
}
