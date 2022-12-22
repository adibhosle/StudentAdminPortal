using StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<List<Gender>> GetGenderAsync();
        Task<Student> GetStudentAsync(Guid studentId);
        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateReq(Guid studentId, Student request);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> AddStudent(Student request);
        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
    }
}
