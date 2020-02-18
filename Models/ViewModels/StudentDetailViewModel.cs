using Models.DTOs;
using System.Collections.Generic;

namespace Models.ViewModels
{
    public class StudentDetailViewModel
    {
        public StudentDTO Student { get; set; }
        public List<GradeLessonResultDTO> SchoolLessonResults { get; set; }
    }
}
