using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEMPORARY.Models;

namespace TEMPORARY.Controllers
{
    public class ExamController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ExamDBContext _examDBContext;

        public ExamController(IHttpContextAccessor contextAccessor, ExamDBContext examDBContext)
        {
            _contextAccessor = contextAccessor;
            _examDBContext = examDBContext;
        }

        // GET: ExamController
        public async Task<ActionResult> Index()
        {
            var list = await _examDBContext.Exams.AsNoTracking().ToListAsync();
            return View(list);
        }

        // GET: ExamController/Details
        public async Task<ActionResult> Details(string code, int no)
        {
            if (code == null)
            {
                return NotFound();
            }

            var exam = await _examDBContext.Exams.FirstOrDefaultAsync(l => l.LessonCode == code && l.StudentNo == no);

            return View(exam);
        }

        // GET: ExamController/AddExam
        public ActionResult AddExam()
        {
            ViewBag.Lessons = _examDBContext.Lessons.ToList<Lesson>().Select(m => new SelectListItem { Text = m.LessonName, Value = m.LessonCode }).ToList<SelectListItem>();

            ViewBag.Students = _examDBContext.Students.ToList<Student>().Select(m => new SelectListItem { Text = m.StudentName, Value = m.StudentNo.ToString() }).ToList<SelectListItem>();


            return View();
        }

        // POST: ExamController/AddExam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddExam(Exam exam)
        {
            try
            {
                await _examDBContext.Exams.AddAsync(exam);
                await _examDBContext.SaveChangesAsync();
                
                ModelState.Clear();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(exam);
            }
        }

        // GET: ExamController/EditExam
        public async Task<IActionResult> EditExam(string code, int no)
        {
            var selectedLesson = await _examDBContext.Exams.FirstOrDefaultAsync(e => e.LessonCode == code && e.StudentNo == no);
            if (selectedLesson == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Lessons = _examDBContext.Lessons.ToList<Lesson>().Select(m => new SelectListItem { Text = m.LessonName, Value = m.LessonCode }).ToList<SelectListItem>();

            ViewBag.Students = _examDBContext.Students.ToList<Student>().Select(m => new SelectListItem { Text = m.StudentName, Value = m.StudentNo.ToString() }).ToList<SelectListItem>();

            return View(selectedLesson);
        }

        // POST: ExamController/EditExam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditExam(Exam exam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Exam selected = new Exam()
                    {
                        LessonCode = exam.LessonCode,
                        StudentNo = exam.StudentNo,
                        ExamDate = exam.ExamDate,
                        ExamGrade = exam.ExamGrade
                    };

                    _examDBContext.Exams.Update(selected);
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

        // GET: ExamController/DeleteExam
        public async Task<ActionResult> DeleteExam(string code, int no)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }

            var exam = await _examDBContext.Exams.FirstOrDefaultAsync(l => l.LessonCode == code && l.StudentNo == no);
            if (exam == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Lessons = _examDBContext.Lessons.ToList<Lesson>().Select(m => new SelectListItem { Text = m.LessonName, Value = m.LessonCode }).ToList<SelectListItem>();

                ViewBag.Students = _examDBContext.Students.ToList<Student>().Select(m => new SelectListItem { Text = m.StudentName, Value = m.StudentNo.ToString() }).ToList<SelectListItem>();

                return View(exam);
            }
        }

        // POST: ExamController/DeleteExam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteExam(Exam exam)
        {
            try
            {
                var model = await _examDBContext.Exams.FirstOrDefaultAsync(l => l.LessonCode == exam.LessonCode && l.StudentNo == exam.StudentNo);
                _examDBContext.Exams.Remove(model);
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
