using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Models;

namespace Vito.Transverse.Identity.DAL.TransverseServices.Audit;

public class AuditRepository(IDataBaseContextFactory _dataBaseContextFactory, ICultureRepository cultureRepository) : IAuditRepository
{
    public async Task<AuditRecord> DeleteRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, DataBaseServiceContext? context = null)
    {
        AuditRecord? auditInfo = null;
        context = _dataBaseContextFactory.GetDbContext();

        var entityName = entity.GetType().Name;
        var auditType = EntityAuditTypeEnum.EntityAuditType_DeleteRow;
        var companyauditEntityInfo = await GetCompanyEntityAuditAsync(companyId, entityName, auditType, context);
        if (companyauditEntityInfo is not null)
        {
            var auditInformation = GetChangesInformationSummary(entity);
            auditInfo = CreateNewAuditRecord(companyId, userId, auditType, companyauditEntityInfo, entityIndex, auditInformation, devideInformation);
            context.AuditRecords.Add(auditInfo);
            context.SaveChanges();
        }
        return auditInfo;
    }

    public async Task<AuditRecord> NewRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, DataBaseServiceContext? context = null)
    {
        AuditRecord? auditInfo = null;
        context = _dataBaseContextFactory.GetDbContext();

        var entityName = entity.GetType().Name;
        var auditType = EntityAuditTypeEnum.EntityAuditType_AddRow;
        var companyauditEntityInfo = await GetCompanyEntityAuditAsync(companyId, entityName, auditType, context);
        if (companyauditEntityInfo is not null)
        {
            var auditInformation = GetChangesInformationSummary(entity);
            auditInfo = CreateNewAuditRecord(companyId, userId, auditType, companyauditEntityInfo, entityIndex, auditInformation, devideInformation);
            context.AuditRecords.Add(auditInfo);
            context.SaveChanges();
        }
        return auditInfo;
    }

    public async Task<AuditRecord> UpdateRowAuditAsync(long companyId, long userId, object oldEntity, object newEntity, string entityIndex, DeviceInformationDTO devideInformation, DataBaseServiceContext? context = null)
    {
        AuditRecord? auditInfo = null;
        context = _dataBaseContextFactory.GetDbContext();
        var entityName = oldEntity.GetType().Name;
        var auditType = EntityAuditTypeEnum.EntityAuditType_UpdateRow;
        var companyauditEntityInfo = await GetCompanyEntityAuditAsync(companyId, entityName, auditType, context);
        if (companyauditEntityInfo is not null)
        {
            var auditInformation = GetChangesInformationSummary(oldEntity, newEntity);
            auditInfo = CreateNewAuditRecord(companyId, userId, auditType, companyauditEntityInfo, entityIndex, auditInformation, devideInformation);
            context.AuditRecords.Add(auditInfo);
            context.SaveChanges();
        }
        return auditInfo;
    }

    public async Task<List<AuditRecord>> GetAuditRecordListAsync(Expression<Func<AuditRecord, bool>> filters, DataBaseServiceContext context = null)
    {
        context = _dataBaseContextFactory.GetDbContext();
        var returnList = await context.AuditRecords.Where(filters).ToListAsync();
        return returnList;
    }


    public async Task<CompanyEntityAudit> GetCompanyEntityAuditAsync(long companyId, string entityName, EntityAuditTypeEnum auditType, DataBaseServiceContext? context = null)
    {
        context = _dataBaseContextFactory.GetDbContext();
        var auditEntity = await context.CompanyEntityAudits.FirstOrDefaultAsync(x => x.CompanyFk == companyId
                        && x.AuditTypeFk == (long)auditType
                        && x.AuditEntityFkNavigation.EntityName.Equals($"{entityName}{"s"}")
                        && x.AuditEntityFkNavigation.IsActive == true);
        return auditEntity;
    }

    private AuditRecord? CreateNewAuditRecord(long companyId, long userId, EntityAuditTypeEnum auditType, CompanyEntityAudit companyEntity, string entityIndex, string auditInformation, DeviceInformationDTO devideInformation)
    {
        var cultureId = cultureRepository.GetCurrentCultureId();
        AuditRecord newRecord = new()
        {
            CreationDate = cultureRepository.UtcNow().DateTime,
            CompanyFk = companyId,
            UserFk = userId,
            AuditTypeFk = (long)auditType,
            AuditEntityFk = companyEntity.AuditEntityFk,
            AuditEntityIndex = entityIndex,
            CultureFk = cultureId,
            AuditInfoJson = auditInformation,
            Browser = devideInformation.Browser,
            DeviceType = devideInformation.DeviceType,
            Engine = devideInformation.Engine,
            HostName = devideInformation.Name,
            IpAddress = devideInformation.IpAddress,
            Platform = devideInformation.Platform,
        };
        return newRecord;
    }

    private string GetChangesInformationSummary(object oldEntity, object? newEntity = null)
    {
        List<PropertyInfo> propertyList = oldEntity.GetType().GetProperties().ToList();
        List<KeyValuePair<string, string>> changeList = new();

        object? oldPropertyValue = null;
        object? newPropertyValue = null;
        propertyList.ForEach(itemProperty =>
        {
            if (!itemProperty.Name.Contains("Navigation") && itemProperty.PropertyType.FullName.Contains("System"))
            {
                oldPropertyValue = GetPropertyValue(oldEntity, itemProperty.Name);
                if (newEntity is not null)
                {
                    newPropertyValue = newEntity is null ? null : GetPropertyValue(newEntity, itemProperty.Name);
                    if (!string.IsNullOrEmpty(oldPropertyValue?.ToString()) && !oldPropertyValue.ToString().Equals(newPropertyValue.ToString(), StringComparison.Ordinal))
                    {
                        changeList.Add(new(itemProperty.Name, $"Before= {oldPropertyValue} | After={newPropertyValue}"));
                    }
                }
                else //Single Entity
                {
                    if (!string.IsNullOrEmpty(oldPropertyValue?.ToString()))
                    {
                        changeList.Add(new(itemProperty.Name, oldPropertyValue.ToString()));
                    }
                }
            }
        });

        var returnJson = changeList.Serialize();
        return returnJson;
    }

    public object GetPropertyValue(object Entidad, string PropertyName)
    {
        PropertyInfo infoColumna = Entidad.GetType().GetProperty(PropertyName);
        return infoColumna.GetValue(Entidad, null);
    }
}
