﻿namespace Core.Data.DTO
{
    public class GradeLessonResultDTO
    {
        public LessonDTO Lesson { get; set; }
        public GradeDTO Grade { get; set; }
        public decimal Average { get; set; }
    }
}
