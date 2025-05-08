using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Models;

namespace Vito.Transverse.Identity.DAL.TransverseServices.Audit;

public interface IAuditRepository
{

    Task<AuditRecord> NewRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, DataBaseServiceContext? context = null);

    Task<AuditRecord> UpdateRowAuditAsync(long companyId, long userId, object oldEntity, object newEntity, string entityIndex, DeviceInformationDTO devideInformation, DataBaseServiceContext? context = null);

    Task<AuditRecord> DeleteRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, DataBaseServiceContext? context = null);

    Task<List<AuditRecord>> GetAuditRecordListAsync(Expression<Func<AuditRecord, bool>> filters, DataBaseServiceContext? context = null);

    Task<CompanyEntityAudit> GetCompanyEntityAuditAsync(long companyId, string entityName, EntityAuditTypeEnum auditType, DataBaseServiceContext? context = null);
}
