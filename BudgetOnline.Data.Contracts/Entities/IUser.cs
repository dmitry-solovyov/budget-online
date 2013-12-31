using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface IUser
	{
		int Id { get; set; }
		int? SectionId { get; set; }
		string Name { get; set; }
		string ContactPhoneNumber { get; set; }
		string Email { get; set; }
		byte[] Avatar { get; set; }

		bool IsDisabled { get; set; }
		bool IsForsePassword { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
		int? UpdatedBy { get; set; }
	}
}
