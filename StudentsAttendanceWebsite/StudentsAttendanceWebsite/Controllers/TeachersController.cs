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

        public ActionResult Groups()
        {
            var db = new StudentAbsenceEntities();
            var user = db.User.First(u => u.login == User.Identity.Name);
            var person = user.Person;
            var lessons = db.Lesson.Where(l => l.TeacherId == person.Id).ToList();
            return View(GetDistictGroups(lessons));
        }

        public ActionResult GroupLessons(int id)
        {
            var db = new StudentAbsenceEntities();
            var user = db.User.First(u => u.login == User.Identity.Name);
            var person = user.Person;
            var lessons = db.Lesson.Where(l => l.TeacherId == person.Id && l.GroupId == id).ToList();
            return View(lessons);
        }

        private List<Group> GetDistictGroups(List<Lesson> lessons)
        {
            var list = new List<Group>();
            if(lessons != null)
            {
                foreach(var lesson in lessons)
                {
                    if (!list.Contains(lesson.Group))
                    {
                        list.Add(lesson.Group);
                    }
                }
            }
            return list;
        }
    }
}