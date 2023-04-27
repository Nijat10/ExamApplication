using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEMPORARY.Models;

namespace TEMPORARY.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ExamDBContext _examDBContext;

        public StudentController(IHttpContextAccessor contextAccessor, ExamDBContext examDBContext)
        {
            _contextAccessor = contextAccessor;
            _examDBContext = examDBContext;
        }

        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            var list = await _examDBContext.Students.AsNoTracking().ToListAsync();
            return View(list);
        }

        // GET: StudentController/Details
        public async Task<ActionResult> Details(int no)
        {
            var student = await _examDBContext.Students.FirstOrDefaultAsync(l => l.StudentNo == no);

            return View(student);
        }

        // GET: StudentController/AddStudent
        public ActionResult AddStudent()
        {
            return View();
        }

        // POST: StudentController/AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _examDBContext.Students.AddAsync(student);
                    await _examDBContext.SaveChangesAsync();
                }
                else
                {
                    return View(student);
                }

                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LessonController/EditStudent
        public async Task<IActionResult> EditStudent(int no)
        {
            var selectedStudent = await _examDBContext.Students.FirstOrDefaultAsync(l => l.StudentNo == no);
            if (selectedStudent == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(selectedStudent);
        }

        // POST: StudentController/EditStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student selected = new Student()
                    {
                        StudentNo = student.StudentNo,
                        StudentName = student.StudentName,
                        StudentSurname = student.StudentSurname,
                        Class = student.Class
                    };

                    _examDBContext.Students.Update(selected);
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

        // GET: StudentController/DeleteStudent
        public async Task<ActionResult> DeleteStudent(int no)
        {
            var student = await _examDBContext.Students.FirstOrDefaultAsync(l => l.StudentNo == no);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(student);
            }
        }

        // POST: StudentController/DeleteStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStudent(Student student)
        {
            try
            {
                var model = await _examDBContext.Students.FirstOrDefaultAsync(l => l.StudentNo == student.StudentNo);
                if (model != null)
                {
                    _examDBContext.Students.Remove(model);
                    await _examDBContext.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
