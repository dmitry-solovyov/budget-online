using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Common.Enums
{
    public enum TransactionTypes
    {
		//[Display(Name = "")]
		//Unknown = 0,
        [Display(Name = "Доход")]
        Income = 1,
        [Display(Name = "Расход")]
        Outcome,
        [Display(Name = "Перевод")]
        Transfer,
        [Display(Name = "Обмен")]
        Exchange
    }
}
