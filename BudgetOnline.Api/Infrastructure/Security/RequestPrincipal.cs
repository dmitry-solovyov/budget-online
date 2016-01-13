using ReMi.BusinessEntities.Auth;
using System;
using System.Security.Principal;

namespace ReMi.Api.Insfrastructure.Security
{
    public class RequestPrincipal : IPrincipal
    {
        private readonly Account _account;

        public RequestPrincipal() : this(null)
        {
        }

        public RequestPrincipal(Account account)
        {
            if (account != null)
            {
                _account = account;
                Identity = new GenericIdentity(account.Email, "Token");
            }
            else
                Identity = new GenericIdentity(string.Empty);
        }

        public Account Account
        {
            get { return _account; }
        }

        public bool IsInRole(string role)
        {
            if (_account == null || _account.Role.Name.Equals("NotAuthenticated"))
                return false;

            return _account.Role.Name.Equals(role);
        }

        public IIdentity Identity { get; private set; }
    }
}