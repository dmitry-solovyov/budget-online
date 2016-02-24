using System;

namespace BudgetOnline.Api.Exceptions
{
    public class ApiOperationException : Exception
    {
        public ApiOperationException(string message)
            : base(message)
        {
            
        }
    }
}