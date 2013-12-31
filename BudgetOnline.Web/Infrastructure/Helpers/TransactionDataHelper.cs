using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Common.Enums;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
    public class TransactionDataHelper : ITransactionDataHelper
    {
        public string NormalizeTags(string tags)
        {
            if (string.IsNullOrWhiteSpace(tags))
                return tags;

            return string.Join(", ", ParseStringWithTags(tags));
        }

        public IEnumerable<string> ParseStringWithTags(string tags)
        {
            if (string.IsNullOrWhiteSpace(tags))
                return Enumerable.Empty<string>();

            return tags
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Trim())
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .Distinct();
        }

        public decimal GetRealSumValue(TransactionTypes transactionType, decimal sum)
        {
            return GetRealSumValue((int)transactionType, sum);
        }

        public decimal GetRealSumValue(int transactionTypeId, decimal sum)
        {
            switch (transactionTypeId)
            {
                case (int)TransactionTypes.Outcome:
                    return -Math.Abs(sum);

                case (int)TransactionTypes.Income:
                    return Math.Abs(sum);

                default:
                    return sum;
            }
        }
    }
}