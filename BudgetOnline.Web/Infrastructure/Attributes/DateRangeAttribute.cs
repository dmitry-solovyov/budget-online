using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Web.UI.Validators
{
	public class YearRangeAttribute : ValidationAttribute
	{
		public int FromYear { get; set; }
		public int ToYear { get; set; }

		public YearRangeAttribute()
		{
			FromYear = 2011;
		}

		public override bool IsValid(object value)
		{
			if(value == null)
				return ToYear == 0;

			DateTime date;
			if (value.GetType().AssemblyQualifiedName != typeof(DateTime).AssemblyQualifiedName)
				date = DateTime.Parse(value as string);
			else
				date = (DateTime)value;

			if ((FromYear <= 0 || date.Year >= FromYear) && (ToYear <= 0 || date.Year <= ToYear))
				return true;

			return false;
		}
	}

}
