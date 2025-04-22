using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Core.Common.Pagination
{
    public class PaginationRequest
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string FilterText { get; set; } = string.Empty;
        public string SortColumn { get; set; } = "Id";
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }
}
