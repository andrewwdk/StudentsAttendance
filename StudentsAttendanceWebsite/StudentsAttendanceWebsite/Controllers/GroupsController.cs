using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentsAttendanceWebsite.Models;

namespace StudentsAttendanceWebsite.Controllers
{
    public class GroupsController : Controller
    {
        public ActionResult Index()
        {
            var db = new StudentAbsenceEntities();
            ViewBag.Faculties = new SelectList(db.Faculty, "Id", "Name");
            var list = db.Group.Select(c => c).OrderBy(c => c.Name).ToList();
            return View(new CreateAndListGroupModel() { Groups = list, NewGroup = new Group() });
        }

        [HttpPost]
        public ActionResult Create(CreateAndListGroupModel model)
        {
            var db = new StudentAbsenceEntities();

            if (!db.IsGroupUnique(model.NewGroup.Name))
            {
                ModelState.AddModelError(string.Empty, "Группа с таким номером уже существует!");
            }

            if (ModelState.IsValid)
            {
                db.Group.Add(model.NewGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var list = db.Group.Select(c => c).OrderBy(c => c.Name).ToList();
            ViewBag.Faculties = new SelectList(db.Faculty, "Id", "Name");
            return View("Index", new CreateAndListGroupModel() { Groups = list, NewGroup = model.NewGroup });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new StudentAbsenceEntities();
            var groupToRemove = db.Group.FirstOrDefault(s => s.Id == id);
            db.Group.Remove(groupToRemove);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}