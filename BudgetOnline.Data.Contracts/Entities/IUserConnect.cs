using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface IUserConnect
	{
		int Id { get; set; }
		int UserId { get; set; }
		int UserConnectStatusId { get; set; }
		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
	}
}
