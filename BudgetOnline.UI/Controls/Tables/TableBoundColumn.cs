using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Web;

namespace BudgetOnline.UI.Controls.Tables
{
	public class TableBoundColumnBuilder<T> : TableBaseColumnBuilder<T, TableBoundColumnBuilder<T>>
		where T : class
	{

		private Type _memberType;

		public TableBoundColumnBuilder()
		{
		}

		public TableBoundColumnBuilder(Expression<Func<T, object>> binding)
		{
			_bindExpression = binding;
			_memberType = GetMemberType();
		}

		private Func<object, string> _formatter;
		public virtual TableBoundColumnBuilder<T> Formatter<TTargetType>(Expression<Func<TTargetType, string>> formatter)
		{
			_formatter = o => formatter.Compile().Invoke((TTargetType)o);
			return this;
		}

		private Expression<Func<T, object>> _bindExpression;
		public virtual TableBoundColumnBuilder<T> Binding(Expression<Func<T, object>> binding)
		{
			_bindExpression = binding;
			_memberType = GetMemberType();

			return this;
		}

		protected override HtmlString BuildHeader(TableDefinitions tableDefinition, T context)
		{
			var caption = string.Empty;
			var attribute = GetAttribute<DisplayNameAttribute>();
			if (attribute != null)
			{
				caption = attribute.DisplayName;
			}

			return new HtmlString(string.Format("<{2}{1}>{0}</{2}>", caption, GetHeaderClass(tableDefinition, context), tableDefinition.HeaderCellTag));
		}

		protected override HtmlString BuildCell(TableDefinitions tableDefinition, T context)
		{
			return new HtmlString(string.Format("<{2}{1}>{0}</{2}>", BuildCellContent(context), GetCellClass(tableDefinition, context), tableDefinition.BodyCellTag));
		}

		private TAttr GetAttribute<TAttr>()
			where TAttr : class
		{
			var memberExpression = _bindExpression.Body as MemberExpression;
			if (memberExpression != null)
			{
				var attributes = memberExpression.Member.GetCustomAttributes(typeof(TAttr), true);
				if (attributes.Length > 0)
				{
					return attributes[0] as TAttr;
				}
			}
			else
			{
				var unaryExpression = _bindExpression.Body as UnaryExpression;
				if (unaryExpression != null)
				{
					memberExpression = unaryExpression.Operand as MemberExpression;
					if (memberExpression != null)
					{
						var attributes = memberExpression.Member.GetCustomAttributes(typeof(TAttr), true);
						if (attributes.Length > 0)
						{
							return attributes[0] as TAttr;
						}
					}
				}
			}

			return null;
		}

		private Type GetMemberType()
		{
			var memberExpression = _bindExpression.Body as MemberExpression;
			if (memberExpression != null)
			{
				return memberExpression.Type;
			}

			var unaryExpression = _bindExpression.Body as UnaryExpression;
			if (unaryExpression != null)
			{
				memberExpression = unaryExpression.Operand as MemberExpression;
				if (memberExpression != null)
				{
					return memberExpression.Type;
				}
			}

			return null;
		}

		private string BuildCellContent(T context)
		{
			var expr = _bindExpression.Compile();
			var value = expr.Invoke(context);

			if (value == null)
				return string.Empty;

			if (_formatter != null)
			{
				try
				{
					//var tmp = Convert.ChangeType(value, _targetType);
					return _formatter(value);
					//return tmp.ToString();
				}
				catch
				{
				    //ignore
				}
			}

			if (_memberType.GUID == typeof(decimal).GUID)
				return Convert.ToDecimal(value).ToString(CultureInfo.CurrentUICulture);

			if (_memberType.GUID == typeof(bool).GUID)
				return Convert.ToBoolean(value) ? "<i class=\"icon-check\"></i>" : string.Empty;

			if (_memberType.GUID == typeof(DateTime).GUID)
			{
				var d = Convert.ToDateTime(value).ToLocalTime();
				return d.ToShortDateString();
			}

			return value.ToString();
		}
	}
}
