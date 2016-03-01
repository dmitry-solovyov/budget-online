using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class UserSession : GuidIdentifiedBaseModel, ICreateTrakingModel
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
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("UserSessionStatusId")]
        public virtual UserSessionStatus UserSessionStatus { get; set; }
        [ForeignKey("UserPasswordId")]
        public virtual UserPassword UserPassword { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
    }
}
