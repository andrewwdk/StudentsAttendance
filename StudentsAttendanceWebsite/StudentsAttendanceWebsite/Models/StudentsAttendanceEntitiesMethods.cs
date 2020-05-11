using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    public partial class StudentAbsenceEntities : DbContext
    {
        public bool IsLoginUnique(string login)
        {
            foreach (var user in User)
            {
                if (user.login == login)
                {
                    return false;
                }
            }

            return true;
        }
    }
}