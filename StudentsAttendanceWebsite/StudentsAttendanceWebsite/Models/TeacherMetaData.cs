using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public partial class Teacher
    {
        public string FullName { get { return Person.FullName; } }
    }
}