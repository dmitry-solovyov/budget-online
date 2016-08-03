using System;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class Transaction
    {
        public GuidRef Section { get; set; }

        public DateTime Date { get; set; }

        public GuidRef Category { get; set; }

        public OperationTypes OperationType { get; set; }

        public string Description { get; set; }

        public string Formula { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedWhen { get; set; }

        public GuidRef CreatedUser { get; set; }

        public DateTime? UpdatedWhen { get; set; }

        public GuidRef UpdatedUser { get; set; }
    }
}
