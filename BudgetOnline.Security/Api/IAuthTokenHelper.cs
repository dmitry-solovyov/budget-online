namespace BudgetOnline.Security.Api
{
    public interface IAuthTokenHelper
    {
        string GetToken();
        string GenerateToken(string userName, string password);
        string GetAuthorizationHeader();

    }
}