using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("SystemModules")]
    public class SystemModuleRecord : IntIdentifiedBaseModel
    {
        [Required, MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
