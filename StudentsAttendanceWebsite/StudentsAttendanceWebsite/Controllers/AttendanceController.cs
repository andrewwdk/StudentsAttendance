using StudentsAttendanceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsAttendanceWebsite.Controllers
{
    public class AttendanceController : Controller
    {
        public ActionResult Create(int id)
        {
            var db = new StudentAbsenceEntities();
            var lesson = db.Lesson.First(l => l.Id == id);
            var studentsInGroup = db.Student.Where(s => s.GroupId == lesson.GroupId).ToList();
            RemoveAbsentStudents(studentsInGroup, lesson.Id);
            ViewBag.Students = new SelectList(studentsInGroup, "PersonId", "FullName");
            var list = db.Attendance.Select(c => c).OrderBy(c => c.Student.Person.Surname).ToList();
            return View(new CreateAndListAttendanceModel() { Absences = list, NewAbsence = new Attendance() { IsAbsent = true, LessonId = id} });
        }

        [HttpPost]
        public ActionResult Create(CreateAndListAttendanceModel model)
        {
            var db = new StudentAbsenceEntities();

            if (ModelState.IsValid)
            {
                db.Attendance.Add(model.NewAbsence);
                db.SaveChanges();
                return RedirectToAction("Create", new { id = model.NewAbsence.LessonId });
            }

            var studentsInGroup = db.Student.Where(s => s.GroupId == model.NewAbsence.LessonId).ToList();
            RemoveAbsentStudents(studentsInGroup, model.NewAbsence.LessonId);
            ViewBag.Students = new SelectList(studentsInGroup, "PersonId", "FullName");
            var list = db.Attendance.Select(c => c).OrderBy(c => c.Student.Person.Surname).ToList();
            return View(new CreateAndListAttendanceModel() { Absences = list, NewAbsence = model.NewAbsence });
        }

        [HttpPost]
        public ActionResult Delete(int lessonId, int studentId)
        {
            var db = new StudentAbsenceEntities();
            var absence = db.Attendance.FirstOrDefault(s => s.LessonId == lessonId && s.StudentId == studentId);
            db.Attendance.Remove(absence);
            db.SaveChanges();
            return RedirectToAction("Create", new { id = lessonId });
        }

        private void RemoveAbsentStudents(List<Student> students, int lessonId)
        {
            var db = new StudentAbsenceEntities();
            for (int i = 0; i < students.Count; i++)
            {
                var student = students[i];
                var attendance = db.Attendance.FirstOrDefault(a => a.StudentId == student.PersonId && a.LessonId == lessonId);
                if(attendance != null)
                {
                    students.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}