﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolDB;
using DBManager;
using System.IO;
using DrivingSchoolWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;

namespace DrivingSchoolWeb.Models
{
    public class SerieController : Controller
    {
        private readonly ISeriesManager _Series;
        private readonly object _hostingEnvironment;

        public SerieController(SeriesManager series, IHostingEnvironment hostingEnvironment)
        {
            _Series = series;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Serie
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Series.ToListAsync());
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
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\Series"));
                var filePath = Path.Combine(uploads, serie.MyImage.FileName + DateTime.Now.ToString("-MMddyyyyHHmmss"));
                serie.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));

                var s = new Serie
                {
                    Number = serie.Number,
                    Image = serie.MyImage.FileName
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
            return View(serie);
        }

        // POST: Serie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Image")] Serie serie)
        {
            if (id != serie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Series.Update(serie);
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
    }
}
