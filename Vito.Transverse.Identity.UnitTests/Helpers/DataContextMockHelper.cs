using Bogus;
using Moq;
using Moq.EntityFrameworkCore;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Entities.Enums;
using  Vito.Transverse.Identity.Infrastructure.Models;

namespace Vito.Transverse.Identity.UnitTests.Helpers;

public static class DataContextMockHelper
{
    private static Mock<IDataBaseContextFactory> dataBaseContextFactory;
    private static Mock<IDataBaseServiceContext> dataBaseServiceContext;


    public static IDataBaseContextFactory GetDataBaseContextFactory()
    {
        if (dataBaseContextFactory == null)
        {
            dataBaseContextFactory = new Mock<IDataBaseContextFactory>();
            dataBaseContextFactory.Setup(x => x.CreateDbContext(It.IsAny<DataBaseNameEnum>())).Returns(GetDataBaseServiceContext());
            dataBaseContextFactory.Setup(x => x.GetDbContext(It.IsAny<DataBaseServiceContext>(), It.IsAny<DataBaseNameEnum>())).Returns(GetDataBaseServiceContext());
            dataBaseContextFactory.Setup(x => x.DefaultDatabaseName()).Returns(DataBaseNameEnum.TransverseDB);
        }
        return dataBaseContextFactory.Object;
    }

    public static DataBaseServiceContext GetDataBaseServiceContext()
    {
        if (dataBaseServiceContext == null)
        {
            dataBaseServiceContext = new Mock<IDataBaseServiceContext>();
            dataBaseServiceContext.Setup(x => x.ActivityLogs).ReturnsDbSet(GetActivityLogTableMock());
        }
        return (DataBaseServiceContext)dataBaseServiceContext.Object;
    }

    private static List<ActivityLog> GetActivityLogTableMock()
    {
        var returnList = new List<ActivityLog>();
        var fakeListMaker = new Faker<ActivityLog>()
            .RuleFor(o => o.TraceId, f => f.IndexFaker + 1)
            .RuleFor(o => o.EndPointUrl, f => f.Lorem.Word())
            .RuleFor(o => o.ActionTypeFk, f => (long)f.PickRandom<GeneralTypeItem>().Id)
            .RuleFor(o => o.EventDate, f => f.Date.Past())
            .RuleFor(o => o.UserFk, f => f.Random.Long(1, 100))
            .RuleFor(o => o.CompanyFk, f => f.Random.Long(1, 10))
            .RuleFor(o => o.DeviceName, f => f.Lorem.Sentence());

        returnList = fakeListMaker.Generate(50);
        return returnList;

    }

}
