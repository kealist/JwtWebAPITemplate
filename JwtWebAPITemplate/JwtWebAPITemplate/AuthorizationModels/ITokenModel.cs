using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwtWebAPITemplate.AuthorizationModels
{
    interface ITokenModel
    {
        string Generate(byte[] secret, Dictionary<string,Object> claims);
        ApplicationUser Verify(ApplicationUserManager userManager, byte[] secret, string jwt);
    }
}