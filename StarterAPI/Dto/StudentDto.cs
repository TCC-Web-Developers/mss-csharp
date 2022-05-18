namespace StarterAPI.Dto
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string StudentNo { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime? DateEnrolled { get; set; }
        public DateTime BirthDate { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
    }
}
