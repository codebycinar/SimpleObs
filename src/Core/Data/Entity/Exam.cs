using System.Collections.Generic;
using System.ComponentModel;

namespace Core.Data.Entity
{
    public class Exam:BaseEntity
    {
        public ExamTypes ExamType { get; set; }
        public string Name { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }

    public enum ExamTypes
    {
        [Description("Yazılı")]
        Yazili = 1,
        [Description("Sözlü")]
        Sozlu = 2
    }

    public abstract class ExamCalculator
    {
        public abstract decimal CalculatePercentage();
    }
}
