using DrivingSchoolDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DrivingSchoolWeb.ViewModel;
using DBManager;
using System.IO;
using System;

namespace DrivingSchoolWeb.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly DrivingSchoolDbContext _context;
        private readonly QuestionsManager _Questions;

        public QuestionsController(QuestionsManager questions)
        {
            _Questions = questions;
        }

        // GET: Questions
        public IActionResult Index(int id, string Serienum)
        {
            ViewBag.SerieId = id;
            ViewBag.Serienum = Serienum;
            return View(_Questions.GetAllAsync().Result.Select(Mapper.Map<Question, QuestionViewModel>));
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create(int id, string Serienum)
        {
            ViewBag.SerieId = id;
            ViewBag.Serienum = Serienum;
            // new Questionviewmodel
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MyAudio,MyImage,Answeres,CorrectAnswer,SerieId")] QuestionCreateViewModel question)
        {
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\Questions"));

                var Imagefile = DateTime.Now.ToString("MMddyyyyHHmmss-") + question.MyImage.FileName;
                var filePath = Path.Combine(uploads, Imagefile);
                await question.MyImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

                var Audiofile = DateTime.Now.ToString("MMddyyyyHHmmss-") + question.MyAudio.FileName;
                filePath = Path.Combine(uploads, Audiofile);
                await question.MyAudio.CopyToAsync(new FileStream(filePath, FileMode.Create));

                Question Q = new Question
                {
                    Name = question.Name,
                    SerieId = question.SerieId,
                    Image = @"images\Questions\Image\" + Imagefile,
                    Audio = @"images\Questions\Audio\" + Audiofile,
                    Answeres = question.Answeres,
                    CorrectAnswer = question.CorrectAnswer
                };
                await _Questions.AddNewAsync(Q);
                await _Questions.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id, int SerieNum)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _Questions.GetQuestionAsync((int)id,SerieNum);

            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Audio,Image,Answeres,CorrectAnswer,SerieId")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
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
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
