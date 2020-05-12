using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public class CreateAndListGroupModel
    {
        public List<Group> Groups { get; set; }
        public Group NewGroup { get; set; }
    }
}