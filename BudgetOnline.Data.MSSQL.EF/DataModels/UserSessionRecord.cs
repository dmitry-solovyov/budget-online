using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("UserSessions")]
    public class UserSessionRecord : GuidIdentifiedBaseModel
    {
        [Index("IX_UserSession_GroupKey", IsClustered = true, Order = 1)]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid UserPasswordId { get; set; }

        [Required]
        public int UserSessionStatusId { get; set; }

        [Index("IX_UserSession_GroupKey", IsClustered = true, Order = 2)]
        [Required, Column(TypeName = "datetime2")]
        public DateTime CreatedWhen { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime ExpiresWhen { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("UserSessionStatusId")]
        public virtual UserSessionStatusRecord UserSessionStatus { get; set; }
        [ForeignKey("UserPasswordId")]
        public virtual UserPasswordRecord UserPassword { get; set; }
        [ForeignKey("UserId")]
        public virtual UserRecord User { get; set; }
    }
}
