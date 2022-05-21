using StarterAPI.Commons.SharedModels;
using StarterAPI.Dto;
using StarterAPI.Entities;
using StarterAPI.Queries;

namespace StarterAPI.Interfaces
{
    public interface IStudentService 
    {
        IEnumerable<StudentDto> Get();

        //Task<IEnumerable<StudentLedgerItemDto>> Get(string searchKey, string studentNo, string courseName, DateTime? dateEnrolledFrom, DateTime? dateEnrolledTo, PagingQuery pagingQuery);
        Task<IEnumerable<StudentLedgerItemDto>> Get(GetStudentLedgerQuery query);

        Task<PaginatedResult<IEnumerable<StudentLedgerItemDto>>> GetPaginatedAndSorted(GetStudentLedgerQuery query);

        Task<StudentDto> Get(int studentId);

        Task<StudentDto> CreateStudent(StudentDto request, CancellationToken ct);

        Task<StudentDto> UpdateStudent(StudentDto request, CancellationToken ct);

        Task<bool> DeleteStudent(int studentId, CancellationToken ct);

    }
}
