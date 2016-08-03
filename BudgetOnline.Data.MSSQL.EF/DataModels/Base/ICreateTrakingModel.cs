using System;

namespace BudgetOnline.Data.MSSQL.EF.DataModels.Base
{
    public interface ICreateTrakingModel
    {
        DateTime CreatedWhen { get; set; }
        Guid CreatedBy { get; set; }
    }
}
