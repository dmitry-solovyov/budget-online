using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Web.Controllers.Api
{
    public class TransactionsController : ApiController
    {
        public ITransactionRepository TransactionRepository { get; set; }

        public IEnumerable<Transaction> Get()
        {
            var ts = (TransactionRepository ?? DependencyResolver.Current.GetService<ITransactionRepository>()).GetList(1, new TransactionSearchOptions { Date1 = DateTime.Today.AddDays(-7) });

            if (ts == null)
                yield return null;

            foreach (var transaction in ts)
                yield return transaction;
        }

        public Transaction Get(int id)
        {
            var t = (TransactionRepository ?? DependencyResolver.Current.GetService<ITransactionRepository>()).GetById(id);

            if (t == null)
                return null;

            return t;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}