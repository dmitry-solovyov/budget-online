using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class TransactionTagMap
    {
        public Guid TransactionId { get; set; }

        public GuidRef Tag { get; set; }

        public DateTime CreatedWhen { get; set; }

        public GuidRef CreatedUser { get; set; }
    }
}
