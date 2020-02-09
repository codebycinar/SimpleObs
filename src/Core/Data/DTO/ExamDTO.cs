using Core.Data.Entity;

namespace Core.Data.DTO
{
    public class ExamDTO
    {
        public string ExamName { get; set; }
        public ExamTypes ExamType { get; set; }
        public LessonDTO Lesson { get; set; }
        public decimal Result { get; set; }
    }
}
