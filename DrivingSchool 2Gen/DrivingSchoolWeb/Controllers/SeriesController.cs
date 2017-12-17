using DBManager;
using DrivingSchoolDB;
using DrivingSchoolWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingSchoolWeb.Models
{
    public class SeriesController : Controller
    {
        private readonly ISeriesManager _Series;
        private readonly QuestionsManager _Questions;
        private readonly object _hostingEnvironment;

        public SeriesController(SeriesManager series, IHostingEnvironment hostingEnvironment, QuestionsManager questions)
        {
            _Questions = questions;
            _Series = series;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Serie
        public async Task<IActionResult> Index()
        {
            _Series.GetAll().Select(s => new { image = s.Image, number = s.Number});
            return View(await _Series.GetAllAsync());
        }

        // GET: Serie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _Series.GetSerieAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // GET: Serie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Serie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Image,MyImage")] SerieViewModel serie)
        {
            if (serie.MyImage == null)
            {
                ModelState.AddModelError("MyImage", "File is required, please upload an image.");
                return View(serie);
            }
            if (serie.MyImage.Length < 0)
            {
                ModelState.AddModelError("MyImage", "File size is 0.");
                return View(serie);
            }
            if (ModelState.IsValid && serie.MyImage.Length > 0)
            {
                var uploads = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\Series"));
                var fileName = DateTime.Now.ToString("MMddyyyyHHmmss-") + serie.MyImage.FileName;
                var filePath = Path.Combine(uploads, fileName);
                await serie.MyImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

                var s = new Serie
                {
                    Number = serie.Number,
                    Image = @"images\Series\" + fileName
                };

                _Series.AddNew(s);
                await _Series.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serie);
        }

        // GET: Serie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _Series.GetSerieAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var s = new SerieViewModel() { Id = serie.Id, Number = serie.Number, Image = serie.Image };
            return View(s);
        }

        // POST: Serie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Image,MyImage")] SerieViewModel serie)
        {
            if (id != serie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "";
                    if (serie.MyImage != null)
                    {
                        var uploads = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\Series"));
                        fileName = DateTime.Now.ToString("MMddyyyyHHmmss-") + serie.MyImage.FileName;
                        var filePath = Path.Combine(uploads, fileName);
                        await serie.MyImage.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        fileName = @"images\Series\" + fileName;
                    }
                    else
                    {
                        fileName = serie.Image;
                    }
                    var s = new Serie()
                    {
                        Id = serie.Id,
                        Number = serie.Number,
                        Image =  fileName
                    };
                    _Series.Update(s);
                    await _Series.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SerieExists(serie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(serie);
        }

        // GET: Serie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _Series.GetSerieAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // POST: Serie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serie = await _Series.GetSerieAsync(id);
            _Series.Delete(serie);
            await _Series.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SerieExists(int id)
        {
            var L = await (_Series.GetAllAsync());
            return L.Any(e => e.Id == id);
        }

        // GET: Serie
        public  IActionResult Questions(int id)
        {
            //return View(await _context.Series.ToListAsync());
            ViewData["SerieId"] = id;
            return View( _Questions.GetQuestionBySerie(id));
        }
    }
}
