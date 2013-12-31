using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using User = BudgetOnline.Data.MSSQL.User;

namespace BudgetOnline.Data.Manage.Repositories
{
	public class UserRepository : InternalRepository<User, Types.Simple.User>, IUserRepository
	{
		public override Table<User> Source
		{
			get { return DatabaseContext.Get().Users; }
		}

		public Types.Simple.User GetUser(int id)
		{
			return GetSingle(o => o.Id == id);
		}

		public IEnumerable<Types.Simple.User> GetUsers(int[] userIds)
		{
			return GetListInternal().Where(row => userIds.Contains(row.Id))
				.Select(row => MappingHelper.OutMapper(row));
		}

		public Types.Simple.User FindByEmail(string email)
		{
			return GetSingle(o => o.Email == email);
		}

		public IEnumerable<Types.Simple.User> GetUsers(int sectionId)
		{
			return GetList(o => o.SectionId == sectionId);
		}

		public void Update(Types.Simple.User user)
		{
			UpdateInternal(
				o => o.Id == user.Id,
				record =>
				{
					record.IsDisabled = user.IsDisabled;
					record.IsForsePassword = user.IsForsePassword;
					record.UpdatedWhen = DateTime.UtcNow;
					record.Name = user.Name;
					record.ContactPhoneNumber = user.ContactPhoneNumber;
				});
		}

		public bool IsUserSectionAdmin(string email)
		{
			using (var db = DatabaseContext.Get())
			{
				var user = FindByEmail(email);

				var sectionAdmins =
					from t in db.SectionAdmins
					where t.SectionId == user.SectionId && t.UserId == user.Id && t.BlockedWhen == null
					select t.UserId;

				return sectionAdmins.Any();
			}
		}
	}
}
