using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.Models;
using BudgetOnline.Api.ViewModels;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Api.Controllers
{
    [RoutePrefix("transactions")]
    public class TransactionsController : BaseApiAuthController
    {
        public ILogWriter LogWriter { get; set; }
        public ITransactionRepository TransactionRepository { get; set; }
        public IDictionaries Dictionaries { get; set; }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            var totals = TransactionRepository.GetList(
                CurrentSession.User.SectionId,
                new TransactionStatisticsSearchOptions
                    {
                        Date1 = DateTime.Now.AddDays(-30)
                    });

            return PrepareResponse(totals);
        }

        [HttpGet]
        [Route("filter")]
        public HttpResponseMessage Filter([FromUri]TransactionStatisticsSearchOptions options)
        {
            var itemsCount = TransactionRepository.GetListLength(CurrentSession.User.SectionId, options);
            var items = TransactionRepository.GetList(CurrentSession.User.SectionId, options);

            return PrepareResponse(new PagedResult<Transaction>(items, itemsCount, options.PageSize ?? 10, options.PageNumber ?? 1));
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var transaction = TransactionRepository.GetById(id);

            return PrepareResponse(transaction);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateOrUpdate()
        {
            var data = GetPostData<TransactionMoveViewModel>();
            if (data == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                Transaction transaction1 = null;
                Transaction transaction2 = null;

                var categoryId = 0;
                if (!string.IsNullOrWhiteSpace(data.Category))
                {
                    var category = Dictionaries.Categories().FirstOrDefault(x => x.Name == data.Category);
                    if (category != null)
                    {
                        categoryId = category.Id;
                    }
                }

                var currencyId = 0;
                if (!string.IsNullOrWhiteSpace(data.Category))
                {
                    var currency = Dictionaries.Currencies().FirstOrDefault(x => x.Name == data.Currency || x.Symbol == data.Currency);
                    if (currency != null)
                    {
                        currencyId = currency.Id;
                    }
                }

                var accounts = Dictionaries.Accounts().ToArray();

                int accountFromId = 0, accountToId = 0;
                if (!string.IsNullOrWhiteSpace(data.AccountFrom))
                {
                    var accountFrom = accounts.FirstOrDefault(x => x.Name == data.AccountFrom);
                    if (accountFrom != null)
                    {
                        accountFromId = accountFrom.Id;
                    }
                }
                if (!string.IsNullOrWhiteSpace(data.AccountTo))
                {
                    var accountTo = accounts.OrderBy(x => !x.IsDisabled).FirstOrDefault(x => x.Name == data.AccountTo);
                    if (accountTo != null)
                    {
                        accountToId = accountTo.Id;
                    }
                }

                if (accountFromId > 0)
                {
                    transaction1 = new Transaction
                    {
                        TransactionTypeId = (int)TransactionTypes.Outcome,
                        Id = data.Id,
                        SectionId = CurrentApiUserProvider.CurrentSession.User.SectionId,
                        Date = data.Date,
                        AccountId = accountFromId,
                        CategoryId = categoryId,
                        Amount = 1,
                        Sum = -Math.Abs(data.Sum),
                        Formula = Math.Abs(data.Sum).ToString(CultureInfo.CurrentCulture),
                        CurrencyId = currencyId,
                        Description = data.Description,
                        Tags = data.Tags,
                        IsDisabled = data.IsDisabled,
                        CreatedBy = CurrentApiUserProvider.CurrentSession.User.Id,
                        CreatedWhen = DateTime.UtcNow
                    };

                    transaction1 = TransactionRepository.Insert(transaction1);
                }
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex);

                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            return PrepareResponse(HttpStatusCode.Accepted);
        }
    }
}
