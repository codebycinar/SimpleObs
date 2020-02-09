namespace Core.Data.Entity
{
    public class StudentLesson
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
