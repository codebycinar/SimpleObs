namespace Core.Data.Entity
{
    public class StudentLesson
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        //Sınav Notlarının ortalamalarının hesaplanması sonucunda dersten aldığı not
        public decimal Result { get; set; }
    }
}
