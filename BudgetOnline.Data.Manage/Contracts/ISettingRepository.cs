using System.Collections.Generic;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface ISettingRepository
	{
		Setting FindByName(int? sectionId, int? userId, string name);
		IEnumerable<Setting> GetSettings(int sectionId, int userId);
		void Set<T>(int sectionId, int? userId, string name, T value);
		void Reset(int sectionId, int userId, string name);
	}
}
