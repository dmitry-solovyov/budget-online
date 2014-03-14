using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Contracts
{
    public interface IAuthenticationDataHelper
    {
        AccountCheckResult ValidateLogin(string userName, string password);
        AccountCheckResult CheckAccount(string userName);
        AccountCheckResult CheckAccount(int userId);
        UserConnect TrackUsersLogin(string userName);
        UserConnect TrackUsersLogin(int userId);
        UserConnect TrackUsersToken(int userId, string token, int passwordId);
        UserConnect TrackUsersToken(string userName, string token, int passwordId);
        AccountCheckResult UserFromToken(string token);
    }
}
