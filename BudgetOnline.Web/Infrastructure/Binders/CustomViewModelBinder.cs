using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.Editors;
using BudgetOnline.UI.Models.SelectItems;
using BudgetOnline.Web.Models;

namespace BudgetOnline.Web.Infrastructure.Binders
{
	public class CustomViewModelBinder : DefaultModelBinder
	{
		protected override void BindProperty(
			ControllerContext controllerContext,
			ModelBindingContext bindingContext,
			PropertyDescriptor propertyDescriptor
			)
		{
			if (propertyDescriptor.PropertyType == typeof(CurrencyWithFormula))
			{
				BindCurrencyWithFormula(controllerContext, bindingContext, propertyDescriptor);
			}
			else if (propertyDescriptor.PropertyType == typeof(IdWithSelectList))
			{
				BindIdWithSelectList(controllerContext, bindingContext, propertyDescriptor);
			}
			else if (propertyDescriptor.PropertyType == typeof(CurrencyBundle))
			{
				BindCurrencyBundle(controllerContext, bindingContext, propertyDescriptor);
			}
			else if (propertyDescriptor.PropertyType == typeof(SelectList))
			{
				return;
			}
			else if (propertyDescriptor.PropertyType == typeof(ListWithMultiSelects))
			{
				BindListWithMultiSelects(controllerContext, bindingContext, propertyDescriptor);
			}
			else if (propertyDescriptor.PropertyType == typeof(DateTime))
			{
				BindDate(controllerContext, bindingContext, propertyDescriptor);
			}
			else if (propertyDescriptor.PropertyType == typeof(SelectItemsModel))
			{
				BindSelectItemsModel(controllerContext, bindingContext, propertyDescriptor);
			}
			else
			{
				base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
			}
		}


		private void BindCurrencyBundle(ControllerContext controllerContext, ModelBindingContext bindingContext,
										PropertyDescriptor propertyDescriptor)
		{
			var formula = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name + ".Formula"];
			var sum = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name + ".Sum"] ?? "0";
			var currency = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name + ".Currency"] ?? "0";
			var account = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name + ".Account"] ?? "0";

			if (!string.IsNullOrWhiteSpace(sum))
			{
				int currencyParsed;
				int accountParsed;
				decimal sumParsed;
				if (decimal.TryParse(sum, NumberStyles.Number, CultureInfo.CurrentUICulture, out sumParsed))
					if (int.TryParse(account, NumberStyles.Integer, CultureInfo.CurrentUICulture, out accountParsed))
						if (int.TryParse(currency, NumberStyles.Integer, CultureInfo.CurrentUICulture, out currencyParsed))
						{
							var value = new CurrencyBundle
											{
												Sum = sumParsed,
												Formula = formula,
												Account = new IdWithSelectList { Id = accountParsed },
												Currency = new IdWithSelectList { Id = currencyParsed },
											};

							propertyDescriptor.SetValue(bindingContext.Model, value);
						}
						else
							throw new FormatException("Invalid post data for CurrencyBundle type (Currency)");
					else
						throw new FormatException("Invalid post data for CurrencyBundle type (Currency)");
				else
					throw new FormatException("Invalid post data for CurrencyBundle type (Sum)");
			}
		}

		private void BindCurrencyWithFormula(ControllerContext controllerContext, ModelBindingContext bindingContext,
											 PropertyDescriptor propertyDescriptor)
		{
			var result = new CurrencyWithFormula();
			var formula = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name + ".Formula"];
			var value = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name + ".Value"];
			if (!string.IsNullOrWhiteSpace(value))
			{
				decimal sum;
				if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentUICulture, out sum))
					result = new CurrencyWithFormula
								{
									Sum = sum,
									Formula = formula,
								};
			}

			propertyDescriptor.SetValue(bindingContext.Model, result);

			return;
		}

		private void BindIdWithSelectList(ControllerContext controllerContext, ModelBindingContext bindingContext,
										  PropertyDescriptor propertyDescriptor)
		{
			var result = new IdWithSelectList();
			var value = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name];

			if (!string.IsNullOrWhiteSpace(value))
			{
				int id;
				if (int.TryParse(value, out id))
					result = new IdWithSelectList
								{
									Id = id,
								};
			}

			propertyDescriptor.SetValue(bindingContext.Model, result);
		}

		private void BindDate(ControllerContext controllerContext, ModelBindingContext bindingContext,
							  PropertyDescriptor propertyDescriptor)
		{
			var value = controllerContext.HttpContext.Request.Form[propertyDescriptor.Name] as string;
			if (!string.IsNullOrWhiteSpace(value))
			{
				DateTime dt;
				if (DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out dt))
					propertyDescriptor.SetValue(bindingContext.Model, dt);
			}
		}

		private void BindSelectItemsModel(ControllerContext controllerContext, ModelBindingContext bindingContext,
							  PropertyDescriptor propertyDescriptor)
		{
			var keys = controllerContext.HttpContext.Request.Form.AllKeys.Where(o => o.StartsWith(string.Format("{0}[", propertyDescriptor.Name))).ToList();

			var items = keys.Select(key => new SelectItemModel
											{
												Selected = true,
												Text = controllerContext.HttpContext.Request.Form[key],
												Value = key.Substring(string.Format("{0}[", propertyDescriptor.Name).Length).TrimEnd(']')
											}).ToList();

			propertyDescriptor.SetValue(bindingContext.Model, new SelectItemsModel(items));
		}


		private void BindListWithMultiSelects(ControllerContext controllerContext, ModelBindingContext bindingContext,
										  PropertyDescriptor propertyDescriptor)
		{
			var keys = controllerContext.HttpContext.Request.Form.AllKeys.Where(o => o.StartsWith(propertyDescriptor.Name + ".")).ToList();
			var result = keys.Select(key => new SelectListItem
												{
													Text = controllerContext.HttpContext.Request.Form[key],
													Value = key.Replace(propertyDescriptor.Name + ".", ""),
													Selected = true
												}).ToList();

			propertyDescriptor.SetValue(bindingContext.Model, new ListWithMultiSelects { Items = result });
		}

	}
}
