using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    [MetadataType(typeof(LessonMetaData))]
    public partial class Lesson
    {
        public string FullName
        {
            get
            {
                if (Teacher != null)
                {
                    return Date.ToShortDateString() + "    " + Group.Name + "    " + Subject.Name + "    " + Teacher.Person.Surname +
                         " " + Teacher.Person.Name[0] + ". " + Teacher.Person.Patronimic[0] + ".    " + ClassroomNumber;
                }
                else
                {
                    return Date.ToShortDateString() + "    " + Group.Name + "    " + Subject.Name + "    " + ClassroomNumber;
                }
            }
        }
    }
    public class LessonMetaData
    {
        [Required]
        [Display(Name = "Аудитория")]
        public string ClassroomNumber { get; set; }

        [Required]
        [Display(Name = "Дата")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime Date { get; set; }

        [Required]
        [Display(Name = "Группа")]
        public Nullable<int> GroupId { get; set; }

        [Display(Name = "Преподаватель")]
        public Nullable<int> TeacherId { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public Nullable<int> SubjectId { get; set; }
    }
}