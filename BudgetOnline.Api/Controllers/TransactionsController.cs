using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;

namespace BudgetOnline.Api.Controllers
{
    public class TransactionsController : BaseApiAuthController
    {
        public ITransactionRepository TransactionRepository { get; set; }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var totals = TransactionRepository.GetList(
                CurrentUser.SectionId,
                new TransactionStatisticsSearchOptions
                    {
                        Date1 = DateTime.Now.AddDays(-30)
                    });

            return PrepareResponse(totals);
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var transaction = TransactionRepository.GetById(id);

            return PrepareResponse(transaction);
        }

        [HttpPost]
        public HttpResponseMessage Create()
        {
            var data = GetPostData();

            return PrepareResponse(HttpStatusCode.Accepted);
        }
    }
}
