using System.Data.Linq;

namespace BudgetOnline.Data.Contracts.Repositories
{
	public interface ISourceContext<T>
		where T: class 
	{
		Table<T> GetSource();
	}
}
