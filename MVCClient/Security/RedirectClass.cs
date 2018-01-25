using MVCClient.Security;
using System;
using System.Web.Mvc;
using System.Web.Routing;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NoDirectAccessAttribute : ActionFilterAttribute
{
    //Prevents the user from redirecting to a page entered in the URL
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Request.UrlReferrer == null ||
                    filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
        {
            SessionLogin.CloseSession();
            filterContext.Result = new RedirectToRouteResult(new
                           RouteValueDictionary(new { controller = "Quiz", action = "Index", area = "" }));

        }
    }
}