using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsAttendanceWebsite.Models
{
    [MetadataType(typeof(SubjectMetaData))]
    public partial class Subject
    {
    }

    public class SubjectMetaData
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}