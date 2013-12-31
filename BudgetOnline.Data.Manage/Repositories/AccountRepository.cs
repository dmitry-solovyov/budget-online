using System;
using System.Collections.Generic;
using System.Data.Linq;
using BudgetOnline.Data.Manage.Contracts;
using Account = BudgetOnline.Data.MSSQL.Account;

namespace BudgetOnline.Data.Manage.Repositories
{
    public class AccountRepository : InternalRepository<Account, Types.Simple.Account>, IAccountRepository
    {
        #region Overrides of InternalRepository<Account,Account>

        public override Table<Account> Source
        {
            get { return DatabaseContext.Get().Accounts; }
        }

        #endregion

        #region Implementation of IAccountRepository

        public IEnumerable<Types.Simple.Account> GetList(int sectionId)
        {
            return GetList(o => o.SectionId == sectionId);
        }

        public Types.Simple.Account GetDefault(int sectionId)
        {
            return base.GetSingle(o => o.IsDefault && o.SectionId == sectionId);
        }

        public void Update(Types.Simple.Account row)
        {
            UpdateInternal(
                o => o.Id == row.Id,
                record =>
                {
                    record.IsDisabled = row.IsDisabled;
                    record.IsDefault = row.IsDefault;
                    record.IsExternal = row.IsExternal;
                    record.UpdatedWhen = DateTime.UtcNow;
                    record.UpdatedBy = row.UpdatedBy;
                    record.Name = row.Name;
                    record.Description = row.Description;
                    record.ShowForIncome = row.ShowForIncome;
                    record.ShowForOutcome = row.ShowForOutcome;
                    record.ShowForTransfer = row.ShowForTransfer;
                });
        }

        public Types.Simple.Account Get(int accountId)
        {
            return base.GetSingle(o => o.Id == accountId);
        }

        #endregion
    }
}
