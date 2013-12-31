using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class User 
	{
		public int Id { get; set; }
		public int SectionId { get; set; }
		public string Name { get; set; }
		public string ContactPhoneNumber { get; set; }
		public string Email { get; set; }
		public byte[] Avatar { get; set; }
		public bool IsDisabled { get; set; }
		public bool IsForsePassword { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
	}
}
