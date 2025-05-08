using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VitoTransverseDbContext : DbContext
{
    public VitoTransverseDbContext()
    {
    }

    public VitoTransverseDbContext(DbContextOptions<VitoTransverseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<AuditEntity> AuditEntities { get; set; }

    public virtual DbSet<AuditRecord> AuditRecords { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyEntityAudit> CompanyEntityAudits { get; set; }

    public virtual DbSet<CompanyMembership> CompanyMemberships { get; set; }

    public virtual DbSet<CompanyMembershipPermission> CompanyMembershipPermissions { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Culture> Cultures { get; set; }

    public virtual DbSet<CultureTranslation> CultureTranslations { get; set; }

    public virtual DbSet<GeneralTypeGroup> GeneralTypeGroups { get; set; }

    public virtual DbSet<GeneralTypeItem> GeneralTypeItems { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MembershipType> MembershipTypes { get; set; }

    public virtual DbSet<MembersipPriceHistory> MembersipPriceHistories { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=Vito.Transverse.DB;Integrated Security=false;TrustServerCertificate=True;Persist Security Info=True; Encrypt=Optional;Command Timeout=120;MultipleActiveResultSets=true;Max Pool Size=200;User ID=sa;Password=VitoLaptop2025+;Application Name=Vito.Transverse;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.TraceId).HasName("PK_UserTraces");

            entity.Property(e => e.AddtionalInformation).HasColumnType("text");
            entity.Property(e => e.Browser).HasMaxLength(50);
            entity.Property(e => e.CultureId).HasMaxLength(50);
            entity.Property(e => e.DeviceName).HasMaxLength(50);
            entity.Property(e => e.DeviceType).HasMaxLength(50);
            entity.Property(e => e.Engine).HasMaxLength(50);
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.Platform).HasMaxLength(50);
            entity.Property(e => e.RequestEndpoint)
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
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AuditEntity>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EntityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SchemaName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AuditRecord>(entity =>
        {
            entity.Property(e => e.AuditEntityIndex)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.HostName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AuditEntityFkNavigation).WithMany(p => p.AuditRecords)
                .HasForeignKey(d => d.AuditEntityFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_AuditEntities");

            entity.HasOne(d => d.AuditTypeFkNavigation).WithMany(p => p.AuditRecords)
                .HasForeignKey(d => d.AuditTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditRecords_GeneralTypeItems");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CompanyClient).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CompanySecret).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CountryFk).HasMaxLength(2);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DefaultCultureFk).HasMaxLength(5);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Subdomain).HasMaxLength(150);

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

            entity.HasOne(d => d.AuditEntityFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .HasForeignKey(d => d.AuditEntityFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_AuditEntities");

            entity.HasOne(d => d.AuditTypeFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .HasForeignKey(d => d.AuditTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_GeneralTypeItems");

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.CompanyEntityAudits)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyEntityAudits_Companies");
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
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CompanyMembershipFkNavigation).WithMany(p => p.CompanyMembershipPermissions)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.CompanyMembershipFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMembershipPermissions_CompanyMemberships");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PageComponents");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DefaultPropertyValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ObjectId).HasMaxLength(50);
            entity.Property(e => e.ObjectName).HasMaxLength(50);
            entity.Property(e => e.ObjectPropertyName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Components_Applications");

            entity.HasOne(d => d.PageFkNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.PageFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PageComponents_ModulePages");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(2);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(50);
        });

        modelBuilder.Entity<Culture>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(5);
            entity.Property(e => e.CountryFk).HasMaxLength(2);
            entity.Property(e => e.LanguageFk).HasMaxLength(2);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(50);

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

            entity.Property(e => e.CultureFk).HasMaxLength(5);
            entity.Property(e => e.TranslationKey).HasMaxLength(50);
            entity.Property(e => e.TranslationValue).HasMaxLength(250);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.CultureTranslations)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Applications");

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.CultureTranslations)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Cultures");
        });

        modelBuilder.Entity<GeneralTypeGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListItemGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsSystemType).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(100);
        });

        modelBuilder.Entity<GeneralTypeItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListItems");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(100);

            entity.HasOne(d => d.ListItemGroupFkNavigation).WithMany(p => p.GeneralTypeItems)
                .HasForeignKey(d => d.ListItemGroupFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListItems_ListItemGroup");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(2);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(50);
        });

        modelBuilder.Entity<MembershipType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Memberships");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey).HasMaxLength(50);
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
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(75)
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
                .HasColumnName("BCC");
            entity.Property(e => e.Cc)
                .HasMaxLength(500)
                .HasColumnName("CC");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CultureFk).HasMaxLength(5);
            entity.Property(e => e.Receiver).HasMaxLength(500);
            entity.Property(e => e.Sender).HasMaxLength(50);
            entity.Property(e => e.SentDate).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(250);

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

            entity.Property(e => e.CultureFk).HasMaxLength(5);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(25);

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.NotificationTemplates)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationTemplates_Cultures");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PagesModules");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.ApplicationFkNavigation).WithMany(p => p.Pages)
                .HasForeignKey(d => d.ApplicationFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pages_Applications");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.Pages)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModulePages_Modules");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.NameTranslationKey).HasMaxLength(150);

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
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ComponentFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.ComponentFk)
                .HasConstraintName("FK_UserRolePermissions_Components");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissions_Modules");

            entity.HasOne(d => d.PageFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PageFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissions_Pages");

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
                .IsFixedLength();
            entity.Property(e => e.IsLocked).HasDefaultValue(true);
            entity.Property(e => e.LastAccess).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.LockedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength();
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

            entity.Property(e => e.ApplicationName).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(150);
            entity.Property(e => e.UserName).HasMaxLength(130);
        });

        modelBuilder.Entity<VwGetAllCompanyPermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetAllCompanyPermissions");

            entity.Property(e => e.ApplicationName).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.ModuleName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.PageName)
                .HasMaxLength(75)
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
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HostName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(100);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SchemaName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(30);
        });

        modelBuilder.Entity<VwGetCompanyMembership>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_GetCompanyMemberships");

            entity.Property(e => e.ApplicationName).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.CountryFk).HasMaxLength(2);
            entity.Property(e => e.DefaultCultureFk).HasMaxLength(5);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.MembershipTypeName).HasMaxLength(50);
            entity.Property(e => e.Subdomain).HasMaxLength(150);
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

            entity.Property(e => e.ApplicationName).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.ModuleName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.PageName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.RoleName).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
