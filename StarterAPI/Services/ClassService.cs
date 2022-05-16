using StarterAPI.Entities;
using StarterAPI.Interfaces;

namespace StarterAPI.Services
{
    public class ClassService : IClassService
    {
        IApplicationDbContext _context;

        public ClassService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Class> CreateClass(Class request, CancellationToken ct)
        {

            var newClass = new Class
            {
                Name = request.Name,
                Unit = request.Unit,
                Description = request.Description,
                ClassCode = request.ClassCode,

            };

            _context.Classes.Add(newClass);

            await _context.SaveChangesAsync(ct);

            return newClass;

        }

        public async Task<bool> DeleteClass(int classId, CancellationToken ct)
        {
            var classItem = await _context.Classes.FindAsync(new object[] { classId });

            if (classItem == null)
            {
                throw new KeyNotFoundException("Class not found");
            }

            _context.Classes.Remove(classItem);

            await _context.SaveChangesAsync(ct);

            return true;

        }

        public IEnumerable<Class> Get()
        {
            return _context.Classes.ToList();
        }

        public async Task<Class> Get(int classId)
        {
            var classItem = await _context.Classes.FindAsync(new object[] { classId });

            if (classItem == null)
            {
                throw new KeyNotFoundException("Class not found");
            }

            return classItem;
        }

        public async Task<Class> UpdateClass(Class request, CancellationToken ct)
        {
            var classItem = await _context.Classes.FindAsync(new object[] { request.ClassId });

            if (classItem == null)
            {
                throw new KeyNotFoundException("Class not found");
            }

            //TODO: Check if class code is unique

            classItem.Name = request.Name;
            classItem.Unit = request.Unit;
            classItem.Description = request.Description;
            classItem.ClassCode = request.ClassCode;

            await _context.SaveChangesAsync(ct);

            return classItem;
        }
    }
}
