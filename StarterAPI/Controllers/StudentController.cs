using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarterAPI.Entities;
using StarterAPI.Interfaces;
using StarterAPI.Persistence;

namespace StarterAPI.Controllers
{
    //Attributes
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IApplicationDbContext _context;

        public StudentController(IApplicationDbContext context)
        {
            _context = context;
        }       


        // Get: api/student
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        // Get: api/student/{studentId}
        // Get: api/student/profile?studentId=
        [HttpGet("profile")]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(new object[] { studentId });

            if (student == null)
            {
                return BadRequest(new { error = "Student not found" });
            }

            return Ok(student);


        }
        // Post: api/student
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student param, CancellationToken ct = default)
        {

            try
            {
                var newStudent = new Student
                {
                    FirstName = param.FirstName,
                    LastName = param.LastName,
                    EmailAddress = param.EmailAddress,
                    BirthDate = param.BirthDate,
                    DateEnrolled = param.DateEnrolled,
                };

                _context.Students.Add(newStudent);

                await _context.SaveChangesAsync(ct);

                string generatedStudentNo 
                    = Convert.ToDateTime(param.DateEnrolled).ToString("MM") + "-" + newStudent.StudentId;

                newStudent.StudentNo = generatedStudentNo;

                await _context.SaveChangesAsync(ct);

                return Ok(newStudent);

            }
            catch (Exception exception)
            {


            }

            throw new NotImplementedException();

        }

        [HttpDelete]
        public IActionResult DeleteStudent()
        {

            throw new NotImplementedException();

        }
    }
}
