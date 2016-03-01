using System.ComponentModel.DataAnnotations;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class OperationType : IntIdentifiedBaseModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
