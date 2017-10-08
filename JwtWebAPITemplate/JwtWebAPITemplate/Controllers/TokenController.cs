using JwtWebAPITemplate.AuthorizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JwtWebAPITemplate.Controllers
{
    public class TokenController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> PostAsync([FromBody]Login login)
        {
            var user = await this.AppUserManager.FindAsync(login.Username, login.Password);
            if (user != null)
            {
                // Todo: Deal with Session creation if you want to go this route

                // Todo: Use a secure method of generating/acquiring a secret
                // Todo: NO SERIOUSLY, CHANGE THIS HARDCODED VALUE SO YOU DON'T GET CAUGHT WITH YOUR PANTS DOWN (love, joshua)
                byte[] secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

                var generator = new TokenModel();

                var claims = new Dictionary<string, object>()
                {
                    //Todo: Insert any claims you want here
                    {"Name",  user.FirstName + " " + user.LastName}
                };
                string token = generator.Generate(secretKey, claims);
                return Ok(ApiOutputFactory.Generate(token));
            }
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public class Login
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}