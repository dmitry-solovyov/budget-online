using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BudgetOnline.Api.Common.Controllers;
using BudgetOnline.Api.Exceptions;
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
        public ITransactionLinkRepository TransactionLinkRepository { get; set; }
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

        [HttpPost]
        [Route("filter")]
        public HttpResponseMessage FilterPost(TransactionStatisticsSearchOptions options)
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
        [Route("create/income")]
        public HttpResponseMessage CreateIncode()
        {
            var data = GetPostData<TransactionMoveViewModel>();
            if (data == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (!data.Sum.HasValue)
                    return PrepareResponse(new { Message = "Sum should be specified" }, HttpStatusCode.BadRequest);

                var categoryId = ResolveCategory(data.Category);
                var currencyId = ResolveCurrency(data.Currency);
                var accountId = ResolveAccount(data.Account);

                if (accountId > 0)
                {
                    var transaction = new Transaction
                    {
                        TransactionTypeId = (int)TransactionTypes.Expense,
                        Id = data.Id,
                        SectionId = CurrentApiUserProvider.CurrentSession.User.SectionId,
                        Date = data.Date,
                        AccountId = accountId,
                        CategoryId = categoryId > 0 ? categoryId : (int?)null,
                        Amount = 1,
                        Sum = Math.Abs(data.Sum.Value),
                        Formula = Math.Abs(data.Sum.Value).ToString(CultureInfo.CurrentCulture),
                        CurrencyId = currencyId,
                        Description = data.Description,
                        Tags = data.Tags,
                        IsDisabled = data.IsDisabled,
                        CreatedBy = CurrentApiUserProvider.CurrentSession.User.Id,
                        CreatedWhen = DateTime.UtcNow
                    };

                    TransactionRepository.Insert(transaction);
                }
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex);

                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            return PrepareResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Route("create/expense")]
        public HttpResponseMessage CreateExpense()
        {
            var data = GetPostData<TransactionMoveViewModel>();
            if (data == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (!data.Sum.HasValue)
                    return PrepareResponse(new { Message = "Sum should be specified" }, HttpStatusCode.BadRequest);

                var categoryId = ResolveCategory(data.Category);
                var currencyId = ResolveCurrency(data.Currency);
                var accountId = ResolveAccount(data.Account);

                if (accountId > 0)
                {
                    var transaction = new Transaction
                    {
                        TransactionTypeId = (int)TransactionTypes.Expense,
                        Id = data.Id,
                        SectionId = CurrentApiUserProvider.CurrentSession.User.SectionId,
                        Date = data.Date,
                        AccountId = accountId,
                        CategoryId = categoryId > 0 ? categoryId : (int?)null,
                        Amount = 1,
                        Sum = -Math.Abs(data.Sum.Value),
                        Formula = Math.Abs(data.Sum.Value).ToString(CultureInfo.CurrentCulture),
                        CurrencyId = currencyId,
                        Description = data.Description,
                        Tags = data.Tags,
                        IsDisabled = data.IsDisabled,
                        CreatedBy = CurrentApiUserProvider.CurrentSession.User.Id,
                        CreatedWhen = DateTime.UtcNow
                    };

                    TransactionRepository.Insert(transaction);
                }
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex);

                return PrepareResponse(new { ex.Message }, HttpStatusCode.BadRequest);
            }

            return PrepareResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Route("create/transfer")]
        public HttpResponseMessage CreateTransfer()
        {
            var data = GetPostData<TransactionMoveViewModel>();
            if (data == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (!data.Sum.HasValue)
                    return PrepareResponse(new { Message = "Sum should be specified" }, HttpStatusCode.BadRequest);

                var categoryId = ResolveCategory(data.Category);
                var currencyId = ResolveCurrency(data.Currency);
                var accountFromId = ResolveAccount(data.AccountFrom);
                var accountToId = ResolveAccount(data.AccountTo);

                if (accountFromId > 0)
                {
                    var transaction1 = new Transaction
                    {
                        TransactionTypeId = (int)TransactionTypes.Expense,
                        Id = 0,
                        SectionId = CurrentApiUserProvider.CurrentSession.User.SectionId,
                        Date = data.Date,
                        AccountId = accountFromId,
                        CategoryId = categoryId,
                        Amount = 1,
                        Sum = -Math.Abs(data.Sum.Value),
                        Formula = Math.Abs(data.Sum.Value).ToString(CultureInfo.CurrentCulture),
                        CurrencyId = currencyId,
                        Description = data.Description,
                        Tags = data.Tags,
                        IsDisabled = data.IsDisabled,
                        CreatedBy = CurrentApiUserProvider.CurrentSession.User.Id,
                        CreatedWhen = DateTime.UtcNow
                    };

                    transaction1 = TransactionRepository.Insert(transaction1);

                    var transaction2 = new Transaction
                    {
                        TransactionTypeId = (int)TransactionTypes.Expense,
                        Id = 0,
                        LinkedId = transaction1.Id,
                        SectionId = CurrentApiUserProvider.CurrentSession.User.SectionId,
                        Date = data.Date,
                        AccountId = accountToId,
                        CategoryId = categoryId,
                        Amount = 1,
                        Sum = Math.Abs(data.Sum.Value),
                        Formula = Math.Abs(data.Sum.Value).ToString(CultureInfo.CurrentCulture),
                        CurrencyId = currencyId,
                        Description = data.Description,
                        Tags = data.Tags,
                        IsDisabled = data.IsDisabled,
                        CreatedBy = CurrentApiUserProvider.CurrentSession.User.Id,
                        CreatedWhen = DateTime.UtcNow
                    };

                    transaction2 = TransactionRepository.Insert(transaction2);

                    TransactionLinkRepository.Insert(new TransactionLink
                    {
                        ParentId = transaction1.Id,
                        ChildId = transaction2.Id,
                        CreatedBy = CurrentApiUserProvider.CurrentSession.User.Id,
                        CreatedWhen = DateTime.UtcNow,
                    });
                }
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex);

                return PrepareResponse(new { ex.Message }, HttpStatusCode.BadRequest);
            }

            return PrepareResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Route("create/exchange")]
        public HttpResponseMessage UpsetExchange()
        {
            var data = GetPostData<TransactionMoveViewModel>();
            if (data == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var categoryId = ResolveCategory(data.Category);
                var currencyId = ResolveCurrency(data.Currency);
                var accountFromId = ResolveAccount(data.AccountFrom);
                var accountToId = ResolveAccount(data.AccountTo);

                if (accountFromId > 0)
                {
                    var transaction1 = new Transaction
                    {
                        TransactionTypeId = (int)TransactionTypes.Expense,
                        Id = data.Id,
                        SectionId = CurrentApiUserProvider.CurrentSession.User.SectionId,
                        Date = data.Date,
                        AccountId = accountFromId,
                        CategoryId = categoryId,
                        Amount = 1,
                        Sum = -Math.Abs(data.Sum.Value),
                        Formula = Math.Abs(data.Sum.Value).ToString(CultureInfo.CurrentCulture),
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

        private int ResolveCategory(string categoryName)
        {
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                var category = Dictionaries.Categories().FirstOrDefault(x => x.Name == categoryName);
                if (category != null)
                {
                    return category.Id;
                }
                throw new ApiOperationException("Category is not found!");
            }
            return 0;
        }

        private int ResolveCurrency(string currencyName)
        {
            if (!string.IsNullOrWhiteSpace(currencyName))
            {
                var currency = Dictionaries.Currencies().FirstOrDefault(x => x.Name.ToLower() == currencyName.ToLower() || x.Symbol.ToLower() == currencyName.ToLower());
                if (currency != null)
                {
                    return currency.Id;
                }
                throw new ApiOperationException("Currency is not found!");
            }
            return 0;
        }

        private int ResolveAccount(string accountName)
        {
            if (!string.IsNullOrWhiteSpace(accountName))
            {
                var account = Dictionaries.Accounts().FirstOrDefault(x => x.Name == accountName);
                if (account != null)
                {
                    return account.Id;
                }
                throw new ApiOperationException("Account is not found!");
            }
            return 0;
        }
    }
}
