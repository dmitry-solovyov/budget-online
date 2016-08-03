using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("OperationTypes")]
    public class OperationTypeRecord : IntIdentifiedBaseModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
