using System;
using BudgetOnline.Data.Models.BaseModels;

namespace BudgetOnline.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public GuidRef Section { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string SecretQuestion { get; set; }

        public string SecretQuestionAnswer { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockStarted { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreatedWhen { get; set; }

        public GuidRef CreatedUser { get; set; }
    }
}