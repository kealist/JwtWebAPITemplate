using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwtWebAPITemplate.AuthorizationModels
{
    public class TokenModel : ITokenModel
    {
        public string Generate(byte[] secret, Dictionary<string, Object> claims)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser VerifyToken(ApplicationUserManager userManager, byte[] secret, string jwt)
        {
            throw new NotImplementedException();
        }
    }
}