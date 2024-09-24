using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebApplicationXml.Models;

public class StudentsController : Controller
{
    public IActionResult Index()
    {
        var students = LoadStudentsFromXml();
        return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        if (ModelState.IsValid)
        {
            var students = LoadStudentsFromXml();
            students.Add(student);
            SaveStudentsToXml(students); // Lưu vào XML
            return RedirectToAction("Index");
        }
        return View(student);
    }

    [HttpGet]
    public IActionResult Edit(string masv)
    {
        var students = LoadStudentsFromXml();
        var student = students.FirstOrDefault(s => s.Masv == masv);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            var students = LoadStudentsFromXml();
            var existingStudent = students.FirstOrDefault(s => s.Masv == student.Masv);
            if (existingStudent != null)
            {
                existingStudent.Hoten = student.Hoten;
                existingStudent.Lop = student.Lop;
                existingStudent.Namsinh = student.Namsinh;
                existingStudent.Gioitinh = student.Gioitinh;
                existingStudent.Sdt = student.Sdt;
                existingStudent.Diachi = student.Diachi;
                SaveStudentsToXml(students);
            }
            return RedirectToAction("Index");
        }
        return View(student);
    }

    [HttpPost]
    public IActionResult Delete(string masv)
    {
        var students = LoadStudentsFromXml();
        var studentToRemove = students.FirstOrDefault(s => s.Masv == masv);
        if (studentToRemove != null)
        {
            students.Remove(studentToRemove);
            SaveStudentsToXml(students);
        }
        return RedirectToAction("Index");
    }

    private List<Student> LoadStudentsFromXml()
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

    private void SaveStudentsToXml(List<Student> students)
    {
        XDocument doc = new XDocument(new XElement("qlsv", students.Select(s => new XElement("sv",
            new XElement("masv", s.Masv),
            new XElement("hoten", s.Hoten),
            new XElement("lop", s.Lop),
            new XElement("namsinh", s.Namsinh),
            new XElement("gioitinh", s.Gioitinh),
            new XElement("sdt", s.Sdt),
            new XElement("diachi", s.Diachi)
        ))));

        doc.Save("App_data/students.xml");
    }
}
