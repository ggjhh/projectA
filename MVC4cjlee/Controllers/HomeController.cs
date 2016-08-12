using System.Web.Mvc;

namespace MVC4cjlee.Controllers
{
    /// <summary>
    /// 기본 Home
    /// </summary>
    /// <remarks>
    ///     항상 ControllerBase 클래스를 상속 받는다. 
    ///     전역으로 처리되어야 하는 기본 이벤트가 정의되어있다.
    /// </remarks>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
