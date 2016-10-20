using System.Web;
using System.Web.Mvc;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Attributes
{
	public class HandleViewErrorsAttribute : HandleErrorAttribute
	{
		private readonly ILogWriter _logger = DependencyResolver.Current.GetService<ILogWriter>();

		public override void OnException(ExceptionContext filterContext)
		{
			_logger.Error(
				string.Format("Url: " + HttpContext.Current.Request.Url.PathAndQuery),
				filterContext.Exception);

			base.OnException(filterContext);
		}
	}
}