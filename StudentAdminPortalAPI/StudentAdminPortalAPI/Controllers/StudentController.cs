using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.DomainModels;
//using StudentAdminPortalAPI.Models;
using StudentAdminPortalAPI.Repositories;

namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studrep;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentController(IStudentRepository studrep, IMapper mapper, IImageRepository imageRepository)
        {
            this.studrep = studrep;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var student = await studrep.GetStudentsAsync();
            return Ok(mapper.Map<List<Student>>(student));
        }

        [HttpGet]
        [Route("[controller]/{studentId}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            var student = await studrep.GetStudentAsync(studentId); 
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudent request)
        {
            if (await studrep.Exists(studentId))
            {
                // Update Details
                var updatedStudent = await studrep.UpdateReq(studentId, mapper.Map<Models.Student>(request));

                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("[controller]/{studentId}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await studrep.Exists(studentId))
            {
                var student = await studrep.DeleteStudent(studentId);
                return Ok(mapper.Map<Student>(student));
            }

            return NotFound();
        }


        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            try
            {
                var student = await studrep.AddStudent(mapper.Map<Models.Student>(request));
                return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                    mapper.Map<Student>(student));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("[controller]/{studentId}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };
            
            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                { 
                    if (await studrep.Exists(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                        if (await studrep.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            } 

            return NotFound();
        }

    }
}
