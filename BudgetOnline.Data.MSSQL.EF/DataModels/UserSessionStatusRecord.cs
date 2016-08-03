using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("UserSessionStatuses")]
    public class UserSessionStatusRecord : IntIdentifiedBaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
