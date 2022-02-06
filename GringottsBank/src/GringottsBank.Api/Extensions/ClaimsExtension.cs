using System;
using System.Security.Claims;

namespace GringottsBank.Api.Extensions
{
    public static class ClaimsExtension
    {
        public static Guid CustomerId(this ClaimsPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            var customerId = claimsIdentity?.FindFirst("CustomerId")?.Value;
            return customerId == null ? Guid.Empty : Guid.Parse(customerId);
        }
    }
}