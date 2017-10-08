using JwtWebAPITemplate.AuthorizationModels;
using JwtWebAPITemplate.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace JwtWebAPITemplate.Controllers
{
    public class BaseApiController : ApiController
    {
            private OutputFactory _outputFactory;
            private ApplicationUserManager _AppUserManager = null;

            protected ApplicationUserManager AppUserManager
            {
                get
                {
                    return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
            }

            public BaseApiController()
            {
            }

            protected OutputFactory ApiOutputFactory
            {
                get
                {
                    if (_outputFactory == null)
                    {
                        _outputFactory = new OutputFactory(this.Request, this.AppUserManager);
                    }
                    return _outputFactory;
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