using System.Data.Linq;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ISourceContext<T>
		where T: class 
	{
		Table<T> GetSource();
	}
}
