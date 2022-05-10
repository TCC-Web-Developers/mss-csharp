using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StarterAPI.Controllers
{
    //Attributes
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        // Get: api/student
        [HttpGet]
        public IActionResult GetStudents()
        {

            throw new NotImplementedException();

        }

        // Get: api/student/{studentId}
        // Get: api/student/profile?studentId=
        [HttpGet("profile")]
        public IActionResult GetStudent(int studentId)
        {

            throw new NotImplementedException();

        }
        // Post: api/student
        [HttpPost]
        public IActionResult CreateStudent()
        {

            throw new NotImplementedException();

        }

        [HttpPost]
        public IActionResult DeleteStudent()
        {

            throw new NotImplementedException();

        }
    }
}
