using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace  Vito.Transverse.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationLicenseTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LicenseFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLicenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    ApplicationClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ApplicationSecret = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    UtcHoursDifference = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SchemaName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    EntityName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemEntity = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralTypeGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    IsSystemType = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItemGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    PositionIndex = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsApi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeneralTypeItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ListItemGroupFk = table.Column<long>(type: "bigint", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListItems_ListItemGroup",
                        column: x => x.ListItemGroupFk,
                        principalTable: "GeneralTypeGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    CountryFk = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    LanguageFk = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cultures_Countries",
                        column: x => x.CountryFk,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cultures_Languages",
                        column: x => x.LanguageFk,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MembersipPriceHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    LastIncreasePercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersipPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembersipPriceHistory_Memberships",
                        column: x => x.MembershipTypeFk,
                        principalTable: "MembershipTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Endpoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    ModuleFk = table.Column<long>(type: "bigint", nullable: false),
                    PositionIndex = table.Column<long>(type: "bigint", nullable: true),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    EndpointUrl = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    Method = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsApi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointsModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endpoints_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuleEndpoint_Modules",
                        column: x => x.ModuleFk,
                        principalTable: "Modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    CompanyClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CompanySecret = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    Subdomain = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    DefaultCultureFk = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    CountryFk = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    IsSystemCompany = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Countries",
                        column: x => x.CountryFk,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Cultures",
                        column: x => x.DefaultCultureFk,
                        principalTable: "Cultures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CultureTranslations",
                columns: table => new
                {
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    CultureFk = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    TranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    TranslationValue = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CultureTranslations", x => new { x.ApplicationFk, x.CultureFk, x.TranslationKey });
                    table.ForeignKey(
                        name: "FK_CultureTranslations_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CultureTranslations_Cultures",
                        column: x => x.CultureFk,
                        principalTable: "Cultures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    NotificationTemplateGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CultureFk = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    SubjectTemplateText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTemplateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => new { x.NotificationTemplateGroupId, x.CultureFk });
                    table.ForeignKey(
                        name: "FK_NotificationTemplates_Cultures",
                        column: x => x.CultureFk,
                        principalTable: "Cultures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    EndpointFk = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    ObjectId = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    ObjectName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    ObjectPropertyName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    DefaultPropertyValue = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    PositionIndex = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EndpointComponents_ModuleEndpoints",
                        column: x => x.EndpointFk,
                        principalTable: "Endpoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationOwners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationLicenseTypeFk = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationOwners_ApplicationLicenseTypes",
                        column: x => x.ApplicationLicenseTypeFk,
                        principalTable: "ApplicationLicenseTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOwners_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationOwners_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyEntityAudits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    EntityFk = table.Column<long>(type: "bigint", nullable: false),
                    AuditTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEntityAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEntityAudits_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyEntityAudits_Entities",
                        column: x => x.EntityFk,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyEntityAudits_GeneralTypeItems",
                        column: x => x.AuditTypeFk,
                        principalTable: "GeneralTypeItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyMemberships",
                columns: table => new
                {
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MembershipTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyApplications", x => new { x.CompanyFk, x.ApplicationFk });
                    table.UniqueConstraint("AK_CompanyMemberships_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMemberships_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyMemberships_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyMemberships_Memberships",
                        column: x => x.MembershipTypeFk,
                        principalTable: "MembershipTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    EntityFk = table.Column<long>(type: "bigint", nullable: false),
                    FileTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    PictureCategoryFk = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BinaryPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PictureSize = table.Column<decimal>(type: "decimal(18,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pictures_Entities",
                        column: x => x.EntityFk,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pictures_GeneralTypeItems",
                        column: x => x.FileTypeFk,
                        principalTable: "GeneralTypeItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pictures_GeneralTypeItems1",
                        column: x => x.PictureCategoryFk,
                        principalTable: "GeneralTypeItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    DescriptionTranslationKey = table.Column<string>(type: "varchar(85)", unicode: false, maxLength: 85, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateByUserFk = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Roles_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    SequenceTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    SequenceNameFormat = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    SequenceIndex = table.Column<long>(type: "bigint", nullable: false),
                    TextFormat = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sequences_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sequences_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    EmailValidated = table.Column<bool>(type: "bit", nullable: false),
                    RequirePasswordChange = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivationEmailSent = table.Column<bool>(type: "bit", nullable: false),
                    ActivationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LockedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedByUserFk = table.Column<long>(type: "bigint", nullable: true),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    NotificationTemplateGroupFk = table.Column<long>(type: "bigint", nullable: false),
                    CultureFk = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    NotificationTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sender = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    Receiver = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CC = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    BCC = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTraces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTraces_Cultures",
                        column: x => x.CultureFk,
                        principalTable: "Cultures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_ListItems",
                        column: x => x.NotificationTypeFk,
                        principalTable: "GeneralTypeItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTemplates",
                        columns: x => new { x.NotificationTemplateGroupFk, x.CultureFk },
                        principalTable: "NotificationTemplates",
                        principalColumns: new[] { "NotificationTemplateGroupId", "CultureFk" });
                });

            migrationBuilder.CreateTable(
                name: "CompanyMembershipPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyMembershipFk = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    ModuleFk = table.Column<long>(type: "bigint", nullable: false),
                    EndpointFk = table.Column<long>(type: "bigint", nullable: false),
                    ComponentFk = table.Column<long>(type: "bigint", nullable: true),
                    PropertyValue = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMembershipPermissions_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMembershipPermissions_CompanyMemberships",
                        column: x => x.CompanyMembershipFk,
                        principalTable: "CompanyMemberships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RoleFk = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    ModuleFk = table.Column<long>(type: "bigint", nullable: false),
                    EndpointFk = table.Column<long>(type: "bigint", nullable: true),
                    ComponentFk = table.Column<long>(type: "bigint", nullable: true),
                    PropertyValue = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles",
                        column: x => x.RoleFk,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Components",
                        column: x => x.ComponentFk,
                        principalTable: "Components",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Endpoints",
                        column: x => x.EndpointFk,
                        principalTable: "Endpoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Modules",
                        column: x => x.ModuleFk,
                        principalTable: "Modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    TraceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    UserFk = table.Column<long>(type: "bigint", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Engine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CultureId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EndPointUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Method = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    QueryString = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UserAgent = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Referer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTraces", x => x.TraceId);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_ListItems",
                        column: x => x.ActionTypeFk,
                        principalTable: "GeneralTypeItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Users",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    UserFk = table.Column<long>(type: "bigint", nullable: false),
                    EntityFk = table.Column<long>(type: "bigint", nullable: false),
                    AuditTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    AuditEntityIndex = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    HostName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DeviceType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Browser = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Platform = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Engine = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CultureFk = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EndPointUrl = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Method = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    QueryString = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UserAgent = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Referer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AuditChanges = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditRecords_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditRecords_Entities",
                        column: x => x.EntityFk,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditRecords_GeneralTypeItems",
                        column: x => x.AuditTypeFk,
                        principalTable: "GeneralTypeItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuditRecords_Users",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserFk = table.Column<long>(type: "bigint", nullable: false),
                    RoleFk = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFk = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationFk = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserFk = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserFk, x.RoleFk });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles",
                        column: x => x.RoleFk,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActionTypeFk",
                table: "ActivityLogs",
                column: "ActionTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserFk",
                table: "ActivityLogs",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOwners_ApplicationFk",
                table: "ApplicationOwners",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOwners_ApplicationLicenseTypeFk",
                table: "ApplicationOwners",
                column: "ApplicationLicenseTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOwners_CompanyFk",
                table: "ApplicationOwners",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_AuditTypeFk",
                table: "AuditRecords",
                column: "AuditTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_CompanyFk",
                table: "AuditRecords",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_EntityFk",
                table: "AuditRecords",
                column: "EntityFk");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRecords_UserFk",
                table: "AuditRecords",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryFk",
                table: "Companies",
                column: "CountryFk");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_DefaultCultureFk",
                table: "Companies",
                column: "DefaultCultureFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEntityAudits_AuditTypeFk",
                table: "CompanyEntityAudits",
                column: "AuditTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEntityAudits_CompanyFk",
                table: "CompanyEntityAudits",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEntityAudits_EntityFk",
                table: "CompanyEntityAudits",
                column: "EntityFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMembershipPermissions",
                table: "CompanyMembershipPermissions",
                column: "CompanyMembershipFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMemberships",
                table: "CompanyMemberships",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMemberships_ApplicationFk",
                table: "CompanyMemberships",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMemberships_MembershipTypeFk",
                table: "CompanyMemberships",
                column: "MembershipTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ApplicationFk",
                table: "Components",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_Components_EndpointFk",
                table: "Components",
                column: "EndpointFk");

            migrationBuilder.CreateIndex(
                name: "IX_Cultures_CountryFk",
                table: "Cultures",
                column: "CountryFk");

            migrationBuilder.CreateIndex(
                name: "IX_Cultures_LanguageFk",
                table: "Cultures",
                column: "LanguageFk");

            migrationBuilder.CreateIndex(
                name: "IX_CultureTranslations",
                table: "CultureTranslations",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_CultureTranslations_CultureFk",
                table: "CultureTranslations",
                column: "CultureFk");

            migrationBuilder.CreateIndex(
                name: "IX_Endpoints_ApplicationFk",
                table: "Endpoints",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_Endpoints_ModuleFk",
                table: "Endpoints",
                column: "ModuleFk");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralTypeItems_ListItemGroupFk",
                table: "GeneralTypeItems",
                column: "ListItemGroupFk");

            migrationBuilder.CreateIndex(
                name: "IX_MembersipPriceHistory_MembershipTypeFk",
                table: "MembersipPriceHistory",
                column: "MembershipTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_ApplicationFk",
                table: "Modules",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CompanyFk",
                table: "Notifications",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CultureFk",
                table: "Notifications",
                column: "CultureFk");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTemplateGroupFk_CultureFk",
                table: "Notifications",
                columns: new[] { "NotificationTemplateGroupFk", "CultureFk" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeFk",
                table: "Notifications",
                column: "NotificationTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_CultureFk",
                table: "NotificationTemplates",
                column: "CultureFk");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures",
                table: "Pictures",
                columns: new[] { "CompanyFk", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_EntityFk",
                table: "Pictures",
                column: "EntityFk");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_FileTypeFk",
                table: "Pictures",
                column: "FileTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_PictureCategoryFk",
                table: "Pictures",
                column: "PictureCategoryFk");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ComponentFk",
                table: "RolePermissions",
                column: "ComponentFk");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_EndpointFk",
                table: "RolePermissions",
                column: "EndpointFk");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ModuleFk",
                table: "RolePermissions",
                column: "ModuleFk");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleFk",
                table: "RolePermissions",
                column: "RoleFk");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ApplicationFk",
                table: "Roles",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CompanyFk",
                table: "Roles",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_Sequences_ApplicationFk",
                table: "Sequences",
                column: "ApplicationFk");

            migrationBuilder.CreateIndex(
                name: "IX_Sequences_CompanyFk",
                table: "Sequences",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleFk",
                table: "UserRoles",
                column: "RoleFk");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyFk",
                table: "Users",
                column: "CompanyFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "ApplicationOwners");

            migrationBuilder.DropTable(
                name: "AuditRecords");

            migrationBuilder.DropTable(
                name: "CompanyEntityAudits");

            migrationBuilder.DropTable(
                name: "CompanyMembershipPermissions");

            migrationBuilder.DropTable(
                name: "CultureTranslations");

            migrationBuilder.DropTable(
                name: "MembersipPriceHistory");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Sequences");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ApplicationLicenseTypes");

            migrationBuilder.DropTable(
                name: "CompanyMemberships");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "GeneralTypeItems");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MembershipTypes");

            migrationBuilder.DropTable(
                name: "GeneralTypeGroups");

            migrationBuilder.DropTable(
                name: "Endpoints");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
