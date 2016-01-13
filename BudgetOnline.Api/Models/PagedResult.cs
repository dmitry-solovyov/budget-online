using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetOnline.Api.Models
{
    public class PagedResult<T>
        where T : class
    {
        public PagedResult(IEnumerable<T> items, int itemsCount, int pageSize, int currentPage)
        {
            ItemsCount = itemsCount;
            Items = items;

            PageSize = pageSize;
            CurrentPage = currentPage;
            PageCount = itemsCount / pageSize + 1;
        }

        public int ItemsCount { get; private set; }
        public int PageCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public IEnumerable<T> Items { get; private set; }
    }
}