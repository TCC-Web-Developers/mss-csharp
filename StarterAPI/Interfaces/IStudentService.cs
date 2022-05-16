using StarterAPI.Entities;

namespace StarterAPI.Interfaces
{
    public interface IStudentService 
    {

        IEnumerable<Student> Get();

        Task<Student> Get(int studentId);

        Task<Student> CreateStudent(Student request, CancellationToken ct);

        Task<Student> UpdateStudent(Student request, CancellationToken ct);

        Task<bool> DeleteStudent(int studentId, CancellationToken ct);

    }
}
