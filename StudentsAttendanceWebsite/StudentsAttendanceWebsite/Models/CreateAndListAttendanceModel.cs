using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public class CreateAndListAttendanceModel
    {
        public List<Attendance> Absences { get; set; }
        public Attendance NewAbsence { get; set; }
    }
}