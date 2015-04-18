using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;


namespace SDTracker.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizerAttribute : AuthorizeAttribute
    {
        // NOTE: This is not thread safe, it is much better to store this
        // value in HttpContext.Items.  See Ben Cull's answer below for an example.
        private bool _isAuthorized;

        private Boolean UserIsAuthorized(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext != null)
            {
                SDTRoleProvider check = new SDTRoleProvider();
                string userName = filterContext.RequestContext.HttpContext.User.Identity.Name;
                return check.IsUserInRole(userName, Roles);
            }
            throw new ArgumentException("filterContext");
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            _isAuthorized = base.AuthorizeCore(httpContext);
            return _isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) { 
                throw new ArgumentException("filterContext");
            }
            else if (filterContext.Result is HttpUnauthorizedResult)
            {
                
                // I can never seem to enter this block..?? For JSON??
                // If so.. How would you set 'client'?
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "client", filterContext.RouteData.Values[ "client" ] },
                    { "controller", "User" },
                    { "action", "Login" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                });
            }
            else if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("~/User/Login?ReturnUrl=" + HttpUtility.HtmlEncode(filterContext.HttpContext.Request.RawUrl));
            }
            else if (!UserIsAuthorized(filterContext))
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("~/Home/NoAccess");
            }
            else 
            {
                // Do nothing user is ok
            }
        }


    }
}