using AutoMapper;
using StarterAPI.Dto;
using StarterAPI.Entities;
using StarterAPI.Interfaces;

namespace StarterAPI.Services
{
    public class ClassService : IClassService
    {
        IApplicationDbContext _context;
        IMapper _mapper;

        public ClassService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClassDto> CreateClass(ClassDto request, CancellationToken ct)
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

            return _mapper.Map<ClassDto>(newClass);

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

        public IEnumerable<ClassDto> Get()
        {
            var classes = _context.Classes.ToList();
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }

        public async Task<ClassDto> Get(int classId)
        {
            var classItem = await _context.Classes.FindAsync(new object[] { classId });

            if (classItem == null)
            {
                throw new KeyNotFoundException("Class not found");
            }

            return _mapper.Map<ClassDto>(classItem); 
        }

        public async Task<ClassDto> UpdateClass(ClassDto request, CancellationToken ct)
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

            return _mapper.Map<ClassDto>(classItem);
        }
    }
}
