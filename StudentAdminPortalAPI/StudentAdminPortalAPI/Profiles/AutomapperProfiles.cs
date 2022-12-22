using AutoMapper;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Profiles.AfterMaps;
using Models = StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Profiles
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Models.Student, Student>()
                .ReverseMap();

            CreateMap<Models.Gender, Gender>()
                .ReverseMap();

            CreateMap<Models.Address, Address>()
                .ReverseMap();

            CreateMap<UpdateStudent, Models.Student>()
                 .AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<AddStudentRequest, Models.Student>()
                .AfterMap<AddStudentRequestAfterMap>();
        }
    }
}
