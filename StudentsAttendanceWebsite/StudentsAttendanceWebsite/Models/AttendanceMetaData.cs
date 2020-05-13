using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    [MetadataType(typeof(AttendanceMetaData))]
    public partial class Attendance
    {
        public string StudentName
        {
            get
            {
                return Student.FullName;
            }
        }

        public string FullInfo
        {
            get
            {
                return Lesson.Date.ToShortDateString() + "    " +  Lesson.Subject.Name;
            }
        }
    }
    public class AttendanceMetaData
    {
        [Required]
        [Display(Name = "Отсутствующий студент")]
        public Nullable<int> StudentId { get; set; }
    }
}