using BudgetOnline.Common.Contracts;
using BudgetOnline.Security.Api;

namespace BudgetOnline.Api.Infrastructure.Security
{
	public class CurrentUserProvider : ICurrentUserProvider
	{
        public IApiSessionProvider ApiSessionProvider { get; set; }

		public int SectionId
		{
            get { return ApiSessionProvider.CurrentSession.User.SectionId; }
		}

	    public int UserId
	    {
            get { return ApiSessionProvider.CurrentSession.User.Id; }
	    }
	}
}