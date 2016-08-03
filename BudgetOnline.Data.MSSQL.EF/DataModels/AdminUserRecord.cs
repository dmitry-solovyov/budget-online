using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("AdminUsers")]
    public class AdminUserRecord : GuidIdentifiedBaseModel, ICreateTrakingModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        
        [ForeignKey("UserId")]
        public virtual UserRecord User { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserRecord CreatedUser { get; set; }
    }
}
