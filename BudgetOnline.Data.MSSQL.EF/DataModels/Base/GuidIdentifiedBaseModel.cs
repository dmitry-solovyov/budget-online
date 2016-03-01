using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Data.MSSQL.EF.DataModels.Base
{
    public abstract class GuidIdentifiedBaseModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
