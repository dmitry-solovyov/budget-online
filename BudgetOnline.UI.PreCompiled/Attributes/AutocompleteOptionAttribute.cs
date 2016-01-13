using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.PreCompiled.Attributes
{
	public class AutocompleteOptionAttribute : Attribute, IMetadataAware
	{
		private string _action;
		public string Action { get { return _action; } }
		
		private string _controller;
		public string Controller { get { return _controller; } }
		
		private string _area;
		public string Area { get { return _area; } }



		public AutocompleteOptionAttribute(string action)
			: this(action, null, null)
		{
		}

		public AutocompleteOptionAttribute(string action, string controller)
			: this(action, controller, null)
		{
		}

		public AutocompleteOptionAttribute(string action, string controller, string area)
		{
			_action = action;
			_controller = controller;
			_area = area;
		}


		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["AutocompleteAction"] = _action;
			metadata.AdditionalValues["AutocompleteController"] = _controller;
			metadata.AdditionalValues["AutocompleteArea"] = _area;
		}
	}
}
