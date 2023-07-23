using FluentAssertions;
using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace IMSystemUI.Test;

public class ShelveTypeTest
{
    private IShelveTypeService _shelvetypeSrv;
    private ShelveTypeController shelvetypeController;
    public static string token =
           "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlNhbUBnbWFpbC5jb20iLCJuYW1laWQiOiI3MjkwMTI5MC02MjUyLTRiNjAtYjhmZS1jMDdkOWQ3MzAyOWMiLCJlbWFpbCI6IlNhbUBnbWFpbC5jb20iLCJuYmYiOjE2ODUwNDA3MDQsImV4cCI6MTY4NTY0NTUwNCwiaWF0IjoxNjg1MDQwNzA0fQ.fKbVv_GAskCUnAveMUYfCleWZszqKmsPkSz07iUeGME";

    [SetUp]
    public void Setup()
    {
        _shelvetypeSrv = Substitute.For<IShelveTypeService>();
        shelvetypeController = new ShelveTypeController(_shelvetypeSrv);
    }

    [Test]
    public void GivenShelveTypeReturned_WhenGetAllShelveTypesAsyncCalled_ShouldReturnOk()
    {
        //Arrange : given
        var shelvetypeList = new List<ShelveType>();

        for (var i = 1; i <= 5; i++)
        {
            var department = new ShelveType
            {
                ShelfId = Guid.NewGuid(),
                ShelfTag = $"Shelf_{i}"
            };
            shelvetypeList.Add(department);
        }
        _shelvetypeSrv.GetAllShelveTypesAsync(token).Returns(shelvetypeList);

        //Act : when
        var result = shelvetypeController.Index();

        //Assert : then
        result.Should().BeOfType<Task<ActionResult>>();
        Assert.That(shelvetypeList.Count, Is.EqualTo(5));
    }

    [Test]
    public void GivenShelveTypeReturned_WhenGetAllShelveTypeAsyncCalledWithValidId_ShouldReturnOk()
    {
        //Arrange : given
        Guid.TryParse("1", out Guid CreatedByid);
        var shelvetypeList = new List<ShelveType>();

        var department = new ShelveType
        {
            ShelfId = Guid.NewGuid(),
            ShelfTag = $"Shelf_1"
        };

        _shelvetypeSrv.GetAllShelveTypeAsync(CreatedByid, token).Returns(department);

        //Act : when
        var result = shelvetypeController.Details(CreatedByid);
        var model = result.Result as ViewResult;

        //Assert : then
        result.Should().BeOfType<Task<ActionResult>>();
        model.Model.Should().BeSameAs(department);
    }
}
