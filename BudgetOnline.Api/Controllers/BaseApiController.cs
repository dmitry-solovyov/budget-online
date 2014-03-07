using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.Infrastructure.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BudgetOnline.Api.Controllers
{
    //[ForceHttps]
    [UnhandledExceptionFilter]
    public class BaseApiController : ApiController
    {
        protected T GetPostData<T>()
        {
            return Request.Content
                .ReadAsStringAsync()
                .ContinueWith(content => JsonConvert.DeserializeObject<T>(content.Result))
                .Result;
        }

        protected object GetPostData()
        {
            return Request.Content
                .ReadAsStringAsync()
                .ContinueWith(content => JsonConvert.DeserializeObject(content.Result))
                .Result;
        }

        private void UpdateContentType(HttpResponseMessage response, string contentType = "application/json")
        {
            if (response.Headers.Contains("ContentType"))
                response.Headers.Remove("ContentType");

            response.Headers.Add("ContentType", contentType);
        }

        protected HttpResponseMessage PrepareResponse(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage(statusCode);

            UpdateContentType(response);

            return response;
        }

        protected HttpResponseMessage PrepareResponse(object data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            string json =
              JsonConvert.SerializeObject(
                data,
                Formatting.Indented,
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
              );

            var response = new HttpResponseMessage(statusCode) { Content = new StringContent(json) };

            UpdateContentType(response);

            return response;
        }

    }
}
