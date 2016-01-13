using System;

namespace BudgetOnline.Api.Models
{
    public class TokenParts
    {
        public string UserName { get; set; }
        public Guid SessionId { get; set; }
    }
}