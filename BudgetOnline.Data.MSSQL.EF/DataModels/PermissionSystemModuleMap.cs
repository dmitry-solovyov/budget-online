using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class PermissionSystemModuleMap : GuidIdentifiedBaseModel, ICreateTrakingModel
    {
        [Required]
        [Index("IX_PermissionSystemModuleMap_GroupKey", IsClustered = true, Order = 1)]
        public int PermissionId { get; set; }
        [Required]
        [Index("IX_PermissionSystemModuleMap_GroupKey", IsClustered = true, Order = 2)]
        public int SystemModuleId { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("SystemModuleId")]
        public virtual SystemModule SystemModule { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
    }
}
