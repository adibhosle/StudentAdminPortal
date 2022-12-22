using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.Controllers;
using StudentAdminPortalAPI.DomainModels;
using Models = StudentAdminPortalAPI.Models;
using StudentAdminPortalAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject.Controllers
{
    public class StudentControllerTest
    {
        private readonly IStudentRepository studrep;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentControllerTest()
        {
            studrep = A.Fake<IStudentRepository>();
            imageRepository = A.Fake<IImageRepository>();
            mapper = A.Fake<IMapper>();  
        }

        [Fact]
        public async Task StudentController_GetStudentsAsync_ReturnOk()
        {
            //Arrange
            var students = A.Fake<ICollection<Student>>();
            var studentList = A.Fake<List<Student>>();
            A.CallTo(() => mapper.Map<List<Student>>(students)).Returns(studentList);
            var controller = new StudentController(studrep, mapper, imageRepository);

            //Act
            var result = await controller.GetStudentsAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public async Task StudentController_GetStudentAsync_ReturnOk()
        {
            var testGuid = new Guid("89087B68-3C4D-4CF4-97F4-14AD6FA955BD");
            //Arrange
            var students = A.Fake<ICollection<Student>>();
            var studentList = A.Fake<List<Student>>();
            A.CallTo(() => mapper.Map<List<Student>>(students)).Returns(studentList);
            var controller = new StudentController(studrep, mapper, imageRepository);

            //Act
            var result = await controller.GetStudentAsync(testGuid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        /*[Fact]
        public async Task StudentController_AddStudentAsync_ReturnOk()
        {
            Guid testGuid = new Guid();
            var studentmap = A.Fake<Models.Student>();
            var studentcreate = A.Fake<Student>();
            var students = A.Fake<ICollection<Student>>();
            var studentList = A.Fake<IList<Student>>();
            A.CallTo(() => mapper.Map<Models.Student>(studentcreate)).Returns(studentmap);
            A.CallTo(() => studrep.AddStudent(studentmap)).Returns(studentmap);
            var controller = new StudentController(studrep, mapper, imageRepository);

            var result = await controller.AddStudentAsync();
        } 

        [Fact]
        public async Task StudentController_DeleteStudentAsync_ReturnOk()
        {
            var testGuid = new Guid("878A72A5-7955-46FB-912D-14B2D24D018B");
            //var testGuid = Guid.NewGuid();
            //Arrange
            var students = A.Fake<ICollection<Student>>();
            var studentList = A.Fake<List<Student>>();
            A.CallTo(() => mapper.Map<List<Student>>(students)).Returns(studentList);
            var controller = new StudentController(studrep, mapper, imageRepository);

            //Act
            var result = await controller.DeleteStudentAsync(testGuid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        } */
    }
}
