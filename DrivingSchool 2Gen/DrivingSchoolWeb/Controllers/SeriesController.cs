using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DBManager;

namespace DrivingSchoolWeb.Controllers
{
    public class SeriesController : Controller
    {
        SeriesManager _Series;
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