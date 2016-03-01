using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class TransactionCorrectionDetail : GuidIdentifiedBaseModel, ICreateTrakingModel, ILastUpdateTrakingModel
    {
        [Index("IX_TransactionCorrectionDetail_SectionId", IsClustered = false)]
        public Guid SectionId { get; set; }

        [Index("IX_TransactionCorrectionDetail_TransactionId", IsClustered = true)]
        public Guid TransactionId { get; set; }

        [Index("IX_TransactionCorrectionDetail_TransactionDetailId", IsClustered = false)]
        public Guid TransactionDetailId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public double TotalSum { get; set; }

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

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
        [ForeignKey("TransactionDetailId")]
        public virtual TransactionDetail TransactionDetail { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedUser { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
    }
}
