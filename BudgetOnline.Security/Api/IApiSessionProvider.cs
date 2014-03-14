using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Security.Api
{
    public interface IApiSessionProvider
    {
        User CurrentUser { get; }
        string StartSession(string userName, string password);
        void UpdateTokenUsage();
    }
}