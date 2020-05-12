using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {
        public string FullName
        {
            get
            {
                return Person.Surname + " " + Person.Name + " " + Person.Patronimic;
            }
        }
    }
    public class StudentMetaData
    {
        [Required]
        [Display(Name = "Зачётная книжка")]
        [MaxLength(7, ErrorMessage = "Длина номера зачётной книжки не должна превышать 7 символов!")]
        [MinLength(7, ErrorMessage = "Длина номера зачётной книжки не должна меньше 7 символов!")]
        public string RecordBookNumber { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime BirthDate { get; set; }

        [Display(Name = "Группа")]
        public Nullable<int> GroupId { get; set; }
    }
}