using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    [Table("Categories")]
    public class CategoryRecord : ClusteredGuidIdentifiedBaseModel
    {
        [Index("IX_Category_ParentId")]
        public Guid? ParentId { get; set; }

        [Required]
        [MaxLength(255)]
        [Index("UX_Category_Name", IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(512)]
        public string Icon { get; set; }

        [Required]
        public bool IsDisabled { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("ParentId")]
        public virtual CategoryRecord Parent { get; set; }
    }
}
