using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("Users")]
    public class UserRecord : GuidIdentifiedBaseModel, ICreateTrakingModel
    {
        [Index("IX_User_SectionId", IsClustered = false)]
        public Guid? SectionId { get; set; }

        [Required]
        [MaxLength(255)]
        [Index("IX_UserEmail", IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ValidFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }

        [MaxLength(1024)]
        public string SecretQuestion { get; set; }
        [MaxLength(1024)]
        public string SecretQuestionAnswer { get; set; }

        public bool IsLocked { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LockStarted { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("SectionId")]
        public virtual SectionRecord Section { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserRecord CreatedUser { get; set; }
    }
}
