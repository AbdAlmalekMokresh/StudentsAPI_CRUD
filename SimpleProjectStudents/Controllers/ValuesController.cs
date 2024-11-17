using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer;
using StudentDataAccessLayer;

namespace SimpleProjectStudents.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("All", Name ="GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            List<StudentDTO> studentList = clsStudent.GetAllStudents();
            if (studentList.Count == 0)
                return NotFound("Not Found Students");
            
            return Ok(studentList);
        }


    }
}
