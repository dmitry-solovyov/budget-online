using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace BudgetOnline.Api.Infrastructure.Filters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            var exType = context.Exception.GetType();

            if (exType == typeof(UnauthorizedAccessException))
                status = HttpStatusCode.Unauthorized;
            else if (exType == typeof(ArgumentException))
                status = HttpStatusCode.NotFound;

            var apiError = new ApiMessageError { Message = context.Exception.Message };

            // create a new response and attach our ApiError object
            // which now gets returned on ANY exception result
            var errorResponse = context.Request.CreateResponse(status, apiError);
            context.Response = errorResponse;

            base.OnException(context);
        }
    }
}