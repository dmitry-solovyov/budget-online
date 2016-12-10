using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.Attributes
{
	public class ControlRowIdAttribute : Attribute, IMetadataAware
	{
		private readonly string _id;
		public ControlRowIdAttribute(string id)
		{
			_id = id;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["ControlRowId"] = _id;
		}

		public string Title
		{
			get { return _id; }
		}
	}
}
