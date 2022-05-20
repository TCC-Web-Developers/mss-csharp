using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarterAPI.Dto;
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
        IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }       


        // Get: api/student
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentService.Get();
            return Ok(new { data = students });
        }

        [HttpGet("ledger")]
        public async Task<IActionResult> GetStudents(string searchKey)
        {
            var students = await _studentService.Get(searchKey);
            return Ok(new { data = students });
        }

        // Get: api/student/{studentId}
        // Get: api/student/profile?studentId=
        [HttpGet("profile")]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            var student = await _studentService.Get(studentId);

            return Ok(new { data = student });


        }
        // Post: api/student
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentDto request, CancellationToken ct = default)
        {
            var newStudent = await _studentService.CreateStudent(request, ct);
            return Ok(new { data = newStudent});

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int studentId, CancellationToken ct = default)
        {
            var isdeleted = await _studentService.DeleteStudent(studentId, ct);

            return Ok(new { data = isdeleted });

        }
    }
}
