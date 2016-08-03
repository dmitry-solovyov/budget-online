using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("UserPasswords")]
    public class UserPasswordRecord : GuidIdentifiedBaseModel, ICreateTrakingModel
    {
        [Required]
        [Index("IX_UserPassword_UserId", IsClustered = false)]
        public Guid UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Required, Column(TypeName = "datetime2")]
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
