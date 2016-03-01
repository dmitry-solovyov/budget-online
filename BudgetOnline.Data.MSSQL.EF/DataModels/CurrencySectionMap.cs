using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class CurrencySectionMap : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [Index("IX_CurrencySectionMap_GroupKey", Order = 1)]
        [Required]
        public Guid SectionId { get; set; }
        [Index("IX_CurrencySectionMap_GroupKey", Order = 2)]
        [Required]
        public int CurrencyId { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedWhen { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public User UpdatedUser { get; set; }
    }
}
