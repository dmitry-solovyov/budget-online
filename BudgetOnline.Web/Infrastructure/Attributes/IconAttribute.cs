using System;

namespace BudgetOnline.Web.Infrastructure.Attributes
{
	public class IconAttribute : Attribute
	{
		private readonly string _name;
		public IconAttribute(string name)
		{
			_name = name;
		}

		public string Name { get { return _name; } }
	}
}