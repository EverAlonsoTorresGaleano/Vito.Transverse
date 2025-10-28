using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Security;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;
using Vito.Transverse.Identity.Domain.Options;
using Vito.Transverse.Identity.UnitTests.Helpers;

namespace Vito.Transverse.Identity.UnitTests.DAL;

[TestFixture]
public class SecurityRepositoryTest
{
    private IDataBaseContextFactory _dbContextFactoryMock;
    private Mock<IOptions<IdentityServiceServerSettingsOptions>> _identityServiceOptionsMock;
    private Mock<IOptions<EncryptionSettingsOptions>> _encryptionSettingsOptionsMock;
    private Mock<ILogger<SecurityRepository>> _loggerMock;
    private SecurityRepository _repository;
    private DataBaseServiceContext _dbContext;

    [SetUp]
    public void SetUp()
    {
        _dbContextFactoryMock = DataContextMockHelper.GetDataBaseContextFactory();
        _identityServiceOptionsMock = new Mock<IOptions<IdentityServiceServerSettingsOptions>>();
        _encryptionSettingsOptionsMock = new Mock<IOptions<EncryptionSettingsOptions>>();
        _loggerMock = new Mock<ILogger<SecurityRepository>>();

        _identityServiceOptionsMock.Setup(x => x.Value).Returns(new IdentityServiceServerSettingsOptions());
        _encryptionSettingsOptionsMock.Setup(x => x.Value).Returns(new EncryptionSettingsOptions());

        _dbContext = _dbContextFactoryMock.CreateDbContext();

        _repository = new SecurityRepository(
            _dbContextFactoryMock,
            _identityServiceOptionsMock.Object,
            _encryptionSettingsOptionsMock.Object,
            _loggerMock.Object
        );
    }



    [Test]
    public async Task CreateNewApplicationAsync_ShouldAddAndReturnApplication()
    {
        var dto = new ApplicationDTO { Id = 1 };
        var device = new DeviceInformationDTO();
        var result = await _repository.CreateNewApplicationAsync(dto, device, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(dto.Id));
    }

    [Test]
    public async Task CreateNewCompanyAsync_ShouldAddAndReturnCompany()
    {
        var companyDto = new CompanyDTO { Id = 1 };
        var dto = new CompanyApplicationsDTO { Company = companyDto };
        var device = new DeviceInformationDTO();
        var result = await _repository.CreateNewCompanyAsync(dto, device, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(companyDto.Id));
    }

    [Test]
    public async Task UpdateCompanyApplicationsAsync_ShouldUpdateAndReturnCompany()
    {
        var companyDto = new CompanyDTO { Id = 1 };
        var dto = new CompanyApplicationsDTO { Company = companyDto };
        var device = new DeviceInformationDTO();
        await _repository.CreateNewCompanyAsync(dto, device, _dbContext);
        var result = await _repository.UpdateCompanyApplicationsAsync(dto, device, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(companyDto.Id));
    }

    [Test]
    public async Task CreateNewUserAsync_ShouldAddAndReturnUser()
    {
        var dto = new UserDTO { Id = 1 };
        var device = new DeviceInformationDTO();
        var result = await _repository.CreateNewUserAsync(dto, device, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(dto.Id));
    }

    [Test]
    public async Task UpdateUserAsync_ShouldUpdateAndReturnUser()
    {
        var dto = new UserDTO { Id = 1 };
        var device = new DeviceInformationDTO();
        await _repository.CreateNewUserAsync(dto, device, _dbContext);
        var result = await _repository.UpdateUserAsync(dto, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(dto.Id));
    }

    [Test]
    public async Task GetAllApplicationListAsync_ShouldReturnApplications()
    {
        Expression<Func<Application, bool>> filter = x => true;
        var result = await _repository.GetAllApplicationListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<ApplicationDTO>>());
    }

    [Test]
    public async Task GetApplicationListAsync_ShouldReturnApplications()
    {
        Expression<Func<CompanyMembership, bool>> filter = x => true;
        var result = await _repository.GetApplicationListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<ApplicationDTO>>());
    }

    [Test]
    public async Task GetCompanyMemberhipListAsync_ShouldReturnCompanyMemberships()
    {
        Expression<Func<CompanyMembership, bool>> filter = x => true;
        var result = await _repository.GetCompanyMemberhipListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<CompanyMembershipsDTO>>());
    }

    [Test]
    public async Task GetAllCompanyListAsync_ShouldReturnCompanies()
    {
        Expression<Func<Company, bool>> filter = x => true;
        var result = await _repository.GetAllCompanyListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<CompanyDTO>>());
    }

    [Test]
    public async Task GetRoleListAsync_ShouldReturnRoles()
    {
        Expression<Func<Role, bool>> filter = x => true;
        var result = await _repository.GetRoleListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<RoleDTO>>());
    }

    [Test]
    public async Task GetRolePermissionListAsync_ShouldReturnRoleDTO()
    {
        Expression<Func<Role, bool>> filter = x => true;
        var result = await _repository.GetRolePermissionListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<RoleDTO>());
    }

    [Test]
    public async Task GetModuleListAsync_ShouldReturnModules()
    {
        Expression<Func<Module, bool>> filter = x => true;
        var result = await _repository.GetModuleListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<ModuleDTO>>());
    }

    [Test]
    public async Task GetEndpointsListAsync_ShouldReturnEndpoints()
    {
        Expression<Func<Endpoint, bool>> filter = x => true;
        var result = await _repository.GetEndpointsListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<EndpointDTO>>());
    }

    [Test]
    public async Task GetEndpointsListByRoleIdAsync_ShouldReturnEndpoints()
    {
        var result = await _repository.GetEndpointsListByRoleIdAsync(1, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<EndpointDTO>>());
    }

    [Test]
    public async Task GetComponentListAsync_ShouldReturnComponents()
    {
        Expression<Func<Component, bool>> filter = x => true;
        var result = await _repository.GetComponentListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<ComponentDTO>>());
    }

    [Test]
    public async Task GetUserRolesListAsync_ShouldReturnUserRoles()
    {
        Expression<Func<UserRole, bool>> filter = x => true;
        var result = await _repository.GetUserRolesListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<UserRoleDTO>>());
    }

    [Test]
    public async Task GetUserPermissionListAsync_ShouldReturnUserDTO()
    {
        Expression<Func<User, bool>> filter = x => true;
        var result = await _repository.GetUserPermissionListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<UserDTO>());
    }

    [Test]
    public async Task GetUserListAsync_ShouldReturnUsers()
    {
        Expression<Func<User, bool>> filter = x => true;
        var result = await _repository.GetUserListAsync(filter, _dbContext);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<List<UserDTO>>());
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

}