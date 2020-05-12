using PagedList;
using StudentsAttendanceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsAttendanceWebsite.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult Index(string search, int? page)
        {
            var db = new StudentAbsenceEntities();
            var students = db.Student.Select(t => t);
            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(p => p.Person.Name.Contains(search) || p.Person.Surname.Contains(search) || p.Person.Patronimic.Contains(search));
            }
            var result = students.OrderBy(p => p.Person.Surname).ToList().ToPagedList(page ?? 1, 3);
            return View(result);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var db = new StudentAbsenceEntities();
            var student = db.Student.First(p => p.PersonId == id);
            return View(student);
        }
    }
}