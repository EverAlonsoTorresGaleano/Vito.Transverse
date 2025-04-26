using Microsoft.EntityFrameworkCore;
using Vito.Transverse.Identity.DAL.DataBaseContext;

namespace Vito.Transverse.Identity.DAL.DataBaseContextFactory;

public interface IDataBaseContextFactory : IDbContextFactory<DataBaseServiceContext>
{
    new DataBaseServiceContext CreateDbContext();

    DataBaseServiceContext GetDbContext(DataBaseServiceContext? context = null);

    void DisposeDbContext(DataBaseServiceContext? context);
}

