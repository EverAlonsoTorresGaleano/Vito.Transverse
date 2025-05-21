using Microsoft.EntityFrameworkCore;
using Vito.Transverse.Identity.Domain.Models;

namespace Vito.Transverse.Identity.DAL.DataBaseContext;

/// <summary>
/// Database reposiories Set.
/// </summary>
public partial interface IDataBaseServiceContext : IDisposable
{
    DbSet<ActivityLog> ActivityLogs { get; set; }

    DbSet<Application> Applications { get; set; }

    DbSet<ApplicationLicenseType> ApplicationLicenseTypes { get; set; }

    DbSet<ApplicationOwner> ApplicationOwners { get; set; }

    DbSet<AuditEntity> AuditEntities { get; set; }

    DbSet<AuditRecord> AuditRecords { get; set; }

    DbSet<Company> Companies { get; set; }

    DbSet<CompanyEntityAudit> CompanyEntityAudits { get; set; }

    DbSet<CompanyMembership> CompanyMemberships { get; set; }

    DbSet<CompanyMembershipPermission> CompanyMembershipPermissions { get; set; }

    DbSet<Component> Components { get; set; }

    DbSet<Country> Countries { get; set; }

    DbSet<Culture> Cultures { get; set; }

    DbSet<CultureTranslation> CultureTranslations { get; set; }

    DbSet<Endpoint> Endpoints { get; set; }

    DbSet<GeneralTypeGroup> GeneralTypeGroups { get; set; }

    DbSet<GeneralTypeItem> GeneralTypeItems { get; set; }

    DbSet<Language> Languages { get; set; }

    DbSet<MembershipType> MembershipTypes { get; set; }

    DbSet<MembersipPriceHistory> MembersipPriceHistories { get; set; }

    DbSet<Module> Modules { get; set; }

    DbSet<Notification> Notifications { get; set; }

    DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    DbSet<Role> Roles { get; set; }

    DbSet<RolePermission> RolePermissions { get; set; }

    DbSet<Sequence> Sequences { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<UserRole> UserRoles { get; set; }

    DbSet<VwCompanyUserRole> VwCompanyUserRoles { get; set; }

    DbSet<VwGetAllCompanyPermission> VwGetAllCompanyPermissions { get; set; }

    DbSet<VwGetAuditRecord> VwGetAuditRecords { get; set; }

    DbSet<VwGetCompanyMembership> VwGetCompanyMemberships { get; set; }

    DbSet<VwGetDatabaseTable> VwGetDatabaseTables { get; set; }

    DbSet<VwGetGeneralTypeItemWithGroup> VwGetGeneralTypeItemWithGroups { get; set; }

    DbSet<VwGetRolePermission> VwGetRolePermissions { get; set; }

}
