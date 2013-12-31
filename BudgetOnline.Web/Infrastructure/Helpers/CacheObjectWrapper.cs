using System;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
    public class CacheObjectWrapper
    {
        public DateTime Updated { get; set; }
        public object Data { get; set; }

        public CacheObjectWrapper() { }
        public CacheObjectWrapper(object data)
        {
            Updated = DateTime.Now;
            Data = data;
        }
    }
}