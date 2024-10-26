using Microsoft.EntityFrameworkCore.Storage;

namespace Vito.Transverse.Identity.DAL.DataBaseContext;

/// <summary>
/// Database repositories Custom Methods
/// </summary>
public partial interface IDataBaseServiceContext : IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IDbContextTransaction BeginTransaction();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task BulkInsertRecordsAndRetrieveIdsAsync<T>(ICollection<T> entities) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task BulkInsertRecordsAsync<T>(ICollection<T> entities) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task BulkUpsertRecordsAsync<T>(ICollection<T> entities) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="sqlStatement"></param>
    /// <returns></returns>
    IQueryable<TEntity> RunSqlInterpolated<TEntity>(FormattableString sqlStatement) where TEntity : class;

}
