namespace BudgetOnline.Api.Infrastructure.Security
{
    public interface IAuthTokenHelper
    {
        string GetToken();
        string GenerateToken(string userName, string password);
        string GetAuthorizationHeader();

    }
}