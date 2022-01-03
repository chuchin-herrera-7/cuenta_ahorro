using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace cuenta_ahorro.Models
{

    #region AccessView
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessView : ActionFilterAttribute
    {
        public int Type { get; set; }

        public bool IsPartialView { get; set; }

        public AccessView()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.IsAvailable && filterContext.HttpContext.Session.GetInt32("cuenta-ahorro-user-key") != null)
            {
                bool autorize = false;
                if (filterContext.HttpContext.Session.GetInt32("cuenta-ahorro-type") == Type)
                {
                    autorize = true;
                }
                if (!autorize)
                {
                    if(IsPartialView){
                        filterContext.Result = new PartialViewResult
                        {
                            ViewName = "../Error/NoAccess",
                        };
                    }else{
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "../Error/NoAccess",
                        };
                    }
                        
                    return;
                }                
            }
            else
            {
                var isHtps = filterContext.HttpContext.Request.IsHttps;
                var Host = filterContext.HttpContext.Request.Host;
                var Path = filterContext.HttpContext.Request.Path;
                string url = string.Format("{0}//{1}{2}",(isHtps ? "https:" : "http:"), Host, Path);
                filterContext.HttpContext.Session.SetString("url_next",url);
                filterContext.Result = new RedirectResult("~/");
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
    #endregion
}
