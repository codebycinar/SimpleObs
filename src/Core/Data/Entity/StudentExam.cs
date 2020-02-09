namespace Core.Data.Entity
{
    public class StudentExam 
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public decimal Result { get; set; }
    }
}
