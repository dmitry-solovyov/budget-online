﻿using System;
using System.Collections.Generic;

namespace BudgetOnline.Web.Models
{
	public class SearchCriteria<TListType, TSearch> 
	{
		public string Key { get; set; }

		public TSearch Search { get; set; }

		public IEnumerable<TListType> Result { get; set; }

		public DateTime WhenCreated { get; set; }
		public bool NeedUpdateData { get; set; }

		public SearchCriteria()
		{
			WhenCreated = DateTime.Now;
		}
	}
}