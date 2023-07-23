using FluentAssertions;
using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
//using Moq;

namespace IMSystemUI.Test;

public class DepartmentTest
{
    private IDepartmentService _idepartmentSrv;
    private DepartmentController departmentController;
    public static string token =
           "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlNhbUBnbWFpbC5jb20iLCJuYW1laWQiOiI3MjkwMTI5MC02MjUyLTRiNjAtYjhmZS1jMDdkOWQ3MzAyOWMiLCJlbWFpbCI6IlNhbUBnbWFpbC5jb20iLCJuYmYiOjE2ODUwNDA3MDQsImV4cCI6MTY4NTY0NTUwNCwiaWF0IjoxNjg1MDQwNzA0fQ.fKbVv_GAskCUnAveMUYfCleWZszqKmsPkSz07iUeGME";

    [SetUp]
    public void Setup()
    {
        _idepartmentSrv = Substitute.For<IDepartmentService>();
        departmentController = new DepartmentController(_idepartmentSrv);
    }

    [Test]
    public void GivenDepartmentReturned_WhenGetAllDepartmentCalled_ShouldReturnOk()
    {
        //Arrange : given
        var departmentList = new List<Department>();

        for (var i = 1; i <= 5; i++)
        {
            var department = new Department
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = $"Department_{i}"
            };
            departmentList.Add(department);
        }
        _idepartmentSrv.GetAllDepartmentsAsync(token).Returns(departmentList);

        //Act : when
        var result = departmentController.Index();

        //Assert : then
        result.Should().BeOfType<Task<ActionResult>>();
        Assert.That(departmentList.Count, Is.EqualTo(5));
    }
}
