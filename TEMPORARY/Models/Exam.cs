using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TEMPORARY.Models
{
    public partial class Exam
    {

        [DisplayName("Dərsin kodu")]
        public string LessonCode { get; set; } = null!;

        [DisplayName("Şagirdin nömrəsi")]
        public int StudentNo { get; set; }

        [DisplayName("İmtahan tarixi")]
        public DateTime? ExamDate { get; set; }

        [DisplayName("İmtahan nəticəsi")]
        public int? ExamGrade { get; set; }

        public virtual Lesson LessonCodeNavigation { get; set; } = null!;
        public virtual Student StudentNoNavigation { get; set; } = null!;
    }
}
