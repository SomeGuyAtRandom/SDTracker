using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO; // For now.. should be a data base

namespace SDTracker.Common
{
    public class TrackExecutionTime : ActionFilterAttribute, IExceptionFilter
    {
        /*
         * To use just anotate the controler with [TrackExecutionTime]
         *
         */


        // note the post-fix 'ing' means before or about to
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string msg= "";
            msg += "@ Time:" + DateTime.Now.ToString("hh:mm:ss.ffff");
            msg += filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            msg += "->" + filterContext.ActionDescriptor.ActionName;
            msg += "->" + "OnActionExecuting (or before)";
            msg += "\n";
            LogExecutionTime(msg);
            
        }

        // note the post-fix 'ed' means finished
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string msg = "";
            msg += "@ Time:" + DateTime.Now.ToString("hh:mm:ss.ffff");
            msg += filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            msg += "->" + filterContext.ActionDescriptor.ActionName;
            msg += "->" + "OnActionExecuted (or after)";
            msg += "\n";
            LogExecutionTime(msg);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string msg = "";
            msg += "@ Time:" + DateTime.Now.ToString("hh:mm:ss.ffff");
            msg += "->" + filterContext.RouteData.Values["controller"];
            msg += "->" + filterContext.RouteData.Values["action"];
            msg += "->" + "OnResultExecuting (or before)";
            msg += "\n";

            LogExecutionTime(msg);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string msg = "";
            msg += "@ Time:" + DateTime.Now.ToString("hh:mm:ss.ffff");
            msg += "->" + filterContext.RouteData.Values["controller"];
            msg += "->" + filterContext.RouteData.Values["action"];
            msg += "->" + "OnResultExecuted (or after)";
            msg += "\n";

            LogExecutionTime(msg);
        }

        public void OnException(ExceptionContext filterContext)
        {
            string msg = "";
            msg += "@ Time:" + DateTime.Now.ToString("hh:mm:ss.ffff");
            msg += "EXCEPTION";
            msg += "->" + filterContext.Exception.Message;
            msg += "\n";
            LogExecutionTime(msg);
        }

        private void LogExecutionTime(string data)
        {
            //TODO:
            // There needs to be two things, a on/off switch is something like Global.asax
            // Or .. some reading value of the the web.config
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_data/Log.txt"), data);
        }


    }
}