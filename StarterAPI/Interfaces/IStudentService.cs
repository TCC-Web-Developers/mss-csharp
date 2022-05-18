using StarterAPI.Dto;
using StarterAPI.Entities;

namespace StarterAPI.Interfaces
{
    public interface IStudentService 
    {

        IEnumerable<StudentDto> Get();

        Task<StudentDto> Get(int studentId);

        Task<StudentDto> CreateStudent(StudentDto request, CancellationToken ct);

        Task<StudentDto> UpdateStudent(StudentDto request, CancellationToken ct);

        Task<bool> DeleteStudent(int studentId, CancellationToken ct);

    }
}
