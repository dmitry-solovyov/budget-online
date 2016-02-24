using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetOnline.Data.MSSQL.EF.DataModels.Base
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
