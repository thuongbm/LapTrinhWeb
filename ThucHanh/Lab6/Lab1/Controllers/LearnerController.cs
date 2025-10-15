using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab1.Models;

namespace Lab1.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;
        //khai báo biến toàn cục pageSize
        private int pageSize = 3;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }
        public IActionResult Index(int? mid)
        {
            var learners = (IQueryable<Learner>)db.Learners
                .Include(m => m.Major);

            if (mid != null)
            {
                learners = (IQueryable<Learner>)db.Learners
                    .Where(l => l.MajorID == mid)
                    .Include(m => m.Major);
            }

            //tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);

            //trả số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;

            //lấy dữ liệu trang đầu
            var result = learners.Take(pageSize).ToList();

            return View(result);
        }
        public IActionResult Create()
        {
            var majors = new List<SelectListItem>();
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorID.ToString()
                });
            }

            ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName, LastName, MajorID, EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);

            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID, FirstMidName, LastName, MajorID, EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
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
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }
            var learner = db.Learners.Include(l => l.Major).Include(e => e.Enrollments).FirstOrDefault(m => m.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }
            if (learner.Enrollments.Count() > 0)
            {
                return Content("Cannot delete learner with enrollments.");
            }
            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners'  is null.");
            }
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult LearnerByMajor(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major).ToList();
            return PartialView("LearnerByMajor", learners);
        }

        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            //lấy toàn bộ learners trong dbset chuyển về IQuerrable<Learner> để query
            var learners = (IQueryable<Learner>)db.Learners;

            //lấy chỉ số trang, nếu chỉ số trang null thì gán ngầm định bằng 1
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);

            //nếu có mid thì lọc learner theo mid (chuyên ngành)
            if (mid != null)
            {
                //lọc
                learners = learners.Where(l => l.MajorID == mid);

                //gửi mid về view để ghi lại trên nav-phân trang
                ViewBag.mid = mid;
            }

            //nếu có keyword thì tìm kiếm theo tên
            if (keyword != null)
            {
                //tìm kiếm
                learners = learners.Where(l => l.FirstMidName.ToLower()
                    .Contains(keyword.ToLower()));

                //gửi keyword về view để ghi trên nav-phân trang
                ViewBag.keyword = keyword;
            }

            //tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);

            //gửi số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;

            //chọn dữ liệu trong trang hiện tại
            var result = learners.Skip(pageSize * (page - 1))
                .Take(pageSize).Include(m => m.Major);

            return PartialView("LearnerTable", result);
        }

    }
}
