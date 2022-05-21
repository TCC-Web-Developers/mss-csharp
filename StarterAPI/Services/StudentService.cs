using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StarterAPI.Commons.Extensions;
using StarterAPI.Commons.Mappings;
using StarterAPI.Commons.SharedModels;
using StarterAPI.Dto;
using StarterAPI.Entities;
using StarterAPI.Interfaces;
using StarterAPI.Queries;
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

        //public async Task<IEnumerable<StudentLedgerItemDto>> Get(string searchKey, string studentNo, string courseName, DateTime? dateEnrolledFrom, DateTime? dateEnrolledTo, PagingQuery pagingQuery)
        public async Task<IEnumerable<StudentLedgerItemDto>> Get(GetStudentLedgerQuery query)
        {
            //var students = _context.Students.ToList();

            string _studentName = query.studentName.Trim();
            string _courseName = query.courseName.Trim();

            var students = await _context.Students
                .Where(o =>
                    (
                        (String.IsNullOrEmpty(_studentName) && o.StudentId != 0)
                        || (!string.IsNullOrEmpty(_studentName) && EF.Functions.Like((o.LastName + ", " + o.FirstName).ToUpper(), $"%{_studentName.ToUpper()}%"))
                    )
                    && (
                        (String.IsNullOrEmpty(_courseName) && o.StudentId != 0)
                        || (!string.IsNullOrEmpty(_courseName) &&  EF.Functions.Like(o.CourseName, $"%{_courseName}%"))
                    )
                ) 
                .ProjectTo<StudentLedgerItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            //return _mapper.Map<IEnumerable<StudentLedgerItemDto>>(students);
            return students;

        }

        public async Task<PaginatedResult<IEnumerable<StudentLedgerItemDto>>> GetPaginatedAndSorted(GetStudentLedgerQuery request)
        {
            //var students = _context.Students.ToList();

            string _studentName = string.IsNullOrEmpty(request.studentName) ? "" : request.studentName.Trim();
            string _courseName = string.IsNullOrEmpty(request.courseName) ? "" :request.courseName.Trim();
            string _studentNo = string.IsNullOrEmpty(request.studentNo) ? "" : request.studentNo.Trim();

            var queryResult = await _context.Students
                .Where(o =>
                    (
                        (String.IsNullOrEmpty(_studentName) && o.StudentId != 0)
                        || (!string.IsNullOrEmpty(_studentName) && EF.Functions.Like((o.LastName + ", " + o.FirstName).ToUpper(), $"%{_studentName.ToUpper()}%"))
                    )
                    && (
                        (String.IsNullOrEmpty(_courseName) && o.StudentId != 0)
                        || (!string.IsNullOrEmpty(_courseName) && EF.Functions.Like(o.CourseName, $"%{_courseName}%"))
                    )
                    && (
                        (String.IsNullOrEmpty(_studentNo) && o.StudentId != 0)
                        || (!string.IsNullOrEmpty(_studentNo) && EF.Functions.Like(o.StudentNo, $"%{_studentNo}%"))
                    )
                )
                .ToPaginatedQueryResultAsync<Student, StudentLedgerItemDto>(_mapper.ConfigurationProvider, 
                        request.page, request.perPage, 
                        query => {

                            switch (request.GetSortCode())
                            {
                                case "lastname-desc":
                                    query = query.OrderByDescending(s => s.LastName);
                                    break;
                                case "lastname-asc":
                                    query = query.OrderBy(s => s.LastName);
                                    break;
                                case "firstname-desc":
                                    query = query.OrderByDescending(s => s.FirstName);
                                    break;
                                case "firstname-asc":
                                    query = query.OrderBy(s => s.FirstName);
                                    break;
                                case "coursename-desc":
                                    query = query.OrderByDescending(s => s.CourseName);
                                    break;
                                case "coursename-asc":
                                    query = query.OrderBy(s => s.CourseName);
                                    break;
                                case "dateenrolled-desc":
                                    query = query.OrderByDescending(s => s.DateEnrolled);
                                    break;
                                case "dateenrolled-asc":
                                    query = query.OrderBy(s => s.DateEnrolled);
                                    break;
                                default:
                                    query = query.OrderBy(s => s.StudentId);
                                    break;
                            }

                            return query;

                        });


            //.ProjectTo<StudentLedgerItemDto>(_mapper.ConfigurationProvider)
            //.ToListAsync();

            //return _mapper.Map<IEnumerable<StudentLedgerItemDto>>(students);
            return queryResult;

        }

    }
}
