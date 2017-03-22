using System.Web.Mvc;
using WebAppWithMySQL.Data;
using WebAppWithMySQL.Models;

namespace WebAppWithMySQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataService _dataService;
        public HomeController()
        {
            _dataService = new DataService();
        }
        public ActionResult Index()
        {
            string error;
            var cities = _dataService.GetCityList(out error);
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
            }
            return View(cities);
        }

        public ActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCity(string city, int country_id)
        {
            string error;
            var added =_dataService.AddNewCity(new City() { city=city, country_id = country_id}, out error);
            if (!string.IsNullOrEmpty(error))
            {
                if (!added)
                {
                    ViewBag.Error = "Can not add new city.";
                }
                ViewBag.Error = error;
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
