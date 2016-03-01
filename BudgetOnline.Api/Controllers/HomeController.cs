using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.Common.Controllers;

namespace BudgetOnline.Api.Controllers
{
    public class HomeController : BaseApiAuthController
    {
        [HttpPost]
        [ActionName("me")]
        public HttpResponseMessage MePost()
        {
            var user = GetPostData<User>();
            if (user == null)
                return PrepareResponse(null, HttpStatusCode.BadRequest);

            return PrepareResponse(new { Saved = true });
        }
    }
    public class User
    {
        public string Name { get; set; }
    }
}
