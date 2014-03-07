using System.Collections;

namespace BudgetOnline.Common.Contracts
{
    public interface IRequestGlobalContext
    {
        int UserId { get; set; }
        int DefaultCurrencyId { get; set; }

        Hashtable Data { get; }
    }
}
