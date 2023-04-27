using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEMPORARY.Models;

namespace TEMPORARY.Controllers
{
    public class LessonController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ExamDBContext _examDBContext;

        public LessonController(IHttpContextAccessor contextAccessor, ExamDBContext examDBContext)
        {
            _contextAccessor = contextAccessor;
            _examDBContext = examDBContext;
        }

        // GET: LessonController
        public async Task<ActionResult> Index()
        {
            var list = await _examDBContext.Lessons.AsNoTracking().ToListAsync();
            return View(list);
        }

        // GET: LessonController/Details
        public async Task<ActionResult> Details(string code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var lesson = await _examDBContext.Lessons.FirstOrDefaultAsync(l => l.LessonCode == code);

            return View(lesson);
        }

        // GET: LessonController/AddLesson
        public ActionResult AddLesson()
        {
            return View();
        }

        // POST: LessonController/AddLesson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddLesson(Lesson lesson)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _examDBContext.Lessons.AddAsync(lesson);
                    await _examDBContext.SaveChangesAsync();
                }
                else
                {
                    return View(lesson);
                }

                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LessonController/EditLesson
        public async Task<IActionResult> EditLesson(string code)
        {
            var selectedLesson = await _examDBContext.Lessons.FirstOrDefaultAsync(l => l.LessonCode == code);
            if (selectedLesson == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(selectedLesson);
        }

        // POST: LessonController/EditLesson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLesson(Lesson lesson)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Lesson selected = new Lesson()
                    {
                        LessonCode = lesson.LessonCode,
                        LessonName = lesson.LessonName,
                        Class = lesson.Class,
                        TeacherName = lesson.TeacherName,
                        TeacherSurname = lesson.TeacherSurname
                    };

                    _examDBContext.Lessons.Update(selected);
                    await _examDBContext.SaveChangesAsync();
                }

                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LessonController/DeleteLesson
        public async Task<ActionResult> DeleteLesson(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }

            var lesson = await _examDBContext.Lessons.FirstOrDefaultAsync(l => l.LessonCode == code);
            if (lesson == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(lesson);
            }
        }

        // POST: LessonController/DeleteLesson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteLesson(Lesson lesson)
        {
            try
            {
                var model = await _examDBContext.Lessons.FirstOrDefaultAsync(l=>l.LessonCode == lesson.LessonCode);
                _examDBContext.Lessons.Remove(model);
                await _examDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
