using DBManager;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchoolWeb.Controllers
{
    public class SeriesController : Controller
    {
        ISeriesManager _Series;
        public SeriesController(SeriesManager series)
        {
            _Series = series;
        }
        public IActionResult Index()
        {
            var list = _Series.GetAll();
            return View(list);
        }
    }
}