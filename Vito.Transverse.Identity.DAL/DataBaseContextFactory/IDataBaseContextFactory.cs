using Microsoft.EntityFrameworkCore;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Enums;

namespace Vito.Transverse.Identity.DAL.DataBaseContextFactory;

public interface IDataBaseContextFactory : IDbContextFactory<DataBaseServiceContext>
{
    new DataBaseServiceContext CreateDbContext();

    DataBaseNameEnum DefaultDatabaseId();

    DataBaseServiceContext CreateDbContext(DataBaseNameEnum dataBaseEnum);

    DataBaseServiceContext GetDbContext(DataBaseServiceContext? context = null, DataBaseNameEnum dataBaseEnum = DataBaseNameEnum.TransverseDB);

    void DisposeDbContext(DataBaseServiceContext? context);
}

