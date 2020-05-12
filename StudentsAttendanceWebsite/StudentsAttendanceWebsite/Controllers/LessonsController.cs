using StudentsAttendanceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsAttendanceWebsite.Controllers
{
    public class LessonsController : Controller
    {
        public ActionResult Index()
        {
            var db = new StudentAbsenceEntities();
            ViewBag.Subjects = new SelectList(db.Subject, "Id", "Name");
            ViewBag.Teachers = new SelectList(db.Teacher, "PersonId", "FullName");
            ViewBag.Groups = new SelectList(db.Group, "Id", "Name");
            var list = db.Lesson.Select(c => c).OrderBy(c => c.Group.Name).ThenBy(c => c.Date).ToList();
            return View(new CreateAndListLessonModel() { Lessons = list, NewLesson = new Lesson() });
        }

        [HttpPost]
        public ActionResult Create(CreateAndListLessonModel model)
        {
            var db = new StudentAbsenceEntities();

            if (ModelState.IsValid)
            {
                db.Lesson.Add(model.NewLesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Subjects = new SelectList(db.Subject, "Id", "Name");
            ViewBag.Teachers = new SelectList(db.Teacher, "PersonId", "FullName");
            ViewBag.Groups = new SelectList(db.Group, "Id", "Name");
            var list = db.Lesson.Select(c => c).OrderBy(c => c.Group.Name).ThenBy(c => c.Date).ToList();
            return View("Index", new CreateAndListLessonModel() { Lessons = list, NewLesson = model.NewLesson });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new StudentAbsenceEntities();
            var lesson = db.Lesson.FirstOrDefault(s => s.Id == id);
            db.Lesson.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}