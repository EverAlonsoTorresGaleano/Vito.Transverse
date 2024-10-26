using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Options;

namespace Vito.Transverse.Identity.DAL.DataBaseContextFactory;

public class DataBaseContextFactory(IOptions<DataBaseSettingsOptions> _dataBaseSettingsOptions) : IDataBaseContextFactory
{
    DataBaseSettingsOptions _dataBaseSettingsOptionsValue => _dataBaseSettingsOptions.Value;

    public DataBaseServiceContext CreateDbContext()
    {
        DataBaseServiceContext context;
        var sqlDbConnectionName = "TransverseDB";
        var transverseDBConnectionString = _dataBaseSettingsOptionsValue!.ConnectionStrings!.First(x => x.ConnectionName.Equals(sqlDbConnectionName));
        switch (transverseDBConnectionString.ConnectionType)
        {
            case Framework.Common.Enums.ConnectionStringTypeEnum.SQLServer:
                var builder = new DbContextOptionsBuilder<DataBaseServiceContext>();
                builder.UseSqlServer(transverseDBConnectionString.ConnectionString, delegate (SqlServerDbContextOptionsBuilder sqlOptions)
                {
                    sqlOptions.EnableRetryOnFailure(transverseDBConnectionString.RetryCount, TimeSpan.FromSeconds(transverseDBConnectionString.MaxRetryDelay), null);
                    sqlOptions.CommandTimeout(transverseDBConnectionString.TimeOut);
                });
                context = new DataBaseServiceContext(builder.Options);

                break;
            default:
                context = new DataBaseServiceContext();
                break;

        }
        return context;

    }

    public void DisposeDbContext(DataBaseServiceContext? context, DataBaseServiceContext? transactionContext)
    {
        if (transactionContext is null)
        {
            context!.Dispose();
        }
    }

    public DataBaseServiceContext GetDbContext(DataBaseServiceContext? transactionContext = null)
    {
        if (transactionContext is null)
        {
            transactionContext = CreateDbContext();
        }
        return transactionContext;
    }
}