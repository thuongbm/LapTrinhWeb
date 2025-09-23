using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("Admin/Students")] // tiền tố URL cho tất cả action
public class StudentsController : Controller
{
    private static List<Student> listStudents = new List<Student>()
    {
        new Student() { Id = 101, Name = "Hải Nam", Branchs = Branchs.IT,
            Gender = Gender.Male, IsRegular=true,
            Address = "A1-2018", Email = "nam@g.com" },
        new Student() { Id = 102, Name = "Minh Tú", Branchs = Branchs.BE,
            Gender = Gender.Female, IsRegular=true,
            Address = "A1-2019", Email = "tu@g.com" },
        new Student() { Id = 103, Name = "Hoàng Phong", Branchs = Branchs.CE,
            Gender = Gender.Male, IsRegular=false,
            Address = "A1-2020", Email = "phong@g.com" },
        new Student() { Id = 104, Name = "Xuân Mai", Branchs = Branchs.EE,
            Gender = Gender.Female, IsRegular = false,
            Address = "A1-2021", Email = "mai@g.com" }
    };
    // GET: /Admin/Students/List
    [HttpGet("List")]
    public IActionResult Index()
    {
        return View(listStudents);
    }


    // GET: /Admin/Students/Create
    [HttpGet("Add")]
    public IActionResult Create()
    {
        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "IT" },
            new SelectListItem { Text = "BE", Value = "BE" },
            new SelectListItem { Text = "CE", Value = "CE" },
            new SelectListItem { Text = "EE", Value = "EE" }
        };

        return View();
    }

    // POST: /Admin/Students/Create
    /*[HttpPost("Create")]
    public IActionResult Create(Student s)
    {
        s.Id = listStudents.Last().Id + 1;
        listStudents.Add(s);
        return RedirectToAction("Index"); // redirect về /Admin/Students/List
    }*/
    
    [HttpPost("Add")]
    public IActionResult Create(Student s, IFormFile Photo)
    {
        if (Photo != null && Photo.Length > 0)
        {
            string fileName = $"{Guid.NewGuid()}_{Photo.FileName}";
            string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            string filePath = Path.Combine(uploads, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Photo.CopyTo(fileStream);
            }
            s.PhotoFileName = fileName;
        }

        s.Id = listStudents.Last().Id + 1;
        listStudents.Add(s);

        return RedirectToAction("Index");
    }
}
