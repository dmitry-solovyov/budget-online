using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetOnline.Web.Infrastructure.Extensions
{
	public static class EnumExtensions
	{
		public static DisplayAttribute GetDisplayAttr<T>(this T enumValue)
		{
			var type = typeof(T);
			var memInfo = type.GetMember(enumValue.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
			return (DisplayAttribute)attributes[0];
		}
	}
}