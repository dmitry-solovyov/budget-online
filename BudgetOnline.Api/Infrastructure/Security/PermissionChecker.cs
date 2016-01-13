using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using ReMi.Api.Insfrastructure.Commands;
using ReMi.BusinessEntities.Auth;
using System;
using ReMi.BusinessLogic.BusinessRules;
using ReMi.Common.Constants.Auth;
using ReMi.Common.Constants.BusinessRules;
using ReMi.Common.Utils;
using ReMi.DataAccess.BusinessEntityGateways.Auth;
using ReMi.DataAccess.BusinessEntityGateways.BusinessRules;

namespace ReMi.Api.Insfrastructure.Security
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(CommandProcessorGeneric<>));

        public Func<ISecurityGateway> SecurityGateway { get; set; }
        public IAuthorizationManager AuthorizationManager { get; set; }
        public IBusinessRuleEngine BusinessRuleEngine { get; set; }
        public Func<IBusinessRuleGateway> BusinessRuleGateway { get; set; }

        public PermissionStatus CheckCommandPermission(Type commandType, Account account)
        {
            bool isRoleEmpty;
            return CheckPermissions(commandType, account, GetCommandRoles, out isRoleEmpty);
        }

        public PermissionStatus CheckQueryPermission(Type commandType, Account account)
        {
            bool isRoleEmpty;
            var result = CheckPermissions(commandType, account, GetQueryRoles, out isRoleEmpty);
            return result == PermissionStatus.NotAuthorized && isRoleEmpty ? PermissionStatus.Permmited : result;
        }

        public PermissionStatus CheckRule(Account account, object data)
        {
            using (var gateway = BusinessRuleGateway())
            {
                var type = data.GetType();
                var ruleId = gateway.GetBusinessRuleId(BusinessRuleGroup.Permissions, type.Name + "Rule");
                if (!ruleId.HasValue)
                    return PermissionStatus.Permmited;
                var name = type.FullName.StartsWith("ReMi.Commands") ? "command" : "query";
                return BusinessRuleEngine.Execute<bool>(account, ruleId.Value,
                        new Dictionary<string, object> { { name, data } })
                    ? PermissionStatus.Permmited : PermissionStatus.NotAuthorized;
            }
        }

        private PermissionStatus CheckPermissions(Type type, Account account, Func<string, ISecurityGateway, IEnumerable<Role>> getRolesMethod, out bool isRolesEmpty)
        {
            using (var gateway = SecurityGateway())
            {
                var roles = getRolesMethod(type.Name, gateway);

                var rolesList = roles == null || roles is IList<Role> ? roles as IList<Role> : roles.ToList();
                if (rolesList.IsNullOrEmpty())
                {
                    isRolesEmpty = true;
                    return PermissionStatus.NotAuthorized;
                }

                isRolesEmpty = false;
                if (account == null)
                {
                    if (rolesList.Any(role => role.Name == "NotAuthenticated"))
                    {
                        return PermissionStatus.Permmited;
                    }

                    _logger.ErrorFormat("User is not logged in or session expire.");
                    return PermissionStatus.NotAuthenticated;
                }
                var authorizationCheck = AuthorizationManager.IsAuthorized(rolesList);
                if (!authorizationCheck)
                {
                    _logger.ErrorFormat("User '{0}' with role '{1}' does not have permissions to invoke command '{2}'",
                        account.FullName, account.Role, type.Name);
                }

                return authorizationCheck ? PermissionStatus.Permmited : PermissionStatus.NotAuthorized;
            }
        }

        private IEnumerable<Role> GetCommandRoles(string name, ISecurityGateway gateway)
        {
            return gateway.GetCommandRoles(name);
        }

        private IEnumerable<Role> GetQueryRoles(string name, ISecurityGateway gateway)
        {
            return gateway.GetQueryRoles(name);
        }
    }
}