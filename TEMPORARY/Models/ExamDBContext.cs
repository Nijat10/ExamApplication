using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TEMPORARY.Models
{
    public partial class ExamDBContext : DbContext
    {
        public ExamDBContext()
        {
        }

        public ExamDBContext(DbContextOptions<ExamDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-L8ODPVS;Database=ExamDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => new { e.LessonCode, e.StudentNo })
                    .HasName("PK__Exams__180FA18771452F22");

                entity.Property(e => e.LessonCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LESSON_CODE")
                    .IsFixedLength();

                entity.Property(e => e.StudentNo).HasColumnName("STUDENT_NO");

                entity.Property(e => e.ExamDate)
                    .HasColumnType("date")
                    .HasColumnName("EXAM_DATE");

                entity.Property(e => e.ExamGrade).HasColumnName("EXAM_GRADE");

                entity.HasOne(d => d.LessonCodeNavigation)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.LessonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Exams__Lesson");

                entity.HasOne(d => d.StudentNoNavigation)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.StudentNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Exams__Student");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonCode)
                    .HasName("PK__Lessons__06665F753D752335");

                entity.Property(e => e.LessonCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LESSON_CODE")
                    .IsFixedLength();

                entity.Property(e => e.Class).HasColumnName("CLASS");

                entity.Property(e => e.LessonName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LESSON_NAME");

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TEACHER_NAME");

                entity.Property(e => e.TeacherSurname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TEACHER_SURNAME");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentNo)
                    .HasName("PK__Students__E69FEF205D990059");

                entity.Property(e => e.StudentNo)
                    .ValueGeneratedNever()
                    .HasColumnName("STUDENT_NO");

                entity.Property(e => e.Class).HasColumnName("CLASS");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_NAME");

                entity.Property(e => e.StudentSurname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_SURNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
