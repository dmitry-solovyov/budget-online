using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Security
{
	public class CurrentUserProvider : ICurrentUserProvider
	{
		public IMembershipHelper MembershipHelper { get; set; }

		public int SectionId
		{
			get { return MembershipHelper.CurrentUser.SectionId; }
		}

	    public int UserId
	    {
            get { return MembershipHelper.CurrentUser.Id; }
	    }
	}
}