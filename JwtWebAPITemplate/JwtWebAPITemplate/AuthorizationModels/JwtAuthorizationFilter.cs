
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace JwtWebAPITemplate.AuthorizationModels
{
    public class JwtAuthorizationFilter : AuthorizationFilterAttribute
    {
        bool Active = true;
        public JwtAuthorizationFilter()
        { }
        /// <summary>
        /// Overriden constructor to allow explicit disabling of this
        /// filter's behavior. Pass false to disable (same as no filter
        /// but declarative)
        /// </summary>
        /// <param name="active"></param>
        public JwtAuthorizationFilter(bool active)
        {
            Active = active;
        }

        /// <summary>
        /// Override to Web API filter method to handle Basic Auth check
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Active)
            {
                var success = actionContext.Request.Headers.TryGetValues("Authorization", out IEnumerable<string> authHeader);

                if (!success || authHeader == null || authHeader.ToString().StartsWith("Bearer"))
                {
                    Challenge(actionContext);
                    return;
                }

                //Extract credentials
                string fullHeader = authHeader.First().ToString();
                string token = fullHeader.Substring("Bearer ".Length).Trim();

                ApplicationUser user = OnAuthorizeUser(token);
                if (user == null)
                {
                    Challenge(actionContext);
                    return;
                }

                var principal = new GenericPrincipal(new GenericIdentity(user.Id), null);

                Thread.CurrentPrincipal = principal;

                // inside of ASP.NET this is required
                if (HttpContext.Current != null)
                    HttpContext.Current.User = principal;

                base.OnAuthorization(actionContext);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        protected virtual ApplicationUser OnAuthorizeUser(string jwt)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var validator = new TokenModel();
            byte[] secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

            //Todo: WARNING: Add a proper secret handling mechansm here. 
            ApplicationUser user = validator.Verify(userManager,secretKey, jwt);
            if (user == null)
                return null;

            return user;
        }
        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionContext"></param>
        void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("Authorization", string.Format("Bearer realm=\"{0}\"", host));
        }
    }
}