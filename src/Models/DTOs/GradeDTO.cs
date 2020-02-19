using System.Collections.Generic;

namespace Models.DTOs
{
    public class GradeDTO
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public List<GradeLessonResultDTO> GradeLessonResults { get; set; }
    }
}
