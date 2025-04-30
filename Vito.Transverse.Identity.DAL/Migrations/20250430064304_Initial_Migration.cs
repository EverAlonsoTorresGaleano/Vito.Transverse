using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vito.Transverse.Identity.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Secret = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UtcHoursDifference = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListItemGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsSystemType = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItemGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryFk = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    LanguageFk = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
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
                name: "ListItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ListItemGroupFk = table.Column<long>(type: "bigint", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListItems_ListItemGroup",
                        column: x => x.ListItemGroupFk,
                        principalTable: "ListItemGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ModuleFk = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagesModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModulePages_Modules",
                        column: x => x.ModuleFk,
                        principalTable: "Modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Subdomain = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Secret = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DefaultCultureFk = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CountryFk = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    IsSystemCompany = table.Column<bool>(type: "bit", nullable: false)
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
                    CultureFk = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TranslationKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TranslationValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CultureTranslations", x => new { x.CultureFk, x.TranslationKey });
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
                    CultureFk = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
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
                    PageFk = table.Column<long>(type: "bigint", nullable: false),
                    NameTranslationKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageComponents_ModulePages",
                        column: x => x.PageFk,
                        principalTable: "Pages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyApplications",
                columns: table => new
                {
                    ApplicationFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CompanyFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyApplications", x => new { x.ApplicationFk, x.CompanyFk });
                    table.ForeignKey(
                        name: "FK_CompanyApplications_Applications",
                        column: x => x.ApplicationFk,
                        principalTable: "Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyApplications_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    DocumentValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    GenderFk = table.Column<long>(type: "bigint", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_ListItems",
                        column: x => x.DocumentTypeFk,
                        principalTable: "ListItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_ListItems1",
                        column: x => x.GenderFk,
                        principalTable: "ListItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTemplateGroupFk = table.Column<long>(type: "bigint", nullable: false),
                    CultureFk = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NotificationTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BCC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_Notifications_ListItems",
                        column: x => x.NotificationTypeFk,
                        principalTable: "ListItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTemplates",
                        columns: x => new { x.NotificationTemplateGroupFk, x.CultureFk },
                        principalTable: "NotificationTemplates",
                        principalColumns: new[] { "NotificationTemplateGroupId", "CultureFk" });
                });

            migrationBuilder.CreateTable(
                name: "UserRolePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserRoleFk = table.Column<long>(type: "bigint", nullable: false),
                    ModuleFk = table.Column<long>(type: "bigint", nullable: false),
                    PageFk = table.Column<long>(type: "bigint", nullable: true),
                    ComponentFk = table.Column<long>(type: "bigint", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Components",
                        column: x => x.ComponentFk,
                        principalTable: "Components",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Modules",
                        column: x => x.ModuleFk,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Pages",
                        column: x => x.PageFk,
                        principalTable: "Pages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRolePermissions_Roles",
                        column: x => x.UserRoleFk,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PersonFk = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleFk = table.Column<long>(type: "bigint", nullable: false),
                    EmailValidated = table.Column<bool>(type: "bit", nullable: false),
                    RequirePasswordChange = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivationEmailSent = table.Column<bool>(type: "bit", nullable: false),
                    ActivationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LockedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies",
                        column: x => x.CompanyFk,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Persons",
                        column: x => x.PersonFk,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleFk,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    TraceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserFk = table.Column<long>(type: "bigint", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActionTypeFk = table.Column<long>(type: "bigint", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Engine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CultureId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddtionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTraces", x => x.TraceId);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_ListItems",
                        column: x => x.ActionTypeFk,
                        principalTable: "ListItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTraces_Users",
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
                name: "IX_Companies_CountryFk",
                table: "Companies",
                column: "CountryFk");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_DefaultCultureFk",
                table: "Companies",
                column: "DefaultCultureFk");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyApplications_CompanyFk",
                table: "CompanyApplications",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_Components_PageFk",
                table: "Components",
                column: "PageFk");

            migrationBuilder.CreateIndex(
                name: "IX_Cultures_CountryFk",
                table: "Cultures",
                column: "CountryFk");

            migrationBuilder.CreateIndex(
                name: "IX_Cultures_LanguageFk",
                table: "Cultures",
                column: "LanguageFk");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ListItemGroupFk",
                table: "ListItems",
                column: "ListItemGroupFk");

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
                name: "IX_Pages_ModuleFk",
                table: "Pages",
                column: "ModuleFk");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CompanyFk",
                table: "Persons",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DocumentTypeFk",
                table: "Persons",
                column: "DocumentTypeFk");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderFk",
                table: "Persons",
                column: "GenderFk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermissions_ComponentFk",
                table: "UserRolePermissions",
                column: "ComponentFk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermissions_ModuleFk",
                table: "UserRolePermissions",
                column: "ModuleFk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermissions_PageFk",
                table: "UserRolePermissions",
                column: "PageFk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermissions_UserRoleFk",
                table: "UserRolePermissions",
                column: "UserRoleFk");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyFk",
                table: "Users",
                column: "CompanyFk");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonFk",
                table: "Users",
                column: "PersonFk");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleFk",
                table: "Users",
                column: "RoleFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "CompanyApplications");

            migrationBuilder.DropTable(
                name: "CultureTranslations");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "UserRolePermissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "ListItemGroup");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
