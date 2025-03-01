﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudentByID(int id)
        {
            if (id < 1)
                return BadRequest($"Not Accepted ID {id}");

            clsStudent student = clsStudent.Find(id);
            // here, we can make a SAVE(), UPDATE() ,...  

            if (student == null)
                return NotFound($"ٍStudent with ID {id} not found");

            StudentDTO SDTO = student.SDTO;
            return Ok(SDTO);
        }

        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.age < 18)
                return BadRequest("Invalid Student data");

            clsStudent student = new clsStudent(new StudentDTO(newStudentDTO.Id, newStudentDTO.Name, newStudentDTO.age, newStudentDTO.grade));
         
            
            if(student.Save())
            {
                newStudentDTO.Id = student.ID;
                return CreatedAtRoute("GetStudentById", new { id = newStudentDTO.Id }, newStudentDTO);
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }



        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<StudentDTO> UpdateStudent(int id,  StudentDTO updatedStudent)
        {
            if(id < 1 || UpdateStudent == null || string.IsNullOrEmpty(updatedStudent.Name))
            {
                return BadRequest("Invalid student data");
            }

            clsStudent student = clsStudent.Find(id);
            if (student == null)
                return NotFound($"Student with ID {id} Not Found");

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.age;
            student.Grade = updatedStudent.grade;

            if (student.Save())
            {
                return Ok(student.SDTO);
            }
            else return BadRequest("an error occurred");
        }


        [HttpDelete("Id", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
                return BadRequest($"Not Accepted ID {id}");

            if (clsStudent.DeleteStudent(id))
                return Ok("Deleted Succesfully");
            else return NotFound($"Student with ID {id} not found");
        }



        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("no file uploaded");

            var uploadDirectory = @"D:\Projects\CURD API Project\Images";

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            if (!Directory.Exists(uploadDirectory))  // ensure the directory exist, if not create a new directory
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }
        [HttpDelete("DeletePerson")]
        public IActionResult DeletePerson(int ID)
        {
            var isDeleted = StudentData.DeleteStudent(ID);
            if (isDeleted)
                return Ok("Deleted Successfully");
            else return BadRequest("can't find this person");
        }

        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var uploadDirectory = @"D:\Projects\CURD API Project\Images";
            var filePath = Path.Combine(uploadDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found");

            var image = System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);

            return File(image, mimeType);
        }

        private string GetMimeType(string filepath)
        {
            var extension = Path.GetExtension(filepath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }
        
  
    }
}
