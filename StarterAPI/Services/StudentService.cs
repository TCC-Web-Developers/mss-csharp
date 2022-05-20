using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StarterAPI.Commons.Mappings;
using StarterAPI.Commons.SharedModels;
using StarterAPI.Dto;
using StarterAPI.Entities;
using StarterAPI.Interfaces;
using System.Collections.Generic;
using System.Dynamic;

namespace StarterAPI.Services
{
    public class StudentService : IStudentService
    {
        IApplicationDbContext _context;
        IMapper _mapper;

        public StudentService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<StudentDto> Get()
        {
            var students = _context.Students.ToList();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> Get(int studentId)
        {
            var student = await _context.Students.FindAsync(new object[] { studentId });

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> CreateStudent(StudentDto request, CancellationToken ct)
        {
            var newStudent = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                BirthDate = request.BirthDate,
                DateEnrolled = request.DateEnrolled,
                Address = request.Address,
                CourseName = request.CourseName,
                ContactNo = request.ContactNo,
                Profile = request.Profile,
            };

            _context.Students.Add(newStudent);

            await _context.SaveChangesAsync(ct);

            string generatedStudentNo
                = Convert.ToDateTime(request.DateEnrolled).ToString("yyyyMM") + "-" + newStudent.StudentId;

            newStudent.StudentNo = generatedStudentNo;

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<StudentDto>(newStudent);
        }

        public async Task<StudentDto> UpdateStudent(StudentDto request, CancellationToken ct)
        {
            var student = await _context.Students.FindAsync(new object[] { request.StudentId });

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Address = request.Address;
            student.EmailAddress = request.EmailAddress;
            student.BirthDate = request.BirthDate;
            student.CourseName = request.CourseName;
            student.ContactNo = request.ContactNo;
            student.Profile = request.Profile;

            await _context.SaveChangesAsync(ct);

            return _mapper.Map<StudentDto>(student);

        }

        public async Task<bool> DeleteStudent(int studentId, CancellationToken ct)
        {
            var student = await _context.Students.FindAsync(new object[] { studentId });

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync(ct);

            return true;
        }

        //public async Task<IEnumerable<StudentListDto>> Get(string searchKey, string studentNo, string courseName, DateTime dateEnrolledFrom, DateTime dateEnrolledTo, PagingQuery pagingQuery)
        public async Task<IEnumerable<StudentLedgerItemDto>> Get(string searchKey)
        {
            //var students = _context.Students.ToList();

            var students = await _context.Students
                .Where(o => EF.Functions.Like((o.LastName + ", " + o.FirstName).ToUpper(), $"%{searchKey.ToUpper()}%"))
                //.Select(a => new { a.FirstName, a.LastName, a.StudentNo, a.DateEnrolled, a.CourseName, a.Profile })
                //.ProjectToListAsync<StudentLedgerItemDto>(_mapper.ConfigurationProvider);
                .ProjectTo<StudentLedgerItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
                //.ToListAsync();

            //return _mapper.Map<IEnumerable<StudentLedgerItemDto>>(students);
            return students;

        }

    }
}
