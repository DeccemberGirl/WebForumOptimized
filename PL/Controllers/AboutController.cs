using System.Web.Mvc;

namespace PL.Controllers
{
    /// <summary>
    /// Controller which corresponds to the page with information about the forum
    /// </summary>
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            return View();
        }
    }
}