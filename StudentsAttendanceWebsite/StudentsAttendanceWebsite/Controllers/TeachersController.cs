using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StudentsAttendanceWebsite.Models;

namespace StudentsAttendanceWebsite.Controllers
{
    public class TeachersController : Controller
    {
        public ActionResult Index(string search, int? page)
        {
            var db = new StudentAbsenceEntities();
            var teachers = db.Teacher.Select(t => t);
            if (!string.IsNullOrEmpty(search))
            {
                teachers = teachers.Where(p => p.Person.Name.Contains(search) || p.Person.Surname.Contains(search) || p.Person.Patronimic.Contains(search));
            }
            var result = teachers.OrderBy(p => p.Person.Surname).ToList().ToPagedList(page ?? 1, 3);
            return View(result);
        }
    }
}