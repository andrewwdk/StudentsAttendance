using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentsAttendanceWebsite.Models;

namespace StudentsAttendanceWebsite.Controllers
{
    public class MyProfileController : Controller
    {
        public ActionResult Index()
        {
            var db = new StudentAbsenceEntities();
            var user = db.User.First(u => u.login == User.Identity.Name);
            var person = user.Person;
            return View(person);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var db = new StudentAbsenceEntities();
            var user = db.User.First(u => u.login == User.Identity.Name);
            var person = user.Person;
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            var db = new StudentAbsenceEntities();

            ModelState.Remove("Role");
            ModelState.Remove("Password");
            ModelState.Remove("Login");
            if (ModelState.IsValid)
            {
                var dbPerson = db.Person.First(p => p.Id == person.Id);
                if (dbPerson != null)
                {
                    dbPerson.Address = person.Address;
                    dbPerson.Email = person.Email;
                    dbPerson.PhoneNumber = person.PhoneNumber;
                    dbPerson.Name = person.Name;
                    dbPerson.Patronimic = person.Patronimic;
                    dbPerson.Surname = person.Surname;
                    dbPerson.Role = "d";
                    dbPerson.Password = "3285358503";
                    dbPerson.Login = "admin123";
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}