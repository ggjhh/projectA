using System;
using System.Web.Mvc;

namespace MVC4cjlee.Web.Controllers
{
    public class ControllerBase : Controller
    {
        /// <summary>
        /// 페이지 요청이 시작된 경우 인증 상태를 우선 검사한다.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}