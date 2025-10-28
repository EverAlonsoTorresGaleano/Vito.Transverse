using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Vito.Framework.Common.Constants;

namespace  Vito.Transverse.Identity.Infrastructure.DataBaseContext;

/// <see cref="IDataBaseServiceContext"/>
public partial class DataBaseServiceContext : DbContext, IDataBaseServiceContext
{
    #region Public Methods 
    //private readonly IUserIdentity userIdentity;

    public DataBaseServiceContext()
    {
    }

    public DataBaseServiceContext(DbContextOptions<DataBaseServiceContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //FOR AOT
        //optionsBuilder.UseModel(VitoTransverseContextModel.Instance);
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception(FrameworkConstants.SQL_CONNECTION_STRING_EMPTY);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    public virtual IDbContextTransaction BeginTransaction()
    {
        return Database.BeginTransaction();
    }

  

    /// <see cref="IDataBaseServiceContext.BulkInsertRecordsAndRetrieveIdsAsync{T}(ICollection{T})"/>
    public async Task BulkInsertRecordsAndRetrieveIdsAsync<T>(ICollection<T> entities) where T : class
    {
        var nonNullEntities = entities.Where(e => e != null).ToList();
        //AddTrackingForBulkOperations(nonNullEntities, true);
        await this.BulkInsertAsync(nonNullEntities, GetBulkConfigRetrieveIds());
    }

    /// <see cref="IDataBaseServiceContext.BulkInsertRecordsAsync{T}(ICollection{T})"/>
    public async Task BulkInsertRecordsAsync<T>(ICollection<T> entities) where T : class
    {
        var nonNullEntities = entities.Where(e => e != null).ToList();
        //AddTrackingForBulkOperations(nonNullEntities, true);
        await this.BulkInsertAsync(nonNullEntities, GetBulkConfig());
    }

    /// <see cref="IDataBaseServiceContext.BulkUpsertRecordsAsync{T}(ICollection{T})"/>
    public async Task BulkUpsertRecordsAsync<T>(ICollection<T> entities) where T : class
    {
        var nonNullEntities = entities.Where(e => e != null).ToList();
        //AddTrackingForBulkOperations(nonNullEntities);
        await this.BulkInsertOrUpdateAsync(nonNullEntities, GetBulkConfig());
    }

    /// <see cref="IDataBaseServiceContext.RunSqlInterpolated{TEntity}(FormattableString)"/>
    public virtual IQueryable<TEntity> RunSqlInterpolated<TEntity>(FormattableString sqlStatement) where TEntity : class
    {
        return this.Set<TEntity>().FromSqlInterpolated(sqlStatement);
    }

#endregion

    #region Private Methods
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static BulkConfig GetBulkConfigRetrieveIds()
    {
        return new BulkConfig { SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints, SetOutputIdentity = true };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static BulkConfig GetBulkConfig()
    {
        return new BulkConfig
        {
            SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints,

            //Shadow columns used for Temporal table. Has defaults elements: 'PeriodStart' and 'PeriodEnd'.
            //We're using ValidFrom/ValidTo per SQL Server conventions
            TemporalColumns = new List<string>() { "PeriodStart", "PeriodEnd", "ValidFrom", "ValidTo", },
        };
    }

    //private void AddTrackingForBulkOperations<T>(IEnumerable<T> entities, bool alwaysSetCreateInformation = false) where T : class
    //{
    //    var now = clock.Now();

    //    var trackableEntities = entities.Select(e => e as ITracking)
    //        .Where(e => e != null)
    //        .ToList();

    //    foreach (var trackableEntity in trackableEntities)
    //    {
    //        if (trackableEntity == null)
    //        {
    //            continue;
    //        }

    //        bool shouldUpdateCreateInformation = alwaysSetCreateInformation || trackableEntity.CreatedDate == default || trackableEntity.CreatedByUserId == default;
    //        if (shouldUpdateCreateInformation)
    //        {
    //            trackableEntity.CreatedDate = now;
    //            trackableEntity.CreatedByUserId = userIdentity.Id;
    //        }

    //        trackableEntity.LastUpdatedDate = now;
    //        trackableEntity.LastUpdatedByUserId = userIdentity.Id;
    //    }
    //}

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //Model forSp return with create tables
        //modelBuilder.Entity<SP1Entity>().ToView(null);
    }
    #endregion

}
