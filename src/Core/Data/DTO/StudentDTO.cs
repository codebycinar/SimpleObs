namespace Core.Data.DTO
{
    public class StudentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentCode { get; set; }
        public GradeDTO Grade { get; set; }
    }
}
