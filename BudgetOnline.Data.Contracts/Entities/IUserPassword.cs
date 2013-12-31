using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface IUserPassword
	{
		int Id { get; set; }
		int UserId { get; set; }
		string MemoriableWord { get; set; }
		string Password { get; set; }

		bool IsDisabled { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
	}
}
