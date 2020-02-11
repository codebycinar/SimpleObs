using System.Collections.Generic;

namespace Core.Data.Entity
{
    public class Student:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentCode { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set; }
    }
}
