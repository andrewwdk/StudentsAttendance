using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    [MetadataType(typeof(GroupMetaData))]
    public partial class Group
    {
        public string FullName { get { return Name + " " + Faculty.Name; } }
    }

    public class GroupMetaData
    {
        [Required]
        [Display(Name = "Номер группы")]
        [MaxLength(6)]
        [MinLength(6)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Номер группы должен содержать только цифры!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        public Nullable<int> FacultyId { get; set; }
    }
}