using System;

namespace BudgetOnline.Data.Keys
{
    public struct GuidKeyField : IKeyField
    {
        public Guid Id { get; set; }
    }
}
