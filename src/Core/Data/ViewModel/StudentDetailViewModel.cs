using Core.Data.DTO;
using System.Collections.Generic;

namespace Core.Data.ViewModel
{
    public class StudentDetailViewModel
    {
        public StudentDTO Student { get; set; }
        public List<GradeLessonResultDTO> SchoolLessonResults { get; set; }
    }
}
