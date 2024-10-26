using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VitoTransverseContext : DbContext
{
    public VitoTransverseContext()
    {
    }

    public VitoTransverseContext(DbContextOptions<VitoTransverseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Culture> Cultures { get; set; }

    public virtual DbSet<CultureTranslation> CultureTranslations { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<ListItem> ListItems { get; set; }

    public virtual DbSet<ListItemGroup> ListItemGroups { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRolePermission> UserRolePermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local)\\MSSQLSERVER2019;Database=Vito.Transverse;Integrated Security=false;TrustServerCertificate=True;Trusted_Connection=True;Persist Security Info=True; User ID=vito.torres;Password=Vito87152+24Oct*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.TraceId).HasName("PK_UserTraces");

            entity.Property(e => e.Browser).HasMaxLength(50);
            entity.Property(e => e.CultureId).HasMaxLength(50);
            entity.Property(e => e.DeviceName).HasMaxLength(50);
            entity.Property(e => e.DeviceType).HasMaxLength(50);
            entity.Property(e => e.Engine).HasMaxLength(50);
            entity.Property(e => e.EventDateTime).HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.Platform).HasMaxLength(50);

            entity.HasOne(d => d.ActionTypeFkNavigation).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ActionTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_ListItems");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTraces_Users");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Applications_1");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Secret).HasDefaultValueSql("(newid())");

            entity.HasMany(d => d.CompanyFks).WithMany(p => p.ApplicationFks)
                .UsingEntity<Dictionary<string, object>>(
                    "CompanyApplication",
                    r => r.HasOne<Company>().WithMany()
                        .HasForeignKey("CompanyFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CompanyApplications_Companies"),
                    l => l.HasOne<Application>().WithMany()
                        .HasForeignKey("ApplicationFk")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CompanyApplications_Applications"),
                    j =>
                    {
                        j.HasKey("ApplicationFk", "CompanyFk");
                        j.ToTable("CompanyApplications");
                        j.IndexerProperty<Guid>("ApplicationFk").HasDefaultValueSql("(newid())");
                        j.IndexerProperty<Guid>("CompanyFk").HasDefaultValueSql("(newid())");
                    });
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CountryFk).HasMaxLength(2);
            entity.Property(e => e.DefaultCultureFk).HasMaxLength(5);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Secret).HasDefaultValueSql("(newid())");
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

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PageComponents");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ComponentId).HasMaxLength(50);
            entity.Property(e => e.ComponentName).HasMaxLength(50);

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
            entity.HasKey(e => new { e.CultureFk, e.TranslationKey });

            entity.Property(e => e.CultureFk).HasMaxLength(5);
            entity.Property(e => e.TranslationKey).HasMaxLength(50);
            entity.Property(e => e.TranslationValue).HasMaxLength(250);

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.CultureTranslations)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CultureTranslations_Cultures");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(2);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(50);
        });

        modelBuilder.Entity<ListItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(100);

            entity.HasOne(d => d.ListItemGroupFkNavigation).WithMany(p => p.ListItems)
                .HasForeignKey(d => d.ListItemGroupFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListItems_ListItemGroup");
        });

        modelBuilder.Entity<ListItemGroup>(entity =>
        {
            entity.ToTable("ListItemGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsSystemType).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey).HasMaxLength(100);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.NotificationTemplateFk).HasMaxLength(25);
            entity.Property(e => e.Receiver).HasMaxLength(500);
            entity.Property(e => e.Sender).HasMaxLength(50);
            entity.Property(e => e.SentDate).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(250);

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CultureFk)
                .HasConstraintName("FK_NotificationTraces_Cultures");

            entity.HasOne(d => d.NotificationTemplateFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTemplateFk)
                .HasConstraintName("FK_NotificationTraces_NotificationTemplates");

            entity.HasOne(d => d.NotificationTypeFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_ListItems");
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(25);
            entity.Property(e => e.CultureFk).HasMaxLength(5);
            entity.Property(e => e.TemplateText)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.CultureFkNavigation).WithMany(p => p.NotificationTemplates)
                .HasForeignKey(d => d.CultureFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationTemplates_Cultures");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PagesModules");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.NameTranslationKey)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.Pages)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModulePages_Modules");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.DocumentValue).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persons_Companies");

            entity.HasOne(d => d.DocumentTypeFkNavigation).WithMany(p => p.PersonDocumentTypeFkNavigations)
                .HasForeignKey(d => d.DocumentTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persons_ListItems");

            entity.HasOne(d => d.GenderFkNavigation).WithMany(p => p.PersonGenderFkNavigations)
                .HasForeignKey(d => d.GenderFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persons_ListItems1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameTranslationKey).HasMaxLength(150);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.IsLocked).HasDefaultValue(true);
            entity.Property(e => e.LastAccess).HasColumnType("datetime");
            entity.Property(e => e.LockedDate).HasColumnType("datetime");
            entity.Property(e => e.RequirePasswordChange).HasDefaultValue(true);
            entity.Property(e => e.UserName).HasMaxLength(25);

            entity.HasOne(d => d.CompanyFkNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Companies");

            entity.HasOne(d => d.PersonFkNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Persons");

            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<UserRolePermission>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ComponentFkNavigation).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.ComponentFk)
                .HasConstraintName("FK_UserRolePermissions_Components");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissions_Modules");

            entity.HasOne(d => d.PageFkNavigation).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.PageFk)
                .HasConstraintName("FK_UserRolePermissions_Pages");

            entity.HasOne(d => d.UserRoleFkNavigation).WithMany(p => p.UserRolePermissions)
                .HasForeignKey(d => d.UserRoleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissions_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
