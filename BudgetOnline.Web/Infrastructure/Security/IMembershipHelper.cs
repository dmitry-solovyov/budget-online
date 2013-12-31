using System.Globalization;
using BudgetOnline.Web.Models;

namespace BudgetOnline.Web.Infrastructure.Security
{
	public interface IMembershipHelper
	{
		UserModel CurrentUser { get; }
		UserModel GetUser();
		UserModel GetUser(int userId);
		UserModel GetUser(string userName);

		CultureInfo GetCulture();
		CultureInfo GetCulture(int userId);

		void SetCulture(CultureInfo culture);

		bool UsersInOneSection(params int?[] ids);
	}
}