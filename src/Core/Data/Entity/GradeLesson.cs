namespace Core.Data.Entity
{
    public class GradeLesson : BaseEntity
    {
        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public decimal Average { get; set; }
    }
}
