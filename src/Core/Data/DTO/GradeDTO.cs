using System.Collections.Generic;

namespace Core.Data.DTO
{
    public class GradeDTO
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public List<GradeLessonResultDTO> GradeLessonResults { get; set; }
    }
}
