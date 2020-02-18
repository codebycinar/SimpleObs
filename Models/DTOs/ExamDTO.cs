using Core.Data.Entity;

namespace Models.DTOs
{
    public class ExamDTO
    {
        public string ExamName { get; set; }
        public ExamTypes ExamType { get; set; }
        public LessonDTO Lesson { get; set; }
    }
}
