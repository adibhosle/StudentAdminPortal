namespace StudentAdminPortalAPI.DomainModels
{
    public class UpdateStudent
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public Guid GenderId { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
    }
}
