using System.Collections.Generic;

namespace Core.Data.Entity
{
    /// <summary>
    /// Ders sınıfımız
    /// Tüm nesnelerimizin ortak özelliği olan Id özelliğini barındırır.
    /// </summary>
    public class Lesson:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Exam> LessonExams { get; set; }
        public ICollection<StudentLesson> LessonStudents { get; set; }

        public ICollection<GradeLesson> LessonResults { get; set; }
    }
}
