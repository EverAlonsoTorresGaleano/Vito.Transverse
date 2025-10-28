using Microsoft.EntityFrameworkCore;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Entities.Enums;

namespace  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;

public interface IDataBaseContextFactory : IDbContextFactory<DataBaseServiceContext>
{
    new DataBaseServiceContext CreateDbContext();

    DataBaseNameEnum DefaultDatabaseName();

    DataBaseServiceContext CreateDbContext(DataBaseNameEnum dataBaseEnum);

    DataBaseServiceContext GetDbContext(DataBaseServiceContext? context = null, DataBaseNameEnum dataBaseEnum = DataBaseNameEnum.TransverseDB);

    void DisposeDbContext(DataBaseServiceContext? context);
}

