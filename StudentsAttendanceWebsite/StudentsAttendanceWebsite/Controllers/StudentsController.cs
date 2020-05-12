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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new StudentAbsenceEntities();
            var student = db.Student.First(c => c.PersonId == id);
            ViewBag.Groups = new SelectList(db.Group, "Id", "Name", student.GroupId);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student model)
        {
            var db = new StudentAbsenceEntities();

            if (ModelState.IsValid)
            {
                var student = db.Student.First(c => c.PersonId == model.PersonId);
                if (student != null)
                {
                    student.GroupId = model.GroupId;
                    student.RecordBookNumber = model.RecordBookNumber;
                    student.BirthDate = model.BirthDate;
                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.PersonId});
            }
            model.Person = db.Person.First(p => p.Id == model.PersonId);
            ViewBag.Groups = new SelectList(db.Group, "Id", "Name", model.GroupId);
            return View(model);
        }
    }
}