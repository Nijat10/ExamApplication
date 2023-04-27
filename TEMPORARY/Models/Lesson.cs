using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TEMPORARY.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Exams = new HashSet<Exam>();
        }

        [DisplayName("Dərsin kodu")]
        public string LessonCode { get; set; } = null!;

        [DisplayName("Dərsin adı")]
        public string LessonName { get; set; } = null!;

        [DisplayName("Sinif")]
        public int Class { get; set; }

        [DisplayName("Müəllimin adı")]
        public string? TeacherName { get; set; }

        [DisplayName("Müəllimin soyadı")]
        public string? TeacherSurname { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
