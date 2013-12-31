using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using BudgetOnline.Data.MSSQL;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Complex;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class UserConnectRepository : InternalRepository<UserConnect, Types.Simple.UserConnect>, IUserConnectRepository
	{
		public override Table<UserConnect> Source
		{
			get { return DatabaseContext.Get().UserConnects; }
		}

		public Types.Simple.UserConnect Get(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		public IEnumerable<Types.Simple.UserConnect> GetHistory(int userId)
		{
			return GetList(o => o.UserId == userId)
				.OrderBy(o => o.CreatedWhen);
		}

		public IEnumerable<UserConnectInfo> GetUsersWithConnects(int sectionId)
		{
			return GetUsersWithConnects(sectionId, string.Empty);
		}

		public IEnumerable<UserConnectInfo> GetUsersWithConnects(int sectionId, string searchBy)
		{
			using (var db = DatabaseContext.Get())
			{
				var sectionAdmins =
					from t in db.SectionAdmins
					where t.SectionId == sectionId && t.BlockedWhen == null
					select t.UserId;

				var items =
					from usr in db.Users
					where usr.SectionId == sectionId
						&& (SqlMethods.Like(usr.Email, "%" + (searchBy ?? string.Empty) + "%")
							|| SqlMethods.Like(usr.Name, "%" + (searchBy ?? string.Empty) + "%"))
					join conn in db.UserConnects on usr.Id equals conn.UserId into conns
					from j2 in conns.DefaultIfEmpty()
					group j2 by new { usr.Id, usr.Email, usr.IsDisabled, usr.Name } into g
					select new { g.Key.Id, g.Key.Email, g.Key.Name, g.Key.IsDisabled, LastConnect = g.Max(t => t.CreatedWhen) };

				return items.Select(o => new UserConnectInfo
				{
					Email = o.Email,
					Name = o.Name,
					UserId = o.Id,
					IsDisabled = o.IsDisabled,
					LastConnected = o.LastConnect,
					IsSectionAdmin = sectionAdmins.Any(subt1 => subt1 == o.Id)
				}).ToList();
			}
		}
	}
}
