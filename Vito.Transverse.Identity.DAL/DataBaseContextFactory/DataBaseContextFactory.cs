using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.Options;

namespace Vito.Transverse.Identity.DAL.DataBaseContextFactory;

public class DataBaseContextFactory(IOptions<DataBaseSettingsOptions> _dataBaseSettingsOptions) : IDataBaseContextFactory
{
    DataBaseSettingsOptions _dataBaseSettingsOptionsValue => _dataBaseSettingsOptions.Value;

    public DataBaseNameEnum DefaultDatabaseName()
    {
        return DataBaseNameEnum.TransverseDB;
    }

    public DataBaseServiceContext CreateDbContext()
    {
        return CreateDbContext(DefaultDatabaseName());
    }

    public DataBaseServiceContext CreateDbContext(DataBaseNameEnum dataBaseEnum)
    {
        DataBaseServiceContext context;
        var sqlDbConnectionName = dataBaseEnum.ToString();
        var transverseDBConnectionString = _dataBaseSettingsOptionsValue!.ConnectionStrings!.First(x => x.ConnectionName.Equals(sqlDbConnectionName));
        switch (transverseDBConnectionString.ConnectionType)
        {
            case Framework.Common.Enums.ConnectionStringTypeEnum.SQLServer:
                var builder = new DbContextOptionsBuilder<DataBaseServiceContext>();
                builder.UseSqlServer(transverseDBConnectionString.FullConnectionString, delegate (SqlServerDbContextOptionsBuilder sqlOptions)
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


    public DataBaseServiceContext GetDbContext(DataBaseServiceContext? context = null, DataBaseNameEnum dataBaseEnum = DataBaseNameEnum.TransverseDB)
    {
        if (context is null)// || context.Database.CurrentTransaction is null)
        {
            context = CreateDbContext();
        }
        return context;
    }

    public void DisposeDbContext(DataBaseServiceContext? context)
    {
        if (context?.Database?.CurrentTransaction is not null)
        {
            context.Database.CurrentTransaction.Dispose();
        }
        context!.Dispose();
    }


}