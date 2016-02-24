using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOnline.Data.MSSQL.EF.DataModels.Base;

namespace BudgetOnline.Data.MSSQL.EF.DataModels
{
    public class User : BaseModel, ICreateTrakingModel
    {
        public Guid? SectionId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsDisabled { get; set; }

        public DateTime CreatedWhen { get; set; }
        public Guid CreatedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Section Section { get; set; }
        
        [ForeignKey("CreatedBy")]
        public virtual User CreatedUser { get; set; }
    }
}
