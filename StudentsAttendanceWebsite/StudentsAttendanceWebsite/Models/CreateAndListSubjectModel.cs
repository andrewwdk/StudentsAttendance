using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public class CreateAndListSubjectModel
    {
        public List<Subject> Subjects { get; set; }
        public Subject NewSubject { get; set; }
    }
}