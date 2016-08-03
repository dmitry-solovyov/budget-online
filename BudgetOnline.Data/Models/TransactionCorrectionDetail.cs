using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class TransactionCorrectionDetail
    {
        public GuidRef Section { get; set; }

        public Guid TransactionId { get; set; }

        public Guid TransactionDetailId { get; set; }

        public DateTime Date { get; set; }

        public GuidRef Account { get; set; }

        public IntRef Currency { get; set; }

        public double TotalSum { get; set; }

        public DateTime CreatedWhen { get; set; }

        public GuidRef CreatedUser { get; set; }

        public DateTime? UpdatedWhen { get; set; }

        public GuidRef UpdatedUser { get; set; }
    }
}
