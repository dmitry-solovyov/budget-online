using System;

namespace BudgetOnline.Data.MSSQL.EF.DataModels.Base
{
    public interface ILastUpdateTrakingModel
    {
        DateTime? UpdatedWhen { get; set; }
        Guid? UpdatedBy { get; set; }
        User UpdatedUser { get; set; }
    }
}
