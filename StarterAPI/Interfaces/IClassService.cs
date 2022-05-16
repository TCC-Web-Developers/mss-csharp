using StarterAPI.Entities;

namespace StarterAPI.Interfaces
{
    public interface IClassService
    {

        IEnumerable<Class> Get();
        Task<Class> Get(int classId);
        Task<Class> CreateClass(Class request, CancellationToken ct); 
        Task<Class> UpdateClass(Class request, CancellationToken ct);

        Task<bool> DeleteClass(int classId, CancellationToken ct);



    }
}
