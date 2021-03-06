﻿using System;

namespace BudgetOnline.Api.Common.Models
{
    public class SessionValidationRequest
    {
        public string Token { get; set; }
    }

    public class SessionValidationResponse
    {
        public bool Valid { get; set; }

        public DateTime ExpiredAfter { get; set; } 
    }
}