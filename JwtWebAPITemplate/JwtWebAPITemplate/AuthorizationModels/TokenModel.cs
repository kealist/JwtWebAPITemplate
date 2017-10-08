using Jose;
using JwtWebAPITemplate.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwtWebAPITemplate.AuthorizationModels
{
    public class TokenModel : ITokenModel
    {
        //Todo Add fields as desired for claims to verify

        //For example:


        public string Generate(byte[] secret, Dictionary<string, Object> claims)
        {
            return Jose.JWT.Encode(claims, secret, JwsAlgorithm.HS256);
        }

        public ApplicationUser Verify(ApplicationUserManager userManager, byte[] secret, string jwt)
        {
            try
            {
                //Make sure token is valid
                string jsonString = JWT.Decode(jwt, secret, JwsAlgorithm.HS256);
                var jwtToken = JsonConvert.DeserializeObject<TokenModel>(jsonString);
                ApplicationUser user;

                //Todo: Verify claims as desired.  Delete the line below and find the user in your database as needed
                user = null;

                return user;
            }
            catch //Todo: Specify exception types
            {
                return null;
            }
        }
    }
}