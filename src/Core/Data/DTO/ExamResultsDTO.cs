using System.Collections.Generic;

namespace Core.Data.DTO
{
    public class ExamResultsDTO
    {
        public StudentDTO Student { get; set; }
        public List<ExamDTO> ExamResults { get; set; }
    }
}
