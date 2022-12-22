namespace StudentAdminPortalAPI.DomainModels
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string? ProfileImg { get; set; }
        public Guid GenderId { get; set; }

        public Gender Gender { get; set; }
        public Address Address { get; set; }
    }
}
