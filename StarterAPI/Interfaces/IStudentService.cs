using StarterAPI.Commons.SharedModels;
using StarterAPI.Dto;
using StarterAPI.Entities;

namespace StarterAPI.Interfaces
{
    public interface IStudentService 
    {
        IEnumerable<StudentDto> Get();

        //Task<IEnumerable<StudentListDto>> Get(string searchKey, string studentNo, string courseName, DateTime dateEnrolledFrom, DateTime dateEnrolledTo, PagingQuery pagingQuery)
        Task<IEnumerable<StudentLedgerItemDto>> Get(string searchKey);

        Task<StudentDto> Get(int studentId);

        Task<StudentDto> CreateStudent(StudentDto request, CancellationToken ct);

        Task<StudentDto> UpdateStudent(StudentDto request, CancellationToken ct);

        Task<bool> DeleteStudent(int studentId, CancellationToken ct);

    }
}
