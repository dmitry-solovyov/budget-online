﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;


namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class AccountListViewModel
	{
		public int Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Блокирован")]
		public bool IsDisabled { get; set; }
		[DisplayName("По-умолчанию")]
		public bool IsDefault { get; set; }

		public DateTime CreatedWhen { get; set; }
		public DateTime? UpdatedWhen { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }
	}
}