using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.Attributes
{
	public class ControlRowHeaderAttribute : Attribute, IMetadataAware
	{
		private readonly string _title;
		public ControlRowHeaderAttribute(string title)
		{
			_title = title;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["ControlHeader"] = _title;
		}

		public string Title
		{
			get { return _title; }
		}
	}
}
