using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Lab1.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();
        static StudentController()
        {
            // Tạo danh sách sinh viên với 5 dữ liệu mẫu
            listStudents = new List<Student>()
    {
        new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
            Gender = Gender.Male, IsRegular=true,
            Address = "A1-2018", Email = "nam@gmail.com", Score = 7.2 },
        new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
            Gender = Gender.Female, IsRegular=true,
            Address = "A1-2019", Email = "tu@gmail.com", Score = 8.1 },
        new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
            Gender = Gender.Male, IsRegular=false,
            Address = "A1-2020", Email = "phong@gmail.com", Score = 5.7 },
        new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
            Gender = Gender.Female, IsRegular=false,
            Address = "A1-2021", Email = "mai@gmail.com", Score = 3.4 },
        new Student() { Id = 105, Name = "Ngọc Bích", Branch = Branch.IT,
            Gender = Gender.Female, IsRegular=true,
            Address = "A1-2018", Email = "bich@gmail.com", Score = 9.5 }
    };
        }
        public IActionResult Index()
        {
            //Trả về View Index.cshtml cùng Model là danh sách sv listStudents
            return View(listStudents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //lấy danh sách các giá trị Gender để hiển thị radio button trên form
            ViewBag.AllGenders = Enum.GetValues (typeof(Gender)).Cast<Gender>().ToList();
            //lấy danh sách các giá trị Branch để hiển thị select-option trên form
            //Để hiển thị select-option trên View cần dùng List<SelectListItem>
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(Student s)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       s.Id = listStudents.Last<Student>().Id + 1;
        //       listStudents.Add(s);
        //       return View("Index", listStudents);
        //    }

        //    //lấy danh sách các giá trị Gender để hiển thị radio button trên form
        //    ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        //    //lấy danh sách các giá trị Branch để hiển thị select-option trên form
        //    //Để hiển thị select-option trên View cần dùng List<SelectListItem>
        //    ViewBag.AllBranches = new List<SelectListItem>()
        //    {
        //        new SelectListItem { Text = "IT", Value = "1" },
        //        new SelectListItem { Text = "BE", Value = "2" },
        //        new SelectListItem { Text = "CE", Value = "3" },
        //        new SelectListItem { Text = "EE", Value = "4" }
        //    };
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(Student s, IFormFile photo)
        //{
        //    if (photo != null && photo.Length > 0)
        //    {
        //        // Tạo đường dẫn tới thư mục wwwroot/uploads
        //        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        //        // Nếu chưa có thư mục thì tạo
        //        if (!Directory.Exists(uploads))
        //        {
        //            Directory.CreateDirectory(uploads);
        //        }

        //        // Đặt tên file
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
        //        var filePath = Path.Combine(uploads, fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            photo.CopyTo(stream);
        //        }

        //        // Lưu đường dẫn tương đối để hiển thị trong View
        //        s.Photo = "/uploads/" + fileName;
        //    }

        //    s.Id = listStudents.Last<Student>().Id + 1;
        //    listStudents.Add(s);
        //    return View("Index", listStudents);

        //}

        [HttpPost]
        public IActionResult Create(Student s, IFormFile? photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }

                    s.Photo = "/uploads/" + fileName;
                }

                s.Id = listStudents.Last().Id + 1;
                listStudents.Add(s);

                return RedirectToAction("Index");
            }

            // load lại dữ liệu ViewBag nếu model không hợp lệ
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
        };

            return View(s);
        }

    }
}