using Microsoft.EntityFrameworkCore;
using Vito.Transverse.Identity.DAL.DataBaseContext;

namespace Vito.Transverse.Identity.DAL.DataBaseContextFactory;

public interface IDataBaseContextFactory : IDbContextFactory<DataBaseServiceContext>
{
    DataBaseServiceContext GetDbContext(DataBaseServiceContext? transactionContext = null);

    void DisposeDbContext(DataBaseServiceContext? context, DataBaseServiceContext? transactionContext);
}

