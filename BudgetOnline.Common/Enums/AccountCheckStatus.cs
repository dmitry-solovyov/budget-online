namespace BudgetOnline.Common.Enums
{
	public enum AccountCheckStatus
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
