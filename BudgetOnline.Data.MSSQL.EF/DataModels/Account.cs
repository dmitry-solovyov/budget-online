using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class Account : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [Required]
        [Index("IX_Account_SectionId", IsClustered = false)]
        [Index("UX_Account_AccountSectionName", Order = 1, IsUnique = true, IsClustered = true)]
        public Guid SectionId { get; set; }

        [Required]
        [MaxLength(255)]
        [Index("UX_Account_AccountSectionName", Order = 2, IsUnique = true, IsClustered = true)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(512)]
        public string Icon { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsExternal { get; set; }

        [Required]
        public bool IsDisabled { get; set; }
        
        [Required, Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedWhen { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedUser { get; set; }
    }
}
