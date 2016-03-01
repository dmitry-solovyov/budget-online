using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Data.Models;

namespace BudgetOnline.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return DataContext.Users.Select(x => new User
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName
            });
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(User item)
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
