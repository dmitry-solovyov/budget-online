using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
    public class StatisticsTotalOnDate
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public DateTime TotalOnDate { get; set; }
        public DateTime CreatedWhen { get; set; }
        public int? CreatedBy { get; set; }
    }
}
