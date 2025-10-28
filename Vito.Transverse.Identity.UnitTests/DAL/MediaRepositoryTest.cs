using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Media;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.UnitTests.Helpers;

namespace Vito.Transverse.Identity.UnitTests.DAL;

[TestFixture]
public class MediaRepositoryTest
{
    private IDataBaseContextFactory _dbContextFactoryMock;
    private Mock<ILogger<MediaRepository>> _loggerMock = new();
    private MediaRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dbContextFactoryMock = DataContextMockHelper.GetDataBaseContextFactory();
        _repository = new MediaRepository(_dbContextFactoryMock, _loggerMock.Object);
    }

    [Test]
    public async Task GetPictureList_ReturnsPictureDTOList()
    {
        Expression<Func<Picture, bool>> filter = x => true;
        var result = await _repository.GetPictureList(filter);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<PictureDTO>>());
    }

    [Test]
    public async Task AddNewPictureAsync_ReturnsTrue()
    {
        var dto = new PictureDTO { Id = 1 };
        var result = await _repository.AddNewPictureAsync(dto);
        Assert.That(result, Is.True);
    }
}