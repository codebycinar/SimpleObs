using System.Collections.Generic;

namespace Core.Data.DTO
{
    public class GradeDTO
    {
        public string GradeName { get; set; }
        public List<GradeLessonResultDTO> GradeLessonResults { get; set; }
    }
}
