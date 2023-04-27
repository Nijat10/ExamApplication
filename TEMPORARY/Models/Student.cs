using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TEMPORARY.Models
{
    public partial class Student
    {
        public Student()
        {
            Exams = new HashSet<Exam>();
        }

        [DisplayName("Şagirdin nömrəsi")]
        public int StudentNo { get; set; }

        [DisplayName("Şagirdin adı")]
        public string StudentName { get; set; } = null!;

        [DisplayName("Şagirdin soyadı")]
        public string? StudentSurname { get; set; }

        [DisplayName("Sinifi")]
        public int Class { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
