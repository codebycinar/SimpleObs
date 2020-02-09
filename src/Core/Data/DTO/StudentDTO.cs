using Core.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.DTO
{
    public class StudentDTO : ExamCalculator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentCode { get; set; }
        public GradeDTO Class { get; set; }
        public List<ExamDTO> ExamsResults { get; set; }

        public override decimal CalculatePercentage()
        {
            /* [(Yazılı notların aritmetik ortalaması)+Sözlü notlar] / girilen not sayısı (yazılı notlar 1 not olarak hesaplanır) */
            var writtenExams = ExamsResults.Where(x => x.ExamType == ExamTypes.Yazili);
            var speechExams = ExamsResults.Where(x => x.ExamType == ExamTypes.Sozlu);
            var result = (writtenExams.Average(x => x.Result) + speechExams.Sum(x => x.Result)) / (1 + speechExams.Count());
            return result;
        }
    }
}
