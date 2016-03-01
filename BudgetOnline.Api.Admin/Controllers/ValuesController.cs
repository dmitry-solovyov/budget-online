using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BudgetOnline.Data.Repositories;

namespace BudgetOnline.Api.Admin.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        public IUserRepository UserRepository { get; set; }

        [Route("values")]
        public IEnumerable<string> Get()
        {
            UserRepository = new UserRepository();
            return new [] { UserRepository.GetAll().Count().ToString() };
        }

        [Route("values/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
