using System.Collections;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Core
{
    public class RequestGlobalContext : IRequestGlobalContext
    {
        public int UserId { get; set; }
        public int DefaultCurrencyId { get; set; }

        public Hashtable Data { get; private set; }

        public RequestGlobalContext()
        {
            Data = new Hashtable();
        }
    }
}