using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer;
using StudentDataAccessLayer;

namespace SimpleProjectStudents.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
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


        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            List<StudentDTO> PassedStudentsList = clsStudent.GetPassedStudents();
            if (PassedStudentsList.Count == 0)
                return NotFound("Not Found Students");
            return Ok(PassedStudentsList);
        }


        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> GetAverageGrade()
        {
            double avg = clsStudent.GetAverageGrade();
            return Ok(avg);
        }
    }
}
