using System.ComponentModel.DataAnnotations;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class UserSessionStatus : IntIdentifiedBaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
