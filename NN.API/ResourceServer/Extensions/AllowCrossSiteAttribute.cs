using System.Web.Http.Filters;


namespace NNResourceServer.API.Extensions
{
    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "*");
        //    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");

        //    base.OnActionExecuting(filterContext);
        //}

    }
}