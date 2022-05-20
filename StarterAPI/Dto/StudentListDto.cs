namespace StarterAPI.Dto
{
    public class StudentLedgerItemDto
    {
        public int StudentId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string StudentNo { get; set; } = string.Empty;
        public DateTime? DateEnrolled { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
    }
}
