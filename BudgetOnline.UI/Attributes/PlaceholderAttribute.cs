using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.Attributes
{
	public class PlaceholderAttribute : Attribute, IMetadataAware
	{
		public string PlaceholderText { get; private set; }
		public PlaceholderAttribute(string placeholderText)
		{
			PlaceholderText = placeholderText;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["Placeholder"] = PlaceholderText;
		}
	}
}
