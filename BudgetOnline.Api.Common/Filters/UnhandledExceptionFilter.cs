using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Api.Common.Filters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public ILogWriter LogWriter { get; set; }

        public override void OnException(HttpActionExecutedContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception != null)
            {
                LogWriter.Error(context.Exception);

                var exType = context.Exception.GetType();

                if (exType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                else if (exType == typeof(ArgumentException))
                {
                    statusCode = HttpStatusCode.NotFound;
                }
            }

            var apiError = new ApiMessageError { Message = "Error occured" };

            // create a new response and attach our ApiError object
            // which now gets returned on ANY exception result
            var errorResponse = context.Request.CreateResponse(statusCode, apiError);
            context.Response = errorResponse;

            base.OnException(context);
        }
    }
}