using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public class CreateAndListLessonModel
    {
        public List<Lesson> Lessons { get; set; }
        public Lesson NewLesson { get; set; }
    }
}