using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using StudentsAttendanceWebsite.Models;

namespace StudentsAttendanceWebsite.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index(string search, int? page)
        {
            var db = new StudentAbsenceEntities();
            var users = db.Person.Select(s => s);
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(p => p.Name.Contains(search) || p.Surname.Contains(search) || p.Patronimic.Contains(search));
            }
            var result = users.OrderBy(p => p.Surname).ToList().ToPagedList(page ?? 1, 3);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var authDB = new ApplicationDbContext();
            ViewBag.Roles = new SelectList(authDB.Roles, "Name", "Name");
            return View(new Person());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            var authDB = new ApplicationDbContext();
            var db = new StudentAbsenceEntities();
            ViewBag.Roles = new SelectList(authDB.Roles, "Name", "Name");

            if (!db.IsLoginUnique(person.Login))
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует!");
            }

            if (ModelState.IsValid)
            {
                var store = new UserStore<ApplicationUser>(authDB);
                var manager = new ApplicationUserManager(store);
                var authUser = new ApplicationUser() { UserName = person.Login, Email = person.Email };
                await manager.CreateAsync(authUser, person.Password);
                var role = authDB.Roles.First(r => r.Name == person.Role);
                var savedAuthUser = authDB.Users.First(u => u.UserName == person.Login);
                authUser.Roles.Add(new IdentityUserRole() { RoleId = role.Id, UserId = savedAuthUser.Id });
                authDB.SaveChanges();

                var savedPerson = db.Person.Add(person);
                var userRole = db.Role.First(r => r.Role1 == person.Role);
                db.User.Add(new User() { login = person.Login, PersonId = savedPerson.Id, Role = userRole });
                db.SaveChanges();

                if (person.Role == "Студент")
                {
                    db.Student.Add(new Student() { PersonId = savedPerson.Id, RecordBookNumber = "0000000", BirthDate = DateTime.Today, GroupId = null });
                }
                else
                {
                    if (person.Role == "Преподаватель")
                    {
                        db.Teacher.Add(new Teacher() { PersonId = savedPerson.Id});
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var db = new StudentAbsenceEntities();
            var user = db.Person.First(p => p.Id == id);
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new StudentAbsenceEntities();
            var user = db.Person.First(u => u.Id == id);
            return View(user);
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var authDB = new ApplicationDbContext();
            var db = new StudentAbsenceEntities();
            var dbPerson = db.Person.FirstOrDefault(p => p.Id == id);
            if (dbPerson != null)
            {
                var userName = db.User.FirstOrDefault(u => u.PersonId == id).login;
                db.Person.Remove(dbPerson);
                db.SaveChanges();

                var authUser = authDB.Users.FirstOrDefault(u => u.UserName == userName);
                authDB.Users.Remove(authUser);
                authDB.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}