using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public class CreateAndListFacultyModel
    {
        public List<Faculty> Faculties { get; set; }
        public Faculty NewFaculty { get; set; }
    }
}