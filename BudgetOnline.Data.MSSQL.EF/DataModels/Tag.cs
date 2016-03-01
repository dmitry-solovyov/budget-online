using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class Tag : GuidIdentifiedBaseModel, ICreateTrakingModel
    {
        [Required, MaxLength(100)]
        [Index("UX_Tag_Name", IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Icon { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required, Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
    }
}
