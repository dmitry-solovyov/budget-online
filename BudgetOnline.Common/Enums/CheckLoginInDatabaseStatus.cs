namespace BudgetOnline.Common.Enums
{
	public enum CheckLoginInDatabaseStatus
	{
		Ok,
		UserNotFound,
		UserDisabled,
		PasswordNotFound,
		PasswordIsDisabled,
		PasswordIsExpired,
		PasswordNotMatch
	}
}
