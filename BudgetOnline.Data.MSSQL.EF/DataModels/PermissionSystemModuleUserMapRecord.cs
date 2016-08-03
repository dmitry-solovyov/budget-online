using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("PermissionSystemModuleUserMaps")]
    public class PermissionSystemModuleUserMapRecord : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [Required]
        [Index("IX_PermissionSystemModuleUserMap_GroupKey", IsClustered = false, Order = 1)]
        public int PermissionId { get; set; }
        [Required]
        [Index("IX_PermissionSystemModuleUserMap_GroupKey", IsClustered = false, Order = 2)]
        public int SystemModuleId { get; set; }
        [Required]
        [Index("IX_PermissionSystemModuleUserMap_GroupKey", IsClustered = false, Order = 3)]
        public Guid UserId { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedWhen { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("PermissionId")]
        public virtual PermissionRecord Permission { get; set; }
        [ForeignKey("SystemModuleId")]
        public virtual SystemModuleRecord SystemModule { get; set; }
        [ForeignKey("UserId")]
        public virtual UserRecord User { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserRecord CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual UserRecord UpdatedUser { get; set; }
    }
}
