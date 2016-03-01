using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class TransactionTagMap : GuidIdentifiedBaseModel, ICreateTrakingModel
    {
        [Index("IX_TransactionTagMap_GroupKey", IsClustered = true, Order = 1)]
        public Guid TransactionId { get; set; }

        [Index("IX_TransactionTagMap_GroupKey", IsClustered = true, Order = 2)]
        public Guid TagId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedWhen { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
    }
}
