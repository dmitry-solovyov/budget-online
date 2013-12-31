using System.Web.Mvc;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Attributes
{
	public class GlobalExecuteActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly object locker = new object();

		private static ILogWriter _logWriter;
		public static ILogWriter LogWriter
		{
			get
			{
				if (_logWriter == null)
				{
					lock (locker)
					{
						if (_logWriter == null)
						{
							_logWriter = DependencyResolver.Current.GetService<ILogWriter>();
						}
					}
				}

				return _logWriter;
			}
		}

		public override void OnResultExecuting(ResultExecutingContext context)
		{
			base.OnResultExecuting(context);

			if (Constants.TraceRequests)
				LogWriter.TraceFormat(
					"Controller: {1}, Url: {0}",
					context.RequestContext.HttpContext.Request.Url.PathAndQuery,
					context.Controller);
		}
	}
}