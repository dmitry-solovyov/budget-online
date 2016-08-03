using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Contracts;
using BudgetOnline.Data.Keys;
using BudgetOnline.Data.Models;
using BudgetOnline.Data.Models.BaseModels;
using BudgetOnline.Data.QueryManagement;
using BudgetOnline.Data.Repositories.Base;

namespace BudgetOnline.Data.Repositories
{
    public class UserSessionRepository : BaseRepository, IUserSessionRepository
    {
        public UserSession GetById(IKeyField key)
        {
            if (!(key is GuidKeyField))
                throw new ArgumentOutOfRangeException("key");

            var gkey = (GuidKeyField)key;

            return DataContext.UserSessions.Include("Users")
                .Where(x => x.Id == gkey.Id)
                .Select(x =>
                new UserSession
                {
                    UserPasswordId = x.UserPasswordId,
                    User = new GuidRef
                    {
                        Id = x.UserId,
                        Name = x.User.UserName,
                        Email = x.User.Email
                    },
                    CreatedWhen = x.CreatedWhen,
                    UserSessionStatus = (UserSessionStatuses)x.UserSessionStatusId
                })
                .FirstOrDefault();
        }

        public IEnumerable<UserSession> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserSession> Find(OperationsGroup @group)
        {
            throw new NotImplementedException();
        }

        public UserSession GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(UserSession item)
        {
            throw new NotImplementedException();
        }

        public void Add(IEnumerable<UserSession> items)
        {
            throw new NotImplementedException();
        }

        public void Update(UserSession item)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserSession item)
        {
            throw new NotImplementedException();
        }

        public void MarkPreviousTokensDisabled(Guid userId)
        {
            var connections = DataContext.UserSessions
                .Where(x => x.UserId == userId
                    && (x.ExpiresWhen > DateTime.UtcNow));

            foreach (var connection in connections)
            {
                connection.ExpiresWhen = DateTime.UtcNow;
            }

            DataContext.SaveChanges();
        }
    }
}
