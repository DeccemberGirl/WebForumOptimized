using System.Web.Mvc;

namespace WebForum
{
    /// <summary>
    /// Controller which corresponds to the page of some error happened
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Indicates that no information was found
        /// </summary>
        /// <returns>Not found error view</returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Indicates that the access to the page is forbidden
        /// </summary>
        /// <returns>Forbidden error view</returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        /// <summary>
        /// Indicates that internal server error happened
        /// </summary>
        /// <returns>InternalServerError error view</returns>
        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}