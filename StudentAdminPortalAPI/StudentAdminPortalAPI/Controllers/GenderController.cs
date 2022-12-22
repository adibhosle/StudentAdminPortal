using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Repositories;

namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IStudentRepository studentrepo;
        private readonly IMapper mapper;

        public GenderController(IStudentRepository studentrepo, IMapper mapper)
        {
            this.studentrepo = studentrepo;
            this.mapper = mapper;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var gender = await studentrepo.GetGenderAsync();

            if(gender == null | !gender.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<Gender>>(gender));
        }
    }
}
