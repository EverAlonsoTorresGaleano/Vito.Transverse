using Microsoft.EntityFrameworkCore;
using  Vito.Transverse.Identity.Infrastructure.Models;


namespace  Vito.Transverse.Identity.Infrastructure.DataBaseContext;

/// <see cref="IDataBaseServiceContext"/>
public partial class DataBaseServiceContext : DbContext, IDataBaseServiceContext
{
    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationLicenseType> ApplicationLicenseTypes { get; set; }

    public virtual DbSet<AuditRecord> AuditRecords { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyEntityAudit> CompanyEntityAudits { get; set; }

    public virtual DbSet<CompanyMembership> CompanyMemberships { get; set; }

    public virtual DbSet<CompanyMembershipPermission> CompanyMembershipPermissions { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Culture> Cultures { get; set; }

    public virtual DbSet<CultureTranslation> CultureTranslations { get; set; }

    public virtual DbSet<Endpoint> Endpoints { get; set; }

    public virtual DbSet<Entity> Entities { get; set; }

    public virtual DbSet<GeneralTypeGroup> GeneralTypeGroups { get; set; }

    public virtual DbSet<GeneralTypeItem> GeneralTypeItems { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MembershipType> MembershipTypes { get; set; }

    public virtual DbSet<MembersipPriceHistory> MembersipPriceHistories { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Sequence> Sequences { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<VwCompanyUserRole> VwCompanyUserRoles { get; set; }

    public virtual DbSet<VwGetAllCompanyPermission> VwGetAllCompanyPermissions { get; set; }

    public virtual DbSet<VwGetAuditRecord> VwGetAuditRecords { get; set; }

    public virtual DbSet<VwGetCompanyMembership> VwGetCompanyMemberships { get; set; }

    public virtual DbSet<VwGetDatabaseTable> VwGetDatabaseTables { get; set; }

    public virtual DbSet<VwGetGeneralTypeItemWithGroup> VwGetGeneralTypeItemWithGroups { get; set; }

    public virtual DbSet<VwGetRolePermission> VwGetRolePermissions { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.TraceId).HasName("PK_UserTraces");

            entity.HasOne(d => d.ActionTypeFkNavigation).WithMany(p => p.ActivityLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_ListItems");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.ActivityLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_Users");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Applications_1");

            entity.Property(e => e.ApplicationClient).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApplicationSecret).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ApplicationLicenseTypeFkNavigation).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_ApplicationLicenseTypes");

            entity.HasOne(d => d.OwnerFkNavigation).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Companies");
        });

        modelBuilder.Entity<ApplicationLicenseType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AuditRecord>(entity =>
        {
            entity.HasOne(d => d.AuditTypeFkNavigation).WithMany(p => p.AuditRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_GeneralTypeItems");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.AuditRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_Companies");

            entity.HasOne(d => d.EntityFkNavigation).WithMany(p => p.AuditRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_Entities");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.AuditRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_Users");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.CompanyClient).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CompanySecret).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.CountryFkNavigation).WithMany(p => p.Companies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_Countries");

            entity.HasOne(d => d.DefaultCultureFkNavigation).WithMany(p => p.Companies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_Cultures");
        });

        modelBuilder.Entity<CompanyEntityAudit>(entity =>
        {
            entity.HasOne(d => d.AuditTypeFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_GeneralTypeItems");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_Companies");

            entity.HasOne(d => d.EntityFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_Entities");
        });

        modelBuilder.Entity<CompanyMembership>(entity =>
        {
            entity.HasKey(e => new { e.CompanyFk, e.ApplicationFk }).HasName("PK_CompanyApplications");

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.CompanyMemberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMemberships_Applications");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.CompanyMemberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMemberships_Companies");

            entity.HasOne(d => d.MembershipTypeFkNavigation).WithMany(p => p.CompanyMemberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMemberships_Memberships");
        });

        modelBuilder.Entity<CompanyMembershipPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CompanyMembershipPermissions_1");

            entity.HasOne(d => d.CompanyMembershipFkNavigation).WithMany(p => p.CompanyMembershipPermissions)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.CompanyMembershipFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMembershipPermissions_CompanyMemberships");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EndpointComponents");

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Components)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Components_Applications");

            entity.HasOne(d => d.EndpointFkNavigation).WithMany(p => p.Components)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EndpointComponents_ModuleEndpoints");
        });

        modelBuilder.Entity<Culture>(entity =>
        {
            entity.HasOne(d => d.CountryFkNavigation).WithMany(p => p.Cultures)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cultures_Countries");

            entity.HasOne(d => d.LanguageFkNavigation).WithMany(p => p.Cultures)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cultures_Languages");
        });

        modelBuilder.Entity<CultureTranslation>(entity =>
        {
            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.CultureTranslations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Applications");

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.CultureTranslations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Cultures");
        });

        modelBuilder.Entity<Endpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EndpointsModules");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Endpoints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Endpoints_Applications");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.Endpoints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleEndpoint_Modules");
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AuditEntities");
        });

        modelBuilder.Entity<GeneralTypeGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListItemGroup");

            entity.Property(e => e.IsSystemType).HasDefaultValue(true);
        });

        modelBuilder.Entity<GeneralTypeItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListItems");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);

            entity.HasOne(d => d.ListItemGroupFkNavigation).WithMany(p => p.GeneralTypeItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListItems_ListItemGroup");
        });

        modelBuilder.Entity<MembershipType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Memberships");
        });

        modelBuilder.Entity<MembersipPriceHistory>(entity =>
        {
            entity.HasOne(d => d.MembershipTypeFkNavigation).WithMany(p => p.MembersipPriceHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MembersipPriceHistory_Memberships");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Modules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modules_Applications");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationTraces");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Companies");

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationTraces_Cultures");

            entity.HasOne(d => d.NotificationTypeFkNavigation).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_ListItems");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_NotificationTemplates");
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.NotificationTemplates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationTemplates_Cultures");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Pictures)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_Companies");

            entity.HasOne(d => d.EntityFkNavigation).WithMany(p => p.Pictures).HasConstraintName("FK_Pictures_Entities");

            entity.HasOne(d => d.FileTypeFkNavigation).WithMany(p => p.PictureFileTypeFkNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_GeneralTypeItems");

            entity.HasOne(d => d.PictureCategoryFkNavigation).WithMany(p => p.PicturePictureCategoryFkNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_GeneralTypeItems1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Roles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Applications");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Roles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Companies");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserRolePermissions");

            entity.HasOne(d => d.ComponentFkNavigation).WithMany(p => p.RolePermissions).HasConstraintName("FK_UserRolePermissions_Components");

            entity.HasOne(d => d.EndpointFkNavigation).WithMany(p => p.RolePermissions).HasConstraintName("FK_UserRolePermissions_Endpoints");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissions_Modules");

            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Roles");
        });

        modelBuilder.Entity<Sequence>(entity =>
        {
            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Sequences).HasConstraintName("FK_Sequences_Applications");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Sequences).HasConstraintName("FK_Sequences_Companies");

            entity.HasOne(d => d.SequenceTypeFkNavigation).WithMany(p => p.Sequences)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sequences_GeneralTypeItems");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.IsLocked).HasDefaultValue(true);
            entity.Property(e => e.RequirePasswordChange).HasDefaultValue(true);

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Companies");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<VwCompanyUserRole>(entity =>
        {
            entity.ToView("Vw_CompanyUserRoles");
        });

        modelBuilder.Entity<VwGetAllCompanyPermission>(entity =>
        {
            entity.ToView("Vw_GetAllCompanyPermissions");
        });

        modelBuilder.Entity<VwGetAuditRecord>(entity =>
        {
            entity.ToView("Vw_GetAuditRecords");
        });

        modelBuilder.Entity<VwGetCompanyMembership>(entity =>
        {
            entity.ToView("Vw_GetCompanyMemberships");
        });

        modelBuilder.Entity<VwGetDatabaseTable>(entity =>
        {
            entity.ToView("Vw_GetDatabaseTables");
        });

        modelBuilder.Entity<VwGetGeneralTypeItemWithGroup>(entity =>
        {
            entity.ToView("Vw_GetGeneralTypeItemWithGroups");
        });

        modelBuilder.Entity<VwGetRolePermission>(entity =>
        {
            entity.ToView("Vw_GetRolePermissions");
        });

        OnModelCreatingPartial(modelBuilder);
    }


}
