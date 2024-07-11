using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Serilog;

namespace Library_Management_System_BackEnd.Extensions
{
    public static class GetClaimData
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.SingleOrDefault(claim =>
                claim.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            );
            return claim!.Value;
        }
    }
}
