﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Application.Features.Posting.Services
{
    public interface IUserIdentityService
    {
        Task<string> GetCurrentLoggedInUserEmailAsync();
        Task<Guid?> GetCurrentLoggedInUserGuidAsync();
    }
}
