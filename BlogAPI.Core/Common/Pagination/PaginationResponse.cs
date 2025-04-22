using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Core.Common.Pagination
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public PaginationResponse(IEnumerable<T> data, int page, int pageSize, int total)
        {
            Data = data;
            Page = page;
            PageSize = pageSize;
            Total = total;
        }
    }
}
