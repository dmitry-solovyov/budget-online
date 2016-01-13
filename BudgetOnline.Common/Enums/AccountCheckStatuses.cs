namespace BudgetOnline.Common.Enums
{
	public enum AccountCheckStatuses
	{
		Ok,
		UserNotFound,
		UserDisabled,
		PasswordNotFound,
		PasswordDisabled,
		PasswordExpired,
		PasswordNotMatch,
        TokenNotFound,
        TokenExpired
	}
}
