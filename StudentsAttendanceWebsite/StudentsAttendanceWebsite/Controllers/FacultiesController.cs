using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentsAttendanceWebsite.Models;

namespace StudentsAttendanceWebsite.Controllers
{
    public class FacultiesController : Controller
    {
        public ActionResult Index()
        {
            var db = new StudentAbsenceEntities();
            var result = db.Faculty.Select(s => s).OrderBy(s => s.Name).ToList();
            return View(new CreateAndListFacultyModel() { NewFaculty = new Faculty(), Faculties = result });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new StudentAbsenceEntities();
            var faculty = db.Faculty.FirstOrDefault(s => s.Id == id);
            db.Faculty.Remove(faculty);
            db.SaveChanges();
            var result = db.Faculty.Select(s => s).OrderBy(s => s.Name).ToList();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(CreateAndListFacultyModel model)
        {
            var db = new StudentAbsenceEntities();
            if (ModelState.IsValid)
            {
                db.Faculty.Add(model.NewFaculty);
                db.SaveChanges();
                var result = db.Faculty.Select(s => s).OrderBy(s => s.Name).ToList();
                return View("Index", new CreateAndListFacultyModel() { NewFaculty = new Faculty(), Faculties = result });
            }

            var list = db.Faculty.Select(s => s).OrderBy(s => s.Name).ToList();
            return View("Index", new CreateAndListFacultyModel() { NewFaculty = model.NewFaculty, Faculties = list });
        }
    }
}