using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StarterAPI.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string StudentNo { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        //public string NormalizedEmailAddress { get; set; } = string.Empty;
        public DateTime? DateEnrolled { get; set; }
        public DateTime BirthDate { get; set; }
        public string CourseName { get; set; } = string.Empty;
        //public string NormalizedCourseName { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;

    }

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.EmailAddress).IsRequired();
            builder.Property(p => p.StudentNo).IsRequired();

            builder.HasIndex(p => p.StudentNo).IsUnique();         
            builder.HasIndex(p => new { p.FirstName, p.LastName });
        }
    }

}
