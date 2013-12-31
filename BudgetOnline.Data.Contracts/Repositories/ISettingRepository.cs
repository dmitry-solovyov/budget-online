using System.Collections.Generic;
using BudgetOnline.Data.Contracts.Entities;

namespace BudgetOnline.Data.Contracts.Repositories
{
	public interface ISettingRepository
	{
		ISetting FindByName(string name, int? sectionId);
		IEnumerable<ISetting> GetUsersSettings(int userId);
		IEnumerable<ISetting> GetGlobalSettings();
		void Add(int userId, string name, string value);
		void ResetValue(int userId, string name);
	}
}
