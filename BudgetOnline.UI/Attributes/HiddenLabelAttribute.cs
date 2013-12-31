using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.Attributes
{
	public class HiddenLabelAttribute : Attribute, IMetadataAware
	{
		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["HiddenLabel"] = "HiddenLabel";
		}
	}
}
