using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanBrewList.Authorize
{
    public class BrewAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userName = httpContext.Session["Username"];

            if (userName != null)
            {
                return true;
            }

            return false;
        }
    }
}