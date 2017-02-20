using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Mvc;
using System.Net.Mime;
using System.Text;
using log4net.Repository.Hierarchy;
using log4net;

namespace Padel.Web.Mvc.Filters
{
    public class ExceptionAjaxLoggerFilter : HandleErrorAttribute
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ExceptionAjaxLoggerFilter));

        public ExceptionAjaxLoggerFilter()
        {

        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                if (filterContext.Controller.ViewBag.ErrorResult != null)
                {
                    filterContext.HttpContext.Response.StatusCode = 200;
                    filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, "Ha ocurrido un error en el sistema y estamos trabajando para solucionarlo. Gracias!.");
                    filterContext.Result = filterContext.Controller.ViewBag.ErrorResult;
                    logger.Error(filterContext.Exception.Message, filterContext.Exception);
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 400;
                    filterContext.Result = new ContentResult { ContentEncoding = Encoding.UTF8, ContentType = "text/plain", Content = "Ha ocurrido un error en el sistema y estamos trabajando para solucionarlo. Gracias!." };
                    logger.Error(filterContext.Exception.Message, filterContext.Exception);
                }
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }

}