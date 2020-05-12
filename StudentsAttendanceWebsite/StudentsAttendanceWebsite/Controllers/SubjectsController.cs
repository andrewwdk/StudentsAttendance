using StudentsAttendanceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsAttendanceWebsite.Controllers
{
    public class SubjectsController : Controller
    {

        public ActionResult Index()
        {
            var db = new StudentAbsenceEntities();
            var result = db.Subject.Select(s => s).OrderBy(s => s.Name).ToList();
            return View(new CreateAndListSubjectModel() { NewSubject = new Subject(), Subjects = result });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new StudentAbsenceEntities();
            var subject = db.Subject.FirstOrDefault(s => s.Id == id);
            db.Subject.Remove(subject);
            db.SaveChanges();
            var result = db.Subject.Select(s => s).OrderBy(s => s.Name).ToList();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(CreateAndListSubjectModel model)
        {
            var db = new StudentAbsenceEntities();
            if (ModelState.IsValid)
            {
                db.Subject.Add(model.NewSubject);
                db.SaveChanges();
                var result = db.Subject.Select(s => s).OrderBy(s => s.Name).ToList();
                return RedirectToAction("Index");
            }

            var list = db.Subject.Select(s => s).OrderBy(s => s.Name).ToList();
            return View("Index", new CreateAndListSubjectModel() { NewSubject = model.NewSubject, Subjects = list });
        }
    }
}