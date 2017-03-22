using System.Web.Mvc;
using WebAppWithMySQL.Data;
using WebAppWithMySQL.Models;

namespace WebAppWithMySQL.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var dataService = new DataService();
            string error;
            var cities = dataService.GetCityList(out error);
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
            }
            return View(cities);
        }

    }
}
