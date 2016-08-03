using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("OperationTypeSectionMaps")]
    public class OperationTypeSectionMapRecord : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [Index("IX_OperationTypeSectionMap_GroupKey", Order = 1)]
        [Required]
        public Guid SectionId { get; set; }
        [Index("IX_OperationTypeSectionMap_GroupKey", Order = 2)]
        [Required]
        public int OperationTypeId { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedWhen { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("SectionId")]
        public virtual SectionRecord Section { get; set; }
        [ForeignKey("OperationTypeId")]
        public virtual OperationTypeRecord OperationType { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserRecord CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual UserRecord UpdatedUser { get; set; }
    }
}
