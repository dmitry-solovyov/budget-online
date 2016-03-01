using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class Section : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [MaxLength(255)]
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedWhen { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedUser { get; set; }
    }
}
