using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Data.MSSQL.EF.DataModels.Base
{
    public abstract class IntIdentifiedBaseModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
