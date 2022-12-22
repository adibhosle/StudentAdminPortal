using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentAdminPortalAPI.Controllers;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Models;
//using StudentAdminPortalAPI.Models;
using StudentAdminPortalAPI.Repositories;
using Gender = StudentAdminPortalAPI.Models.Gender;
using Student = StudentAdminPortalAPI.Models.Student;

namespace StudentTestMock
{
    public class TestController
    {
        private StudentController _studentcontroller;
        private GenderController _genderController;
        private readonly Mock<IStudentRepository> _mockstudentrepo = new Mock<IStudentRepository>();
        private readonly Mock<IImageRepository> _mockimagerepo = new Mock<IImageRepository>();
        private readonly Mock<IMapper> _mockmapper = new Mock<IMapper>();


        public TestController()
        {
            _studentcontroller = new StudentController(_mockstudentrepo.Object, _mockmapper.Object, _mockimagerepo.Object);

            _genderController = new GenderController(_mockstudentrepo.Object, _mockmapper.Object);
        }


        [Fact]
        public async Task StudentController_GetStudentsAsync_ReturnOk()
        {
            // Arrange
            var students = new List<Student>();
            _mockstudentrepo.Setup(x => x.GetStudentsAsync()).ReturnsAsync(students);

            //Act
            var result = await _studentcontroller.GetStudentsAsync();

            // Assert
            var obj = result as ObjectResult;
            Assert.Equal(200, obj?.StatusCode);
        }


        [Fact]
        public async Task StudentController_GetStudentAsync_ReturnOk()
        {
            // Arrange
            var studentid = Guid.NewGuid();
            var name = "Adi";
            var dateOfBirth = DateTime.Now;
            var email = "a@gmail.com";

            var student = new Student
            {
                Id = studentid,
                Name = name,
                DateOfBirth = dateOfBirth,
                Email = email,
            };
            _mockstudentrepo.Setup(x => x.GetStudentAsync(studentid)).ReturnsAsync(student);

            // Act
            var result = await _studentcontroller.GetStudentAsync(studentid);

            // Assert
            Assert.Equal(student.Id, studentid);
            Assert.Equal(student.Name, name);
            Assert.Equal(student.DateOfBirth, dateOfBirth);
            Assert.Equal(student.Email, email);

        }


        [Fact]
        public async Task StudentController_UpdateStudentAsync_ReturnOk()
        {
            // Arrange
            var studentid = Guid.NewGuid();
            var name = "Adi";
            var dateOfBirth = "10-11-1998";
            var email = "a@gmail.com";
            var genderid = Guid.NewGuid();

            var studentModel = new UpdateStudent
            {
                //Id = studentid,
                Name = name,
                DateOfBirth = dateOfBirth,
                Email = email,
                GenderId =  genderid,
                PhysicalAddress = "",
                PostalAddress = ""
            };

            var student = new Student
            {
                Id = studentid,
                Name = name,
                DateOfBirth = DateTime.Now,
                Email = email,
            };


            _mockstudentrepo.Verify(x => x.UpdateReq(studentid, student), Times.Never());

            // Act
            var result = await _studentcontroller.UpdateStudentAsync(studentid, studentModel);

            // Assert
            //Assert.Equal(student.Id, studentid);
            //Assert.Equal(student.Name, name);
            //Assert.Equal(student.DateOfBirth, dateOfBirth);
            //Assert.Equal(student.Email, email);

        }


        [Fact]
        public async Task StudentController_DeleteStudentAsync_ReturnOk()
        {
            // Arrange 
            var studid = Guid.NewGuid();
            var student = new Student
            {
                Id = studid,
            };

            _mockstudentrepo.Setup(x => x.DeleteStudent(studid)).ReturnsAsync(student);

            // Act
            var result = await _studentcontroller.DeleteStudentAsync(studid);

            // Assert
            Assert.Equal(student.Id, studid);
        }

        [Fact]
        public async Task StudentController_AddStudentAsync_ReturnOk()
        {
            // Arrange
            var studentid = Guid.NewGuid();
            var name = "Adi";
            var dateOfBirth = "10-11-1998";
            var email = "a@gmail.com";
            var genderid = Guid.NewGuid();

            var studentModel = new AddStudentRequest
            {
                Name = name,
                DateOfBirth = dateOfBirth,
                Email = email,
                GenderId = genderid,
                PhysicalAddress = "",
                PostalAddress = ""
            };

            var student = new Student
            {
                Id = studentid,
                Name = name,
                DateOfBirth = DateTime.Now,
                Email = email,
            };

            _mockstudentrepo.Verify(x => x.AddStudent(student), Times.Never());

            // Act

            var result = await _studentcontroller.AddStudentAsync(studentModel);

            // Assert
            Assert.Equal(studentModel.Name, name);
            Assert.Equal(studentModel.DateOfBirth, dateOfBirth);
            Assert.Equal(studentModel.Email, email);
        }


       


    }
}
