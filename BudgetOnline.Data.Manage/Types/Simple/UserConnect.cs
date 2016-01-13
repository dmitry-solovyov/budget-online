using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class UserConnect
	{
		public int Id { get; set; }
		public int UserId { get; set; }
        public int? UserPasswordId { get; set; }
        public string Origin { get; set; }
        public string Token { get; set; }
		public UserConnectStatuses UserConnectStatusId { get; set; }
        public DateTime? ExpiresWhen { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int CreatedBy { get; set; }
	}
}
