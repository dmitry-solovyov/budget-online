using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Data.Contracts;
using BudgetOnline.Data.Keys;
using BudgetOnline.Data.Models;
using BudgetOnline.Data.QueryManagement;
using BudgetOnline.Data.Repositories.Base;

namespace BudgetOnline.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User GetById(IKeyField key)
        {
            if(!(key is GuidKeyField))
                throw new ArgumentOutOfRangeException("key");

            var gkey = (GuidKeyField) key;

            return DataContext.Users
                .Where(x => x.Id == gkey.Id)
                .Select(x =>
                new User
                {
                    Id = x.Id,
                    Email = x.Email,
                    UserName = x.UserName
                })
                .FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return DataContext.Users.Select(x => 
                new User
                {
                    Id = x.Id,
                    Email = x.Email,
                    UserName = x.UserName
                });
        }

        public IEnumerable<User> Find(OperationsGroup @group)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public void Add(IEnumerable<User> items)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(User item)
        {
            throw new NotImplementedException();
        }
    }
}
