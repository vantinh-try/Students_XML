using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplicationXml.Models;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace WebApplicationXml.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var students = LoadStudentsFromXml();
            return View(students);
        }

        private List<Student> LoadStudentsFromXml()
        {
            try
            {
                XDocument doc = XDocument.Load("App_data/students.xml");
                return doc.Descendants("sv")
                          .Select(s => new Student
                          {
                              Masv = (string?)s.Element("masv") ?? string.Empty,
                              Hoten = (string?)s.Element("hoten") ?? string.Empty,
                              Lop = (string?)s.Element("lop") ?? string.Empty,
                              Namsinh = (string?)s.Element("namsinh") ?? string.Empty,
                              Gioitinh = (string?)s.Element("gioitinh") ?? string.Empty,
                              Sdt = (string?)s.Element("sdt") ?? string.Empty,
                              Diachi = (string?)s.Element("diachi") ?? string.Empty
                          }).ToList();
            }
            catch (Exception)
            {
                // Xử lý lỗi (ví dụ: ghi log)
                return new List<Student>();
            }
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
