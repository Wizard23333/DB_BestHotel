using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.Models
{
    public class ApiResponseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //模型验证
            if (!context.ModelState.IsValid)
            {
                throw new ApplicationException(context.ModelState.Values.First(p => p.Errors.Count > 0).Errors[0].ErrorMessage);
            }
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 处理正常返回的结果对象，进行统一json格式包装
        /// 异常只能交由ExceptionFilterAttribute 去处理 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null)
            {
                if (context.Result is ObjectResult)
                {
                    var objectResult = context.Result as ObjectResult;
                    if (objectResult.Value == null)
                    {
                        context.Result = new ObjectResult(new { code = 404, data = "" });
                    }
                    else
                    {
                        context.Result = new ObjectResult(new { code = 200, data = objectResult.Value });
                    }
                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new ObjectResult(new { code = 404, data = "" });
                }
                else if (context.Result is ContentResult)
                {
                    context.Result = new ObjectResult(new { code = 200, result = (context.Result as ContentResult).Content });
                }
                else if (context.Result is StatusCodeResult)
                {
                    context.Result = new ObjectResult(new { code = (context.Result as StatusCodeResult).StatusCode, data = "" });
                }

            }
            base.OnActionExecuted(context);
        }
    }

    /// <summary>
    /// api异常统一处理过滤器
    /// 系统级别异常 500 应用级别异常501
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = BuildExceptionResult(context.Exception);
            base.OnException(context);
        }

        /// <summary>
        /// 包装处理异常格式
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private JsonResult BuildExceptionResult(Exception ex)
        {
            int code = 0;
            string message = "";
            string innerMessage = "";
            //应用程序业务级异常
            if (ex is ApplicationException)
            {
                code = 501;
                message = ex.Message;
            }
            else
            {
                // exception 系统级别异常，不直接明文显示的
                code = 500;
                message = "发生系统级别异常";
                innerMessage = ex.Message;
            }

            if (ex.InnerException != null && ex.Message != ex.InnerException.Message)
                innerMessage += "," + ex.InnerException.Message;

            return new JsonResult(new { code, message, innerMessage });
        }
    }
}
