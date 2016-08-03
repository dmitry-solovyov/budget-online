using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("Currencies")]
    public class CurrencyRecord : IntIdentifiedBaseModel
    {
        [Required, MaxLength(100)]
        [Index("UX_Currency_Name", IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(5)]
        public string Symbol { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(512)]
        public string Icon { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
