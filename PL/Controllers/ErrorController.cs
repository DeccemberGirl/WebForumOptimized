using System.Web.Mvc;

namespace WebForum
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}