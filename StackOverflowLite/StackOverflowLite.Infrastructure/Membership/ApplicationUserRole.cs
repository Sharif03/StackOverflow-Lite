using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace StackOverflowLite.Infrastructure.Membership
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
       
    }
}
