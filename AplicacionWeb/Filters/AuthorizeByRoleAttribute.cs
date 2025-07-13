using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWeb.Filters
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] _rolesPermitidos;

        public AuthorizeByRoleAttribute(params string[] roles)
        {
            _rolesPermitidos = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var roles = httpContext.Session["Roles"] as List<string>;
            if (roles == null)
                return false;

            return _rolesPermitidos.Any(r => roles.Contains(r));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Login/Index");
        }
    }
}
