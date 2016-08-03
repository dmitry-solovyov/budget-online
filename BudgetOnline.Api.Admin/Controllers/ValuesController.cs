using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BudgetOnline.Data.Contracts;
using BudgetOnline.Data.Keys;
using BudgetOnline.Data.Models;

namespace BudgetOnline.Api.Admin.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        public IRepository<User> UserRepository { get; set; }

        [Route("values")]
        public IEnumerable<string> Get()
        {
            return new [] { UserRepository.GetAll().FirstOrDefault().Email };
        }

        [Route("values/{id}")]
        public string Get(int id)
        {
            return UserRepository.GetById(new GuidKeyField { Id = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}") }).Email;
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
