using StarterAPI.Dto;
using StarterAPI.Entities;

namespace StarterAPI.Interfaces
{
    public interface IClassService
    {

        IEnumerable<ClassDto> Get();
        Task<ClassDto> Get(int classId);
        Task<ClassDto> CreateClass(ClassDto request, CancellationToken ct); 
        Task<ClassDto> UpdateClass(ClassDto request, CancellationToken ct);

        Task<bool> DeleteClass(int classId, CancellationToken ct);



    }
}
