using System.Collections.Generic;

namespace BudgetOnline.Data.QueryManagement
{
    public class OperationsGroup
    {
        public OperationsGroupTypes Type { get; set; }

        public ICollection<QueryOperation> Operations { get; set; }
    }
}
