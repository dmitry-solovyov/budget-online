namespace BudgetOnline.Security.Api
{
    public interface IApiSessionProvider
    {
        SessionInfo CurrentSession { get; }
        string StartSession(string userName, string password);
        void UpdateTokenUsage();
    }
}