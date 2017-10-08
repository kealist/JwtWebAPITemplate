using JwtWebAPITemplate.AuthorizationModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JwtWebAPITemplate.Controllers
{
    public class BaseApiController : ApiController
    {
            private ModelFactory _modelFactory;
            private ApplicationUserManager _AppUserManager = null;

            protected ApplicationUserManager AppUserManager
            {
                get
                {
                    return _AppUserManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
            }

            public BaseApiController()
            {
            }

            protected ModelFactory ApiOutputFactory
            {
                get
                {
                    if (_modelFactory == null)
                    {
                        _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                    }
                    return _modelFactory;
                }
            }

            protected IHttpActionResult GetErrorResult(IdentityResult result)
            {
                if (result == null)
                {
                    return InternalServerError();
                }

                if (!result.Succeeded)
                {
                    if (result.Errors != null)
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        // No ModelState errors are available to send, so just return an empty BadRequest.
                        return BadRequest();
                    }

                    return BadRequest(ModelState);
                }

                return null;
            }
        }
    }