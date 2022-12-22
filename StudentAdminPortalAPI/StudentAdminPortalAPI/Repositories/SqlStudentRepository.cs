using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentDbContext context;

        public SqlStudentRepository(StudentDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        } 
        
       public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGenderAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Student.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateReq(Guid studentId, Student request)
        {
            var updatestudent = await GetStudentAsync(studentId);

            if(updatestudent != null)
            {
                updatestudent.Name = request.Name;
                updatestudent.DateOfBirth = request.DateOfBirth;
                updatestudent.Email = request.Email;
                updatestudent.GenderId = request.GenderId;
                updatestudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                updatestudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync();
                return updatestudent;
            }
            return null;
            
        }


        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);

            if (student != null)
            {
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }

            return null;
        }


        public async Task<Student> AddStudent(Student request)
        {
            var student = await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }


        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentAsync(studentId);

            if (student != null)
            {
                student.ProfileImg = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }



    }
}
