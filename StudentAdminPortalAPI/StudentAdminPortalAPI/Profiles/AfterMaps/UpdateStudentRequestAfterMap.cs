using AutoMapper;
using StudentAdminPortalAPI.DomainModels;
using Models = StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudent, Models.Student>
    {
        public void Process(UpdateStudent source, Models.Student destination, ResolutionContext context)
        {
            destination.Address = new Models.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
