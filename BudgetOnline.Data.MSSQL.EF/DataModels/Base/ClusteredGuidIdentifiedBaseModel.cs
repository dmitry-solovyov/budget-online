using System;
using System.ComponentModel.DataAnnotations;
using BudgetOnline.Data.MSSQL.EF.Attributes;

namespace BudgetOnline.Data.MSSQL.EF.DataModels.Base
{
    public abstract class ClusteredGuidIdentifiedBaseModel
    {
        [Key, ClusteredKey]
        [Required]
        public Guid Id { get; set; }
    }
}
