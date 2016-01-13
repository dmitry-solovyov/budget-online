using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.PreCompiled.Attributes
{
	public class HiddenLabelAttribute : Attribute, IMetadataAware
	{
		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["HiddenLabel"] = "HiddenLabel";
		}
	}
}
