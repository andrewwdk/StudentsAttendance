using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    [MetadataType(typeof(FacultyMetaData))]
    public partial class Faculty
    {
    }
    public class FacultyMetaData
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}