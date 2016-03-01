﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class Transaction : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [Index("IX_Transaction_SectionId", IsClustered = false)]
        public Guid SectionId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Index("IX_Transaction_CategoryId")]
        [Required]
        public Guid CategoryId { get; set; }

        [Index("IX_Transaction_OperationTypeId", IsClustered = false)]
        [Required]
        public int OperationTypeId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Formula { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedWhen { get; set; }
        public Guid? UpdatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedUser { get; set; }
        [ForeignKey("OperationTypeId")]
        public virtual OperationType OperationType { get; set; }
    }
}
