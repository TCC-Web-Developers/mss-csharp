namespace StarterAPI.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string StudentNo { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime? DateEnrolled { get; set; }

    }


}
