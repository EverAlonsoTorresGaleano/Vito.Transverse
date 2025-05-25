using Microsoft.EntityFrameworkCore;
using Vito.Transverse.Identity.Domain.Models;


namespace Vito.Transverse.Identity.DAL.DataBaseContext;

/// <see cref="IDataBaseServiceContext"/>
public partial class DataBaseServiceContext : DbContext, IDataBaseServiceContext
{
    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationLicenseType> ApplicationLicenseTypes { get; set; }

    public virtual DbSet<ApplicationOwner> ApplicationOwners { get; set; }

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

            entity.Property(e => e.Browser).HasMaxLength(50);
            entity.Property(e => e.CultureId).HasMaxLength(50);
            entity.Property(e => e.DeviceName).HasMaxLength(50);
            entity.Property(e => e.DeviceType).HasMaxLength(50);
            entity.Property(e => e.EndPointUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Engine).HasMaxLength(50);
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Platform).HasMaxLength(50);
            entity.Property(e => e.QueryString)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Referer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ActionTypeFkNavigation).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ActionTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_ListItems");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_Users");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Applications_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApplicationClient).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApplicationSecret).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ApplicationLicenseType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ApplicationOwner>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.ApplicationOwners)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationOwners_Applications");

            entity.HasOne(d => d.ApplicationLicenseTypeFkNavigation).WithMany(p => p.ApplicationOwners)
                .HasForeignKey(d => d.ApplicationLicenseTypeFk)
                .HasConstraintName("FK_ApplicationOwners_ApplicationLicenseTypes");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.ApplicationOwners)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationOwners_Companies");
        });

        modelBuilder.Entity<AuditRecord>(entity =>
        {
            entity.Property(e => e.AuditChanges).HasColumnType("text");
            entity.Property(e => e.AuditEntityIndex)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.Browser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CultureFk)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeviceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EndPointUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Engine)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HostName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.QueryString)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Referer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserAgent)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.AuditTypeFkNavigation).WithMany(p => p.AuditRecords)
                .HasForeignKey(d => d.AuditTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_GeneralTypeItems");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.AuditRecords)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_Companies");

            entity.HasOne(d => d.EntityFkNavigation).WithMany(p => p.AuditRecords)
                .HasForeignKey(d => d.EntityFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_Entities");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.AuditRecords)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_Users");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CompanyClient).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CompanySecret).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CountryFk)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DefaultCultureFk)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.Subdomain)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.CountryFkNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CountryFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_Countries");

            entity.HasOne(d => d.DefaultCultureFkNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.DefaultCultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_Cultures");
        });

        modelBuilder.Entity<CompanyEntityAudit>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.AuditTypeFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .HasForeignKey(d => d.AuditTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_GeneralTypeItems");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_Companies");

            entity.HasOne(d => d.EntityFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .HasForeignKey(d => d.EntityFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_Entities");
        });

        modelBuilder.Entity<CompanyMembership>(entity =>
        {
            entity.HasKey(e => new { e.CompanyFk, e.ApplicationFk }).HasName("PK_CompanyApplications");

            entity.HasIndex(e => e.Id, "IX_CompanyMemberships").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.CompanyMemberships)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMemberships_Applications");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.CompanyMemberships)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMemberships_Companies");

            entity.HasOne(d => d.MembershipTypeFkNavigation).WithMany(p => p.CompanyMemberships)
                .HasForeignKey(d => d.MembershipTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMemberships_Memberships");
        });

        modelBuilder.Entity<CompanyMembershipPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CompanyMembershipPermissions_1");

            entity.HasIndex(e => e.CompanyMembershipFk, "IX_CompanyMembershipPermissions");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PropertyValue)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.CompanyMembershipFkNavigation).WithMany(p => p.CompanyMembershipPermissions)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.CompanyMembershipFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMembershipPermissions_CompanyMemberships");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EndpointComponents");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DefaultPropertyValue)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.ObjectId)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ObjectName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ObjectPropertyName)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Components_Applications");

            entity.HasOne(d => d.EndpointFkNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.EndpointFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EndpointComponents_ModuleEndpoints");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Culture>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CountryFk)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.LanguageFk)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);

            entity.HasOne(d => d.CountryFkNavigation).WithMany(p => p.Cultures)
                .HasForeignKey(d => d.CountryFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cultures_Countries");

            entity.HasOne(d => d.LanguageFkNavigation).WithMany(p => p.Cultures)
                .HasForeignKey(d => d.LanguageFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cultures_Languages");
        });

        modelBuilder.Entity<CultureTranslation>(entity =>
        {
            entity.HasKey(e => new { e.ApplicationFk, e.CultureFk, e.TranslationKey });

            entity.HasIndex(e => e.ApplicationFk, "IX_CultureTranslations");

            entity.Property(e => e.CultureFk)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.TranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.TranslationValue)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.CultureTranslations)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Applications");

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.CultureTranslations)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Cultures");
        });

        modelBuilder.Entity<Endpoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EndpointsModules");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndpointUrl)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Endpoints)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Endpoints_Applications");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.Endpoints)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleEndpoint_Modules");
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AuditEntities");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EntityName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.SchemaName)
                .HasMaxLength(75)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeneralTypeGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListItemGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsSystemType).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeneralTypeItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListItems");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);

            entity.HasOne(d => d.ListItemGroupFkNavigation).WithMany(p => p.GeneralTypeItems)
                .HasForeignKey(d => d.ListItemGroupFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListItems_ListItemGroup");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MembershipType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Memberships");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MembersipPriceHistory>(entity =>
        {
            entity.ToTable("MembersipPriceHistory");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LastIncreasePercentage).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.MembershipTypeFkNavigation).WithMany(p => p.MembersipPriceHistories)
                .HasForeignKey(d => d.MembershipTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MembersipPriceHistory_Memberships");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Modules)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modules_Applications");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotificationTraces");

            entity.Property(e => e.Bcc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("BCC");
            entity.Property(e => e.Cc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CC");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CultureFk)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Receiver)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Sender)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.SentDate).HasColumnType("datetime");
            entity.Property(e => e.Subject)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Companies");

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationTraces_Cultures");

            entity.HasOne(d => d.NotificationTypeFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_ListItems");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.Notifications)
                .HasForeignKey(d => new { d.NotificationTemplateGroupFk, d.CultureFk })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_NotificationTemplates");
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.HasKey(e => new { e.NotificationTemplateGroupId, e.CultureFk });

            entity.Property(e => e.CultureFk)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.NotificationTemplates)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationTemplates_Cultures");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasIndex(e => new { e.CompanyFk, e.Name }, "IX_Pictures");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.PictureSize).HasColumnType("decimal(18, 5)");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_Companies");

            entity.HasOne(d => d.EntityFkNavigation).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.EntityFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_Entities");

            entity.HasOne(d => d.FileTypeFkNavigation).WithMany(p => p.PictureFileTypeFkNavigations)
                .HasForeignKey(d => d.FileTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_GeneralTypeItems");

            entity.HasOne(d => d.PictureCategoryFkNavigation).WithMany(p => p.PicturePictureCategoryFkNavigations)
                .HasForeignKey(d => d.PictureCategoryFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_GeneralTypeItems1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Applications");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Companies");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserRolePermissions");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PropertyValue)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.ComponentFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.ComponentFk)
                .HasConstraintName("FK_UserRolePermissions_Components");

            entity.HasOne(d => d.EndpointFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.EndpointFk)
                .HasConstraintName("FK_UserRolePermissions_Endpoints");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissions_Modules");

            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermissions_Roles");
        });

        modelBuilder.Entity<Sequence>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SequenceNameFormat)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.TextFormat)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Sequences)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sequences_Applications");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Sequences)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sequences_Companies");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsLocked).HasDefaultValue(true);
            entity.Property(e => e.LastAccess).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.LockedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.RequirePasswordChange).HasDefaultValue(true);
            entity.Property(e => e.UserName).HasMaxLength(30);

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Companies");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserFk, e.RoleFk });

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<VwCompanyUserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_CompanyUserRoles");

            entity.Property(e => e.ApplicationName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(130);
        });

        modelBuilder.Entity<VwGetAllCompanyPermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetAllCompanyPermissions");

            entity.Property(e => e.ApplicationName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndpointName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndpointUrl)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ModuleName)
                .HasMaxLength(85)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwGetAuditRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetAuditRecords");

            entity.Property(e => e.AuditInfoJson).HasColumnType("text");
            entity.Property(e => e.Browser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CultureFk)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeviceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Engine)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EntityName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.Expr1)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.HostName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SchemaName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(30);
        });

        modelBuilder.Entity<VwGetCompanyMembership>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetCompanyMemberships");

            entity.Property(e => e.ApplicationName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.CountryFk)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.DefaultCultureFk)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MembershipTypeName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.Subdomain)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwGetDatabaseTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetDatabaseTables");

            entity.Property(e => e.Expr1).HasMaxLength(128);
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.NameRowNumber).HasColumnName("name_row_number");
        });

        modelBuilder.Entity<VwGetGeneralTypeItemWithGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetGeneralTypeItemWithGroups");

            entity.Property(e => e.GeneralTypeGroupName).HasMaxLength(100);
            entity.Property(e => e.GeneralTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<VwGetRolePermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetRolePermissions");

            entity.Property(e => e.ApplicationName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndpointName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.EndpointUrl)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ModuleName)
                .HasMaxLength(85)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(85)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }


}
