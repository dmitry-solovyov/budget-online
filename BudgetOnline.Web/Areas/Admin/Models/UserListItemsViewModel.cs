using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class UserListItemsViewModel
	{
		public int Id { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; }
		[DisplayName("E-mail")]
		public string Email { get; set; }
		[DisplayName("Админ")]
		public bool IsAdmin { get; set; }
		[DisplayName("Читатель")]
		public bool IsReadOnly { get; set; }
		[DisplayName("Блокирован")]
		public bool IsBocked { get; set; }
		[DisplayName("Не подтвержден")]
		public bool IsNotApproved { get; set; }
		[DisplayName("Последний визит")]
		public DateTime? WhenLastConnected { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }
	}
}