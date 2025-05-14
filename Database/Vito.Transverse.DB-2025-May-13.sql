USE [master]
GO
/****** Object:  Database [Vito.Transverse.DB]    Script Date: 5/13/2025 8:46:17 PM ******/
CREATE DATABASE [Vito.Transverse.DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Vito.Transverse.DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Vito.Transverse.DB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Vito.Transverse.DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Vito.Transverse.DB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Vito.Transverse.DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Vito.Transverse.DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Vito.Transverse.DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Vito.Transverse.DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Vito.Transverse.DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Vito.Transverse.DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Vito.Transverse.DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Vito.Transverse.DB] SET  MULTI_USER 
GO
ALTER DATABASE [Vito.Transverse.DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Vito.Transverse.DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Vito.Transverse.DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Vito.Transverse.DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Vito.Transverse.DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Vito.Transverse.DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Vito.Transverse.DB', N'ON'
GO
ALTER DATABASE [Vito.Transverse.DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [Vito.Transverse.DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Vito.Transverse.DB]
GO
/****** Object:  Table [dbo].[AuditRecords]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditRecords](
	[CompanyFk] [bigint] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserFk] [bigint] NOT NULL,
	[AuditEntityFk] [bigint] NOT NULL,
	[AuditTypeFk] [bigint] NOT NULL,
	[AuditEntityIndex] [varchar](50) NOT NULL,
	[HostName] [varchar](50) NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[DeviceType] [varchar](50) NULL,
	[Browser] [varchar](50) NOT NULL,
	[Platform] [varchar](50) NOT NULL,
	[Engine] [varchar](50) NOT NULL,
	[CultureFk] [varchar](50) NOT NULL,
	[AuditInfoJson] [text] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AuditRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[CompanyFk] [bigint] NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[EmailValidated] [bit] NOT NULL,
	[RequirePasswordChange] [bit] NOT NULL,
	[RetryCount] [int] NOT NULL,
	[LastAccess] [datetime] NULL,
	[ActivationEmailSent] [bit] NOT NULL,
	[ActivationId] [uniqueidentifier] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[LockedDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedByUserFk] [bigint] NULL,
	[LastUpdateDate] [datetime] NULL,
	[UpdatedByUserFk] [bigint] NULL,
	[Avatar] [varbinary](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](75) NOT NULL,
	[CompanyClient] [uniqueidentifier] NOT NULL,
	[CompanySecret] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[Subdomain] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[DefaultCultureFk] [nvarchar](5) NOT NULL,
	[CountryFk] [nvarchar](2) NOT NULL,
	[IsSystemCompany] [bit] NOT NULL,
	[Avatar] [varbinary](max) NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralTypeItems]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralTypeItems](
	[ListItemGroupFk] [bigint] NOT NULL,
	[OrderIndex] [int] NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [nvarchar](100) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_ListItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditEntities]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditEntities](
	[Id] [bigint] NOT NULL,
	[SchemaName] [varchar](50) NOT NULL,
	[EntityName] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemEntity] [bit] NOT NULL,
 CONSTRAINT [PK_AuditEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetAuditRecords]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetAuditRecords]
AS
SELECT ar.CreationDate, ar.CompanyFk, c.Name, ar.UserFk, u.UserName, ar.AuditTypeFk, t.NameTranslationKey, ar.AuditEntityFk, ae.SchemaName, ae.EntityName, ae.IsSystemEntity, ar.HostName, ar.DeviceType, ar.Browser, ar.Platform, 
                  ar.Engine, ar.CultureFk, ar.AuditInfoJson
FROM     dbo.AuditRecords AS ar INNER JOIN
                  dbo.AuditEntities AS ae ON ar.AuditEntityFk = ae.Id INNER JOIN
                  dbo.Companies AS c ON ar.CompanyFk = c.Id INNER JOIN
                  dbo.Users AS u ON ar.UserFk = u.Id INNER JOIN
                  dbo.GeneralTypeItems AS t ON ar.AuditTypeFk = t.Id
GO
/****** Object:  Table [dbo].[GeneralTypeGroups]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralTypeGroups](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [nvarchar](100) NOT NULL,
	[IsSystemType] [bit] NOT NULL,
 CONSTRAINT [PK_ListItemGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetGeneralTypeItemWithGroups]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetGeneralTypeItemWithGroups]
AS
SELECT TOP (100) PERCENT lig.Id AS GeneralTypeGroupId, lig.NameTranslationKey AS GeneralTypeGroupName, lig.IsSystemType AS GeneralTypeGroup, lig.IsSystemType, li.Id AS GeneralTypeItemId, 
                  li.NameTranslationKey AS GeneralTypeName, li.OrderIndex AS GeneralTypeItemIndex, li.IsEnabled AS GeneralTypeItemIsEnabled
FROM     dbo.GeneralTypeGroups AS lig INNER JOIN
                  dbo.GeneralTypeItems AS li ON lig.Id = li.ListItemGroupFk
ORDER BY GeneralTypeGroupId, GeneralTypeItemId, GeneralTypeItemIndex
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](150) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
 CONSTRAINT [PK_Roles_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](75) NOT NULL,
	[ApplicationClient] [uniqueidentifier] NOT NULL,
	[ApplicationSecret] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[Avatar] [varbinary](max) NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Applications_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[UserFk] [bigint] NOT NULL,
	[RoleFk] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserFk] ASC,
	[RoleFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_CompanyUserRoles]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_CompanyUserRoles]
AS
SELECT c.Id AS CompanyId, c.Name AS CompanyName, a.Id AS ApplicationId, a.Name AS ApplicationName, u.Id AS UserId, u.UserName + u.LastName AS UserName, r.Id AS RoleId, r.NameTranslationKey AS RoleName, 
                  ur.IsActive AS UserRoleIsActive
FROM     dbo.Roles AS r INNER JOIN
                  dbo.UserRoles AS ur ON r.Id = ur.RoleFk INNER JOIN
                  dbo.Users AS u ON u.Id = ur.UserFk INNER JOIN
                  dbo.Applications AS a ON ur.ApplicationFk = a.Id INNER JOIN
                  dbo.Companies AS c ON ur.CompanyFk = c.Id
GO
/****** Object:  Table [dbo].[CompanyMemberships]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyMemberships](
	[Id] [bigint] NOT NULL,
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[MembershipTypeFk] [bigint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CompanyApplications] PRIMARY KEY CLUSTERED 
(
	[CompanyFk] ASC,
	[ApplicationFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembershipTypes]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembershipTypes](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [nvarchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Memberships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetCompanyMemberships]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetCompanyMemberships]
AS
SELECT cm.Id AS CompanyMemberShipId, cm.MembershipTypeFk, t.NameTranslationKey AS MembershipTypeName, t.IsActive AS MembershipTypeIsActive, c.Id AS CompanyId, c.Name AS CompanyName, c.CompanyClient, c.CompanySecret, 
                  c.Subdomain, c.Email, c.IsActive AS CompanyIsActive, c.IsSystemCompany, c.DefaultCultureFk, c.CountryFk, a.Id AS ApplicationId, a.Name AS ApplicationName, a.IsActive AS ApplicationIsActive, a.ApplicationClient, 
                  a.ApplicationSecret
FROM     dbo.Companies AS c INNER JOIN
                  dbo.CompanyMemberships AS cm ON cm.CompanyFk = c.Id AND c.Id = cm.CompanyFk INNER JOIN
                  dbo.Applications AS a ON cm.ApplicationFk = a.Id AND cm.ApplicationFk = a.Id INNER JOIN
                  dbo.MembershipTypes AS t ON cm.MembershipTypeFk = t.Id
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[ApplicationFk] [bigint] NOT NULL,
	[ModuleFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[PositionIndex] [bigint] NULL,
	[NameTranslationKey] [varchar](75) NOT NULL,
	[PageUrl] [varchar](75) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsApi] [bit] NOT NULL,
 CONSTRAINT [PK_PagesModules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[ApplicationFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](75) NOT NULL,
	[PositionIndex] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsApi] [bit] NOT NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyMembershipPermissions]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyMembershipPermissions](
	[CompanyMembershipFk] [bigint] NOT NULL,
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[ModuleFk] [bigint] NOT NULL,
	[PageFk] [bigint] NOT NULL,
	[ComponentFk] [bigint] NULL,
	[PropertyValue] [varchar](50) NULL,
 CONSTRAINT [PK_CompanyMembershipPermissions_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetAllCompanyPermissions]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*INNER JOIN Components com ON cmp.ComponentFk=com.Id
WHERE cmp.CompanyFk=1*/
CREATE VIEW [dbo].[Vw_GetAllCompanyPermissions]
AS
SELECT cmp.CompanyMembershipFk, cmp.Id AS CompanyMembershipPermissionsId, c.Id AS CompanyId, c.Name AS CompanyName, a.Id AS ApplicationId, a.Name AS ApplicationName, a.IsActive AS ApplicationIsActive, m.Id AS ModuleId, 
                  m.NameTranslationKey AS ModuleName, m.IsActive AS ModuleIsActive, p.Id AS PageId, p.NameTranslationKey AS PageName, p.PageUrl, p.IsActive AS PageIsActive, cmp.ComponentFk
FROM     dbo.CompanyMembershipPermissions AS cmp INNER JOIN
                  dbo.Applications AS a ON cmp.ApplicationFk = a.Id INNER JOIN
                  dbo.Companies AS c ON cmp.CompanyFk = c.Id INNER JOIN
                  dbo.Modules AS m ON cmp.ModuleFk = m.Id INNER JOIN
                  dbo.Pages AS p ON cmp.PageFk = p.Id
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleFk] [bigint] NOT NULL,
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[ModuleFk] [bigint] NOT NULL,
	[PageFk] [bigint] NOT NULL,
	[ComponentFk] [bigint] NULL,
	[PropertyValue] [varchar](50) NULL,
 CONSTRAINT [PK_UserRolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetRolePermissions]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetRolePermissions]
AS
SELECT rp.RoleFk, r.NameTranslationKey AS RoleName, rp.Id AS RolePermissionsId, c.Id AS CompanyId, c.Name AS CompanyName, a.Id AS ApplicationId, a.Name AS ApplicationName, a.IsActive AS ApplicationIsActive, m.Id AS ModuleId, 
                  m.NameTranslationKey AS ModuleName, m.IsActive AS ModuleIsActive, p.Id AS PageId, p.NameTranslationKey AS PageName, p.PageUrl, p.IsActive AS PageIsActive, rp.ComponentFk
FROM     dbo.RolePermissions AS rp INNER JOIN
                  dbo.Applications AS a ON rp.ApplicationFk = a.Id INNER JOIN
                  dbo.Companies AS c ON rp.CompanyFk = c.Id INNER JOIN
                  dbo.Modules AS m ON rp.ModuleFk = m.Id INNER JOIN
                  dbo.Pages AS p ON rp.PageFk = p.Id INNER JOIN
                  dbo.Roles AS r ON rp.RoleFk = r.Id
GO
/****** Object:  View [dbo].[Vw_GetDatabaseTables]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetDatabaseTables]
AS
SELECT ROW_NUMBER() OVER (ORDER BY o.name ASC) AS name_row_number, s.name, o.name AS Expr1, 1 AS IsActive FROM     sys.all_objects AS o INNER JOIN
                  sys.schemas AS s ON s.schema_id = o.schema_id
WHERE  (o.type = 'U') AND (o.schema_id = 1)
GO
/****** Object:  Table [dbo].[ActivityLogs]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLogs](
	[CompanyFk] [bigint] NOT NULL,
	[UserFk] [bigint] NOT NULL,
	[TraceId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[DeviceName] [nvarchar](50) NOT NULL,
	[DeviceType] [nvarchar](50) NOT NULL,
	[ActionTypeFk] [bigint] NOT NULL,
	[IpAddress] [nvarchar](50) NOT NULL,
	[Browser] [nvarchar](50) NOT NULL,
	[Platform] [nvarchar](50) NOT NULL,
	[Engine] [nvarchar](50) NOT NULL,
	[CultureId] [nvarchar](50) NOT NULL,
	[RequestEndpoint] [varchar](100) NOT NULL,
	[AddtionalInformation] [text] NOT NULL,
 CONSTRAINT [PK_UserTraces] PRIMARY KEY CLUSTERED 
(
	[TraceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyEntityAudits]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyEntityAudits](
	[CompanyFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[AuditEntityFk] [bigint] NOT NULL,
	[AuditTypeFk] [bigint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[UpdatedByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CompanyEntityAudits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Components]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Components](
	[ApplicationFk] [bigint] NOT NULL,
	[PageFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](75) NOT NULL,
	[ObjectId] [nvarchar](50) NOT NULL,
	[ObjectName] [nvarchar](50) NOT NULL,
	[ObjectPropertyName] [varchar](50) NOT NULL,
	[DefaultPropertyValue] [varchar](50) NOT NULL,
	[PositionIndex] [bigint] NULL,
 CONSTRAINT [PK_PageComponents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [nvarchar](2) NOT NULL,
	[NameTranslationKey] [nvarchar](50) NOT NULL,
	[UtcHoursDifference] [int] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cultures]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cultures](
	[Id] [nvarchar](5) NOT NULL,
	[NameTranslationKey] [nvarchar](50) NOT NULL,
	[CountryFk] [nvarchar](2) NOT NULL,
	[LanguageFk] [nvarchar](2) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Cultures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CultureTranslations]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CultureTranslations](
	[ApplicationFk] [bigint] NOT NULL,
	[CultureFk] [nvarchar](5) NOT NULL,
	[TranslationKey] [nvarchar](50) NOT NULL,
	[TranslationValue] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CultureTranslations] PRIMARY KEY CLUSTERED 
(
	[ApplicationFk] ASC,
	[CultureFk] ASC,
	[TranslationKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [nvarchar](2) NOT NULL,
	[NameTranslationKey] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembersipPriceHistory]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembersipPriceHistory](
	[MembershipTypeFk] [bigint] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Price] [decimal](18, 4) NOT NULL,
	[LastPrice] [decimal](18, 4) NULL,
	[LastIncreasePercentage] [decimal](18, 4) NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
 CONSTRAINT [PK_MembersipPriceHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[CompanyFk] [bigint] NOT NULL,
	[NotificationTemplateGroupFk] [bigint] NOT NULL,
	[CultureFk] [nvarchar](5) NOT NULL,
	[NotificationTypeFk] [bigint] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
	[Receiver] [nvarchar](500) NOT NULL,
	[CC] [nvarchar](500) NULL,
	[BCC] [nvarchar](500) NULL,
	[Subject] [nvarchar](250) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IsSent] [bit] NOT NULL,
	[SentDate] [datetime] NULL,
	[IsHtml] [bit] NOT NULL,
 CONSTRAINT [PK_NotificationTraces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationTemplates]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationTemplates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NotificationTemplateGroupId] [bigint] NOT NULL,
	[CultureFk] [nvarchar](5) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[SubjectTemplateText] [nvarchar](max) NOT NULL,
	[MessageTemplateText] [nvarchar](max) NULL,
	[IsHtml] [bit] NOT NULL,
 CONSTRAINT [PK_NotificationTemplates] PRIMARY KEY CLUSTERED 
(
	[NotificationTemplateGroupId] ASC,
	[CultureFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sequences]    Script Date: 5/13/2025 8:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sequences](
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[SequenceTypeFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[SequenceNameFormat] [varchar](75) NOT NULL,
	[SequenceIndex] [bigint] NOT NULL,
	[TextFormat] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Sequences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ActivityLogs] ON 
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 52, CAST(N'2025-05-06T08:00:44.883' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 53, CAST(N'2025-05-06T09:03:38.817' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 54, CAST(N'2025-05-06T09:13:29.447' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 55, CAST(N'2025-05-06T09:18:12.877' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 56, CAST(N'2025-05-06T09:51:14.657' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 57, CAST(N'2025-05-06T09:51:50.680' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 58, CAST(N'2025-05-06T09:56:23.600' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 59, CAST(N'2025-05-06T09:59:33.737' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 60, CAST(N'2025-05-06T10:36:00.917' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 61, CAST(N'2025-05-06T10:45:00.063' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 25, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 62, CAST(N'2025-05-06T11:02:33.427' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 63, CAST(N'2025-05-06T16:46:11.033' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 64, CAST(N'2025-05-06T16:48:30.037' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 25, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 65, CAST(N'2025-05-06T16:56:12.123' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 66, CAST(N'2025-05-06T16:58:46.690' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 67, CAST(N'2025-05-06T17:01:11.187' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 68, CAST(N'2025-05-06T17:53:21.390' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 69, CAST(N'2025-05-06T17:54:41.790' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 70, CAST(N'2025-05-06T18:04:12.753' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 71, CAST(N'2025-05-06T18:05:03.183' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 72, CAST(N'2025-05-06T18:05:28.823' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 73, CAST(N'2025-05-06T18:06:47.153' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 74, CAST(N'2025-05-06T18:08:44.833' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 75, CAST(N'2025-05-06T18:09:25.347' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 76, CAST(N'2025-05-06T18:11:43.390' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 77, CAST(N'2025-05-06T18:12:18.850' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 78, CAST(N'2025-05-06T18:13:06.773' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 79, CAST(N'2025-05-06T23:27:08.093' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 2, 80, CAST(N'2025-05-06T23:33:23.247' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 23, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 85, CAST(N'2025-05-06T23:45:41.863' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 25, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 86, CAST(N'2025-05-06T23:47:18.903' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 87, CAST(N'2025-05-06T23:48:56.893' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 88, CAST(N'2025-05-06T23:49:36.850' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 89, CAST(N'2025-05-06T23:52:37.220' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 3, 90, CAST(N'2025-05-06T23:53:01.257' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 91, CAST(N'2025-05-06T23:53:59.490' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 92, CAST(N'2025-05-06T23:54:15.930' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 5, 93, CAST(N'2025-05-07T00:05:43.373' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 5, 94, CAST(N'2025-05-07T00:05:58.583' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 26, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 95, CAST(N'2025-05-07T00:15:24.520' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 96, CAST(N'2025-05-07T00:15:43.557' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 97, CAST(N'2025-05-07T00:55:34.577' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 102, CAST(N'2025-05-07T01:01:12.323' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 24, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":""},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 103, CAST(N'2025-05-07T01:02:55.660' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 24, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":""},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 104, CAST(N'2025-05-07T01:05:29.007' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (1, 4, 110, CAST(N'2025-05-07T01:08:05.997' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 24, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":""},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 111, CAST(N'2025-05-07T19:59:33.720' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 112, CAST(N'2025-05-07T20:28:15.950' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 113, CAST(N'2025-05-07T22:20:11.460' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 114, CAST(N'2025-05-07T22:54:55.453' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 115, CAST(N'2025-05-07T23:19:19.693' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 116, CAST(N'2025-05-07T23:20:21.037' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 117, CAST(N'2025-05-07T23:48:32.570' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 118, CAST(N'2025-05-08T02:13:10.947' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 119, CAST(N'2025-05-08T02:24:58.350' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 120, CAST(N'2025-05-08T02:41:18.907' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 121, CAST(N'2025-05-08T02:45:07.527' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Chrome v135.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":"http://localhost:5237/api-docs/index.html?urls.primaryName=V1"},{"Key":"UserAgent","Value":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 122, CAST(N'2025-05-08T02:51:24.347' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 123, CAST(N'2025-05-08T03:30:13.140' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 124, CAST(N'2025-05-08T03:45:24.437' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"Key":"Endpoint","Value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"Key":"Referer","Value":""},{"Key":"UserAgent","Value":"PostmanRuntime/7.43.4"},{"Key":"Authorization","Value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 125, CAST(N'2025-05-08T03:53:18.087' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 126, CAST(N'2025-05-08T03:54:02.760' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Chrome v135.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":"http://localhost:5237/api-docs/index.html?urls.primaryName=V1"},{"key":"UserAgent","value":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 127, CAST(N'2025-05-08T04:53:42.723' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 128, CAST(N'2025-05-08T08:36:50.667' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 129, CAST(N'2025-05-09T19:02:48.790' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 130, CAST(N'2025-05-09T23:41:26.427' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 131, CAST(N'2025-05-10T01:02:57.683' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 132, CAST(N'2025-05-10T01:10:40.340' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.24,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 133, CAST(N'2025-05-12T21:11:58.473' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.23,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [RequestEndpoint], [AddtionalInformation]) VALUES (2, 6, 134, CAST(N'2025-05-13T00:18:02.477' AS DateTime), N'VITO-TORRES-LAPTOP', N'Desktop', 27, N'192.168.1.23,172.27.144.1,172.25.128.1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'[{"key":"Endpoint","value":"HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\u003E TokenAync"},{"key":"Referer","value":""},{"key":"UserAgent","value":"PostmanRuntime/7.43.4"},{"key":"Authorization","value":""}]')
GO
SET IDENTITY_INSERT [dbo].[ActivityLogs] OFF
GO
INSERT [dbo].[Applications] ([Id], [NameTranslationKey], [ApplicationClient], [ApplicationSecret], [CreationDate], [CreatedByUserFk], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, N'Application_VitoTransverse', N'879ffb6a-4124-41c3-bd24-76c3b2f96109', N'96471380-f3f3-4120-9e25-a4e5c9701c23', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Applications] ([Id], [NameTranslationKey], [ApplicationClient], [ApplicationSecret], [CreationDate], [CreatedByUserFk], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, N'Application_VitoRealState', N'eb2d4ffc-dc34-435f-8983-ecd42481143f', N'ce09510a-7c12-4121-9f8b-ca0506ca302c', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (1, N'dbo', N'ActivityLogs', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (2, N'dbo', N'Applications', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (3, N'dbo', N'AuditEntities', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (4, N'dbo', N'AuditRecords', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (5, N'dbo', N'Companies', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (6, N'dbo', N'CompanyEntityAudits', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (7, N'dbo', N'CompanyMembershipPermissions', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (8, N'dbo', N'CompanyMemberships', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (9, N'dbo', N'Components', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (10, N'dbo', N'Countries', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (11, N'dbo', N'Cultures', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (12, N'dbo', N'CultureTranslations', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (13, N'dbo', N'GeneralTypeGroups', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (14, N'dbo', N'GeneralTypeItems', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (15, N'dbo', N'Languages', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (16, N'dbo', N'MembershipTypes', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (17, N'dbo', N'MembersipPriceHistory', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (18, N'dbo', N'Modules', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (19, N'dbo', N'Notifications', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (20, N'dbo', N'NotificationTemplates', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (21, N'dbo', N'Pages', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (22, N'dbo', N'RolePermissions', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (23, N'dbo', N'Roles', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (24, N'dbo', N'Sequences', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (25, N'dbo', N'sysdiagrams', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (26, N'dbo', N'UserRoles', 1, 0)
GO
INSERT [dbo].[AuditEntities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (27, N'dbo', N'Users', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[AuditRecords] ON 
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 6, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"LastAccess","Value":"8/05/2025 2:15:36\u202Fa.\u00A0m.==\u003E8/05/2025 2:24:10\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T02:24:58.057' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 7, 6, 1, 35, N'119', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"CompanyFk","Value":"2"},{"Key":"UserFk","Value":"6"},{"Key":"TraceId","Value":"119"},{"Key":"EventDate","Value":"8/05/2025 2:24:58\u202Fa.\u00A0m."},{"Key":"DeviceName","Value":"VITO-TORRES-LAPTOP"},{"Key":"DeviceType","Value":"Desktop"},{"Key":"ActionTypeFk","Value":"27"},{"Key":"IpAddress","Value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"Key":"Browser","Value":"Others v0.0"},{"Key":"Platform","Value":"Others v0.0 [Others]"},{"Key":"Engine","Value":"Others"},{"Key":"CultureId","Value":"es-CO"},{"Key":"RequestEndpoint","Value":"/api/Oauth2/v1/Token"},{"Key":"AddtionalInformation","Value":"[{\u0022Key\u0022:\u0022Endpoint\u0022,\u0022Value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022Key\u0022:\u0022Referer\u0022,\u0022Value\u0022:\u0022\u0022},{\u0022Key\u0022:\u0022UserAgent\u0022,\u0022Value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022Key\u0022:\u0022Authorization\u0022,\u0022Value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T02:25:36.597' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 8, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"LastAccess","Value":"8/05/2025 2:37:43\u202Fa.\u00A0m.==\u003E8/05/2025 2:41:10\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T02:41:18.590' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 9, 6, 1, 35, N'120', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"CompanyFk","Value":"2"},{"Key":"UserFk","Value":"6"},{"Key":"TraceId","Value":"120"},{"Key":"EventDate","Value":"8/05/2025 2:41:18\u202Fa.\u00A0m."},{"Key":"DeviceName","Value":"VITO-TORRES-LAPTOP"},{"Key":"DeviceType","Value":"Desktop"},{"Key":"ActionTypeFk","Value":"27"},{"Key":"IpAddress","Value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"Key":"Browser","Value":"Others v0.0"},{"Key":"Platform","Value":"Others v0.0 [Others]"},{"Key":"Engine","Value":"Others"},{"Key":"CultureId","Value":"es-CO"},{"Key":"RequestEndpoint","Value":"/api/Oauth2/v1/Token"},{"Key":"AddtionalInformation","Value":"[{\u0022Key\u0022:\u0022Endpoint\u0022,\u0022Value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022Key\u0022:\u0022Referer\u0022,\u0022Value\u0022:\u0022\u0022},{\u0022Key\u0022:\u0022UserAgent\u0022,\u0022Value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022Key\u0022:\u0022Authorization\u0022,\u0022Value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T02:42:12.410' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 10, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Chrome v135.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'[{"Key":"LastAccess","Value":"Before= 8/05/2025 2:41:10\u202Fa.\u00A0m.| After=8/05/2025 2:45:04\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T02:45:07.513' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 11, 6, 1, 35, N'121', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Chrome v135.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'[{"Key":"CompanyFk","Value":"2"},{"Key":"UserFk","Value":"6"},{"Key":"TraceId","Value":"121"},{"Key":"EventDate","Value":"8/05/2025 2:45:07\u202Fa.\u00A0m."},{"Key":"DeviceName","Value":"VITO-TORRES-LAPTOP"},{"Key":"DeviceType","Value":"Desktop"},{"Key":"ActionTypeFk","Value":"27"},{"Key":"IpAddress","Value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"Key":"Browser","Value":"Chrome v135.0.0.0"},{"Key":"Platform","Value":"Windows v10.0 [x64]"},{"Key":"Engine","Value":"Blink"},{"Key":"CultureId","Value":"es-CO"},{"Key":"RequestEndpoint","Value":"/api/Oauth2/v1/Token"},{"Key":"AddtionalInformation","Value":"[{\u0022Key\u0022:\u0022Endpoint\u0022,\u0022Value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022Key\u0022:\u0022Referer\u0022,\u0022Value\u0022:\u0022http://localhost:5237/api-docs/index.html?urls.primaryName=V1\u0022},{\u0022Key\u0022:\u0022UserAgent\u0022,\u0022Value\u0022:\u0022Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36\u0022},{\u0022Key\u0022:\u0022Authorization\u0022,\u0022Value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T02:45:10.553' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 12, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[]', CAST(N'2025-05-08T02:51:24.033' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 13, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"LastAccess","Value":"Before= 8/05/2025 2:48:50\u202Fa.\u00A0m. | After=8/05/2025 3:30:09\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T03:30:12.803' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 14, 6, 1, 35, N'123', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"CompanyFk","Value":"2"},{"Key":"UserFk","Value":"6"},{"Key":"TraceId","Value":"123"},{"Key":"EventDate","Value":"8/05/2025 3:30:13\u202Fa.\u00A0m."},{"Key":"DeviceName","Value":"VITO-TORRES-LAPTOP"},{"Key":"DeviceType","Value":"Desktop"},{"Key":"ActionTypeFk","Value":"27"},{"Key":"IpAddress","Value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"Key":"Browser","Value":"Others v0.0"},{"Key":"Platform","Value":"Others v0.0 [Others]"},{"Key":"Engine","Value":"Others"},{"Key":"CultureId","Value":"es-CO"},{"Key":"RequestEndpoint","Value":"/api/Oauth2/v1/Token"},{"Key":"AddtionalInformation","Value":"[{\u0022Key\u0022:\u0022Endpoint\u0022,\u0022Value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022Key\u0022:\u0022Referer\u0022,\u0022Value\u0022:\u0022\u0022},{\u0022Key\u0022:\u0022UserAgent\u0022,\u0022Value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022Key\u0022:\u0022Authorization\u0022,\u0022Value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T03:30:16.190' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 15, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"LastAccess","Value":"Before= 8/05/2025 3:30:09\u202Fa.\u00A0m. | After=8/05/2025 3:45:18\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T03:45:24.123' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 16, 6, 1, 35, N'124', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"Key":"CompanyFk","Value":"2"},{"Key":"UserFk","Value":"6"},{"Key":"TraceId","Value":"124"},{"Key":"EventDate","Value":"8/05/2025 3:45:24\u202Fa.\u00A0m."},{"Key":"DeviceName","Value":"VITO-TORRES-LAPTOP"},{"Key":"DeviceType","Value":"Desktop"},{"Key":"ActionTypeFk","Value":"27"},{"Key":"IpAddress","Value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"Key":"Browser","Value":"Others v0.0"},{"Key":"Platform","Value":"Others v0.0 [Others]"},{"Key":"Engine","Value":"Others"},{"Key":"CultureId","Value":"es-CO"},{"Key":"RequestEndpoint","Value":"/api/Oauth2/v1/Token"},{"Key":"AddtionalInformation","Value":"[{\u0022Key\u0022:\u0022Endpoint\u0022,\u0022Value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022Key\u0022:\u0022Referer\u0022,\u0022Value\u0022:\u0022\u0022},{\u0022Key\u0022:\u0022UserAgent\u0022,\u0022Value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022Key\u0022:\u0022Authorization\u0022,\u0022Value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T03:45:27.633' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 17, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 8/05/2025 3:45:18\u202Fa.\u00A0m. | After=8/05/2025 3:53:13\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T03:53:17.787' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 18, 6, 1, 35, N'125', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"125"},{"key":"EventDate","value":"8/05/2025 3:53:18\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T03:53:21.020' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 19, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Chrome v135.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'[{"key":"LastAccess","value":"Before= 8/05/2025 3:53:13\u202Fa.\u00A0m. | After=8/05/2025 3:53:59\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T03:54:02.730' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 20, 6, 1, 35, N'126', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Chrome v135.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"126"},{"key":"EventDate","value":"8/05/2025 3:54:02\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Chrome v135.0.0.0"},{"key":"Platform","value":"Windows v10.0 [x64]"},{"key":"Engine","value":"Blink"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022http://localhost:5237/api-docs/index.html?urls.primaryName=V1\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T04:53:15.253' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 21, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 8/05/2025 3:53:59\u202Fa.\u00A0m. | After=8/05/2025 4:53:39\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T04:53:42.703' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 22, 6, 1, 35, N'127', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"127"},{"key":"EventDate","value":"8/05/2025 4:53:42\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T04:53:44.967' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 23, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 8/05/2025 4:53:39\u202Fa.\u00A0m. | After=8/05/2025 8:36:47\u202Fa.\u00A0m."}]', CAST(N'2025-05-08T08:36:50.367' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 24, 6, 1, 35, N'128', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"128"},{"key":"EventDate","value":"8/05/2025 8:36:50\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-08T08:36:51.713' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 25, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 8/05/2025 8:36:47\u202Fa.\u00A0m. | After=9/05/2025 7:02:46\u202Fp.\u00A0m."}]', CAST(N'2025-05-09T19:02:48.510' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 26, 6, 1, 35, N'129', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"129"},{"key":"EventDate","value":"9/05/2025 7:02:48\u202Fp.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-09T19:02:49.653' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 27, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 9/05/2025 7:02:46\u202Fp.\u00A0m. | After=9/05/2025 11:41:24\u202Fp.\u00A0m."}]', CAST(N'2025-05-09T23:41:26.117' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 28, 6, 1, 35, N'130', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"130"},{"key":"EventDate","value":"9/05/2025 11:41:26\u202Fp.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-09T23:41:26.537' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 29, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 9/05/2025 11:41:24\u202Fp.\u00A0m. | After=10/05/2025 1:02:56\u202Fa.\u00A0m."}]', CAST(N'2025-05-10T01:02:57.407' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 30, 6, 1, 35, N'131', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"131"},{"key":"EventDate","value":"10/05/2025 1:02:57\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-10T01:02:57.767' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 31, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 10/05/2025 1:02:56\u202Fa.\u00A0m. | After=10/05/2025 1:10:40\u202Fa.\u00A0m."}]', CAST(N'2025-05-10T01:10:40.333' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 32, 6, 1, 35, N'132', N'VITO-TORRES-LAPTOP', N'192.168.1.24,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"132"},{"key":"EventDate","value":"10/05/2025 1:10:40\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.24,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-10T01:10:40.357' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 33, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.23,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 10/05/2025 1:10:40\u202Fa.\u00A0m. | After=12/05/2025 9:11:58\u202Fp.\u00A0m."}]', CAST(N'2025-05-12T21:11:58.460' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 34, 6, 1, 35, N'133', N'VITO-TORRES-LAPTOP', N'192.168.1.23,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"133"},{"key":"EventDate","value":"12/05/2025 9:11:58\u202Fp.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.23,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-12T21:11:58.490' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 35, 6, 27, 37, N'6', N'VITO-TORRES-LAPTOP', N'192.168.1.23,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"LastAccess","value":"Before= 12/05/2025 9:11:58\u202Fp.\u00A0m. | After=13/05/2025 12:18:01\u202Fa.\u00A0m."}]', CAST(N'2025-05-13T00:18:02.227' AS DateTime))
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [AuditEntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [AuditInfoJson], [CreationDate]) VALUES (2, 36, 6, 1, 35, N'134', N'VITO-TORRES-LAPTOP', N'192.168.1.23,172.27.144.1,172.25.128.1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'[{"key":"CompanyFk","value":"2"},{"key":"UserFk","value":"6"},{"key":"TraceId","value":"134"},{"key":"EventDate","value":"13/05/2025 12:18:02\u202Fa.\u00A0m."},{"key":"DeviceName","value":"VITO-TORRES-LAPTOP"},{"key":"DeviceType","value":"Desktop"},{"key":"ActionTypeFk","value":"27"},{"key":"IpAddress","value":"192.168.1.23,172.27.144.1,172.25.128.1"},{"key":"Browser","value":"Others v0.0"},{"key":"Platform","value":"Others v0.0 [Others]"},{"key":"Engine","value":"Others"},{"key":"CultureId","value":"es-CO"},{"key":"RequestEndpoint","value":"/api/Oauth2/v1/Token"},{"key":"AddtionalInformation","value":"[{\u0022key\u0022:\u0022Endpoint\u0022,\u0022value\u0022:\u0022HTTP: POST api/Oauth2/v{apiVersion:apiVersion}/Token =\\u003E TokenAync\u0022},{\u0022key\u0022:\u0022Referer\u0022,\u0022value\u0022:\u0022\u0022},{\u0022key\u0022:\u0022UserAgent\u0022,\u0022value\u0022:\u0022PostmanRuntime/7.43.4\u0022},{\u0022key\u0022:\u0022Authorization\u0022,\u0022value\u0022:\u0022\u0022}]"}]', CAST(N'2025-05-13T00:18:02.560' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AuditRecords] OFF
GO
INSERT [dbo].[Companies] ([Id], [NameTranslationKey], [CompanyClient], [CompanySecret], [CreationDate], [CreatedByUserFk], [Subdomain], [Email], [DefaultCultureFk], [CountryFk], [IsSystemCompany], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, N'Company_VitoTorresSoft', N'55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8', N'ba3a564f-946a-4b07-b44c-bf8ea21e808c', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, N'vito.torres.soft', N'eeatg844@hotmail.com', N'es-CO', N'CO', 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Companies] ([Id], [NameTranslationKey], [CompanyClient], [CompanySecret], [CreationDate], [CreatedByUserFk], [Subdomain], [Email], [DefaultCultureFk], [CountryFk], [IsSystemCompany], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, N'Company_ProyectosLasTorresSAS', N'c5bcea98-2974-4d43-8110-28d402cf5ce2', N'8010e5e4-5f3f-4cd9-9d4c-b9babbecc740', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, N'proyectos-las-torres', N'proyectos-las-torres@hotmail.com', N'es-CO', N'CO', 0, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 1, 1, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 2, 1, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 3, 1, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 4, 1, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 5, 1, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 6, 1, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 7, 2, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 8, 2, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 9, 2, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 10, 5, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 11, 5, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 12, 5, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 13, 27, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 14, 27, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 15, 27, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 16, 2, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 17, 2, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 18, 2, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 19, 5, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 20, 5, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 21, 5, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 22, 27, 35, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 23, 27, 36, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [AuditEntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 24, 27, 37, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 1, 1, 1, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 2, 2, 21, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 3, 2, 22, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 4, 2, 23, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 5, 2, 24, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 6, 2, 25, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 7, 2, 26, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 8, 2, 27, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 9, 3, 31, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 10, 3, 32, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 11, 3, 33, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 12, 3, 34, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 13, 3, 35, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 14, 3, 36, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 15, 4, 41, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 16, 4, 42, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 17, 5, 51, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 18, 5, 52, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 19, 5, 53, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 20, 5, 54, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 21, 5, 55, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 22, 5, 56, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 23, 5, 57, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 24, 6, 61, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 25, 7, 71, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 26, 7, 72, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 27, 8, 81, NULL, NULL)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, 1, 1, 5, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, 1, 2, 5, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (3, 2, 1, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (4, 2, 2, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Components] ([ApplicationFk], [PageFk], [Id], [NameTranslationKey], [ObjectId], [ObjectName], [ObjectPropertyName], [DefaultPropertyValue], [PositionIndex]) VALUES (1, 21, 1, N'VitoTransverse_ModuleMembership_PageApplications', N'NewApplicationButton', N'NewApplicationButton', N'enabled', N'false', 0)
GO
INSERT [dbo].[Components] ([ApplicationFk], [PageFk], [Id], [NameTranslationKey], [ObjectId], [ObjectName], [ObjectPropertyName], [DefaultPropertyValue], [PositionIndex]) VALUES (1, 22, 2, N'VitoTransverse_ModuleMembership_PageCompanies', N'NewCompanyButton', N'NewCompanyButton', N'enabled', N'false', 0)
GO
INSERT [dbo].[Countries] ([Id], [NameTranslationKey], [UtcHoursDifference]) VALUES (N'CO', N'Country_Colombia', -5)
GO
INSERT [dbo].[Countries] ([Id], [NameTranslationKey], [UtcHoursDifference]) VALUES (N'US', N'Country_UnitedStates', -5)
GO
INSERT [dbo].[Cultures] ([Id], [NameTranslationKey], [CountryFk], [LanguageFk], [IsEnabled]) VALUES (N'en-US', N'English_US', N'US', N'en', 1)
GO
INSERT [dbo].[Cultures] ([Id], [NameTranslationKey], [CountryFk], [LanguageFk], [IsEnabled]) VALUES (N'es-CO', N'Spanish_CO', N'CO', N'es', 1)
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'ActionType_LoginSuccessByClientCredentials', N'Login Successfully')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'DocumentTypeList_BornRegistry', N'Born Registry')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'DocumentTypeList_CompanyId', N'Company DNI')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'DocumentTypeList_DNI', N'DNI')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'DocumentTypeList_ForeingDNI', N'Foreign DNI')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'DocumentTypeList_Passport', N'Passport')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'DocumentTypeList_Undefined', N'No Specified')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'English_US', N'English [United States]')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'GenderList_Female', N'Female')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'GenderList_Male', N'Male')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'ListItemGroup_DocumentTypeList', N'Document Types List')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'ListItemGroup_GenderList', N'Peron''s Gender List')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'Spanish_CO', N'Español [Colombia]')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'TranslationKey_MessageNotFound', N'Culture: [{0}] ~ Message: ({1}) - Not Found.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'Validator_InternalServerError', N'Internal server error ocurred.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'Validator_NotEmpty', N'''{0}'' should have a valid value. X')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'Validator_NotImplemented', N'''{0}'' value is not in use X')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'Validator_NotNull', N'''{0}'' should no to be empty. X')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'en-US', N'Validator_Title', N'One or more validation errors occurred.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'ActionType_LoginSuccessByClientCredentials', N'Inicio se sesion satisfactorio.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'DocumentTypeList_BornRegistry', N'Registro Civil')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'DocumentTypeList_CompanyId', N'Nit')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'DocumentTypeList_DNI', N'Cedula de ciudadania')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'DocumentTypeList_ForeingDNI', N'Cedula de extrangeria')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'DocumentTypeList_Passport', N'Pasaporte')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'DocumentTypeList_Undefined', N'No Especificado')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'English_US', N'English [United States]')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'GenderList_Female', N'Femenino')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'GenderList_Male', N'Masculino')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'ListItemGroup_DocumentTypeList', N'Listado Tipos de Documento')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'ListItemGroup_GenderList', N'Listado Generos de Personas')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'Spanish_CO', N'Español [Colombia]')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'TranslationKey_MessageNotFound', N'Cultura: [{0}] ~ Mensaje: ({1}) - No Encontrado.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'Validator_InternalServerError', N'Ha ocurrido un error interno en el servidor.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'Validator_NotEmpty', N'''{0}'' deberia tener un valor valido. X')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'Validator_NotImplemented', N'''{0}'' para el valor indicado no esta en habilitado. X')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'Validator_NotNull', N'''{0}'' no deberia estar vacío. X')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (1, N'es-CO', N'Validator_Title', N'Uno o mas errores de validación han ocurrido.')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (2, N'en-US', N'ActionType_LoginSuccessByClientCredentials', N'Login Successfully')
GO
INSERT [dbo].[CultureTranslations] ([ApplicationFk], [CultureFk], [TranslationKey], [TranslationValue]) VALUES (2, N'es-CO', N'ActionType_LoginSuccessByClientCredentials', N'Inicio se sesion satisfactorio.')
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (1, N'GeneralType_NotificationType', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (2, N'GeneralType_DocumentType', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (3, N'GeneralType_GenderList', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (4, N'GeneralType_OAuthActionType', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (5, N'GeneralType_LocationType', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (6, N'GeneralType_SequenceType', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (7, N'GeneralType_EntityAuditType', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (1, 1, 1, N'NotificationType_Email', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (1, 2, 2, N'NotificationType_SMS', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 1, 3, N'DocumentType_BornRegistry', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 2, 4, N'DocumentType_DNI', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 3, 5, N'DocumentType_ForeingDNI', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 4, 6, N'DocumentType_CompanyId', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 5, 7, N'DocumentType_Passport', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (3, 1, 8, N'GenderList_Undefined', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (3, 2, 9, N'GenderList_Female', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (3, 3, 10, N'GenderList_Male', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 1, 11, N'OAuthActionType_Undefined', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 2, 12, N'OAuthActionType_CreateNewApplication', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 3, 13, N'OAuthActionType_CreateNewCompany', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 4, 14, N'OAuthActionType_CreateNewPerson', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 5, 15, N'OAuthActionType_CreateNewUser', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 6, 16, N'OAuthActionType_SendActivationEmail', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 7, 17, N'OAuthActionType_ActivateUser', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 8, 18, N'OAuthActionType_LoginFail_CompanyNotFound', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 9, 19, N'OAuthActionType_LoginFail_CompanySecretInvalid', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 14, 20, N'OAuthActionType_LoginFail_CompanyMembershipDoesNotExist', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 10, 21, N'OAuthActionType_LoginFail_ApplicationNoFound', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 11, 22, N'OAuthActionType_LoginFail_ApplicationSecretInvalid', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 12, 23, N'OAuthActionType_LoginFail_UserNotFound', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 13, 24, N'OAuthActionType_LoginFail_UserSecretInvalid', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 18, 25, N'OAuthActionType_LoginFail_UserUnauthorized', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 15, 26, N'OAuthActionType_LoginSuccessByAuthorizationCode', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 16, 27, N'OAuthActionType_LoginSuccessByClientCredentials', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 17, 28, N'OAuthActionType_ChangeUserPassword', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 18, 29, N'OAuthActionType_Logoff', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (5, 1, 30, N'LocationType_State', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (5, 2, 31, N'LocationType_City', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (5, 3, 32, N'LocationType_Neighborhood', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (6, 1, 33, N'SequenceType_RoleName', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 1, 34, N'EntityAuditType_Read', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 2, 35, N'EntityAuditType_AddRow', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 3, 36, N'EntityAuditType_DeleteRow', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 4, 37, N'EntityAuditType_UpdateRow', 1)
GO
INSERT [dbo].[Languages] ([Id], [NameTranslationKey]) VALUES (N'en', N'Language_English')
GO
INSERT [dbo].[Languages] ([Id], [NameTranslationKey]) VALUES (N'es', N'Language_Spanish')
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, N'Membership_Free', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, N'Membership_Standard', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (3, N'Membership_Enterprise', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (4, N'Membership_Prime', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (5, N'Membership_Owner', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, N'VitoTransverse_ModuleApi', 1, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, N'VitoTransverse_ModuleMembership', 2, 1, 1, 0)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, N'VitoTransverse_ModuleSecurity', 3, 1, 1, 0)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, N'VitoTransverse_ModuleLogging', 4, 1, 1, 0)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, N'VitoTransverse_ModuleSettings', 5, 1, 1, 0)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 6, N'VitoRealState_ModuleApi', 6, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 7, N'VitoRealState_ModuleProjectProperty', 7, 1, 1, 0)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 8, N'VitoRealState_ModulePersons', 8, 1, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[NotificationTemplates] ON 
GO
INSERT [dbo].[NotificationTemplates] ([Id], [NotificationTemplateGroupId], [CultureFk], [Name], [SubjectTemplateText], [MessageTemplateText], [IsHtml]) VALUES (2, 1, N'en-US', N'EMAIL_USER_ACTIVATION', N'Activación Cuenta Vito.ePOS - email: {{EMAIL}}', N'Hola <br/> Bienvenid@ a e-POS <br/> Correo de activaciion para:{{EMAIL}}<br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''{{ACTIVATION_LINK}}''> Activar Cuenta<a/>', 1)
GO
INSERT [dbo].[NotificationTemplates] ([Id], [NotificationTemplateGroupId], [CultureFk], [Name], [SubjectTemplateText], [MessageTemplateText], [IsHtml]) VALUES (1, 1, N'es-CO', N'EMAIL_USER_ACTIVATION', N'Activación Cuenta Vito.ePOS - email: {{EMAIL}}', N'Hola <br/> Bienvenid@ a e-POS <br/> Correo de activaciion para:{{EMAIL}}<br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''{{ACTIVATION_LINK}}''> Activar Cuenta<a/>', 1)
GO
SET IDENTITY_INSERT [dbo].[NotificationTemplates] OFF
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 1, NULL, N'VitoTransverse_ApiOauth2V1', N'/api/Oauth2/v1/Token', 1, 0, 1)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 21, 1, N'VitoTransverse_ModuleMembership_PageApplicationsLIst', N'pages/memerbership/ApplicationList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 22, 2, N'VitoTransverse_ModuleMembership_PageCompaniesList', N'pages/memerbership/CompaniesList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 23, 3, N'VitoTransverse_ModuleMembership_PageMembershipTypeList', N'pages/memerbership/MembershipTypeList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 24, 4, N'VitoTransverse_ModuleMembership_PageCompanyMembershipsList', N'pages/memerbership/CompanymembershipList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 25, 5, N'VitoTransverse_ModuleMembership_PageMembershipPriceHistoryLIst', N'pages/memerbership/membershipPiceHistoryList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 26, 6, N'VitoTransverse_ModuleMembership_PageCompanyMembershipPayments', N'pages/memerbership/membershipPayments', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 27, 7, N'VitoTransverse_ModuleMembership_PageCompanyInfo', N'pages/memerbership/CompanyInfo', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 31, 1, N'VitoTransverse_ModuleSecurity_PageModulesList', N'pages/security/ModuleLIst', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 32, 2, N'VitoTransverse_ModuleSecurity_PagePagesList', N'pages/security/PageList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 33, 3, N'VitoTransverse_ModuleSecurity_PageComponentsList', N'pages/security/ComponentList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 34, 4, N'VitoTransverse_ModuleSecurity_PageRolesList', N'pages/security/RoleList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 35, 5, N'VitoTransverse_ModuleSecurity_PageRolePermissionsList', N'pages/security/RolePermisionList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 36, 6, N'VitoTransverse_ModuleSecurity_PageUsersList', N'pages/security/UserList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 41, 1, N'VitoTransverse_ModuleLogging_PageActivityLogsList', N'pages/settings/ActivityLogList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 42, 2, N'VitoTransverse_ModuleLogging_PageErrosLogsList', N'pages/settings/ErrorLogList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 51, 1, N'VitoTransverse_ModuleSettings_PageCountriesList', N'pages/settings/CountriesList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 52, 2, N'VitoTransverse_ModuleSettings_PageLanguagesList', N'pages/settings/LanguageList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 53, 3, N'VitoTransverse_ModuleSettings_PageCulturesList', N'pages/settings/CultureList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 54, 4, N'VitoTransverse_ModuleSettings_PageCultureTranslationsList', N'pages/settings/CultureTranslationsList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 55, 5, N'VitoTransverse_ModuleSettings_PageNotificationsTemplateList', N'pages/settings/notificationTemplateList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 56, 6, N'VitoTransverse_ModuleSettings_PageNotificationsList', N'pages/settings/notificationList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 57, 7, N'VitoTransverse_ModuleSettings_PageGeneralTypesGroupList', N'pages/settings/GeneralTypeGroup', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (2, 6, 61, NULL, N'VitoRealState_ApiRealState', N'/api/Projects/v1/GetAll', 1, 0, 1)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (2, 7, 71, 1, N'VitoRealState_ModuleProjectProperty_PagePropertiesList', N'pages/projects/PagePropertiesList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (2, 7, 72, 2, N'VitoRealState_ModuleProjectProperty_PageProjectsList', N'pages/projects/PageProjectsList', 1, 1, 0)
GO
INSERT [dbo].[Pages] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [PageUrl], [IsActive], [IsVisible], [IsApi]) VALUES (2, 8, 81, 1, N'VitoRealState_ModulePersons_PagePersonList', N'pages/persons/PagePersonList', 1, 1, 0)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 1, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 2, 2, 21, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 3, 2, 22, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 4, 2, 23, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 5, 2, 24, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 6, 2, 25, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 7, 2, 26, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 8, 2, 27, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 9, 3, 31, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 10, 3, 32, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 11, 3, 33, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 12, 3, 34, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 13, 3, 35, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 14, 3, 36, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 15, 4, 41, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 16, 4, 42, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 17, 5, 51, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 18, 5, 52, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 19, 5, 53, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 20, 5, 54, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 21, 5, 55, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 22, 5, 56, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (4, 1, 1, 23, 5, 57, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (5, 1, 2, 24, 6, 61, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (5, 1, 2, 25, 7, 71, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (5, 1, 2, 26, 7, 72, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (5, 1, 2, 27, 8, 81, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 28, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 1, 29, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (3, 1, 1, 30, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (6, 1, 2, 31, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (7, 1, 1, 32, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (8, 1, 2, 33, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (9, 1, 1, 34, 1, 1, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [PageFk], [ComponentFk], [PropertyValue]) VALUES (10, 1, 2, 35, 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 1, N'Vito-Transverse_Role_System', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 2, N'Vito-Transverse_Role_UnknownUser', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 3, N'Vito-Transverse_Role_ApiUser', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 4, N'Vito-Transverse_Role_0000000001', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 2, 5, N'Vito-RealState_Role_0000000001', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 2, 6, N'Vito-RealState_Role_ApiUser', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 1, 7, N'LaTorres-Transverse_Role_ApiUser', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 2, 8, N'LaTorres-RealState_Role_ApiUser', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 1, 9, N'LaTorres-Transverse_Role_0000000001', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 2, 10, N'LaTorres-RealState_Role_0000000001', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (1, 1, 33, 1, N'Vito-Transverse_Role_', 1, N'0000000000')
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (1, 2, 33, 2, N'Vito-RealState_Role_', 1, N'0000000000')
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (2, 1, 33, 3, N'LaTorres-Transverse_Role_', 1, N'0000000000')
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (2, 2, 33, 4, N'LaTorres-Transverse_Role_', 1, N'0000000000')
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (1, 1, 1, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (1, 1, 2, 2, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (1, 1, 3, 3, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (1, 2, 3, 6, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (1, 1, 4, 4, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (1, 2, 4, 5, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (2, 1, 5, 7, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (2, 2, 5, 8, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (2, 1, 6, 9, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserRoles] ([CompanyFk], [ApplicationFk], [UserFk], [RoleFk], [CreatedDate], [CreatedByUserFk], [IsActive]) VALUES (2, 2, 6, 10, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'system', 1, N'System', N'User', N'system@vito-torres-soft.com                                                                         ', N'system', 0, 0, 0, NULL, 0, N'55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 0, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'unknown', 2, N'Unknown', N'User', N'unknown@vito-torres-soft.com                                                                        ', N'unknown-user', 0, 0, 0, NULL, 0, N'bf3e34a8-b74e-49e3-9100-86d32e35d46c', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'api-user', 3, N'API', N'User', N'api.user@vito-torres-soft.com                                                                       ', N'api-user', 1, 0, 0, CAST(N'2025-05-06T23:53:01.237' AS DateTime), 0, N'6f8a48ad-0045-4c0b-8117-f65a60ba384c', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'ever.torresg', 4, N'Ever Alonso', N'Torres Galeano', N'ever.torres.g@vito-torres-soft.com                                                                  ', N'123', 1, 0, 0, CAST(N'2025-05-07T01:08:05.913' AS DateTime), 0, N'1356de0a-e0ca-4049-8c77-703fbee4126b', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (2, N'api-user', 5, N'API', N'User', N'api.user@proyectos-las-torres.com                                                                   ', N'api-user', 1, 0, 0, CAST(N'2025-05-07T00:05:58.563' AS DateTime), 0, N'15ea4ef1-8fca-42f1-b25e-fc52be4e4712', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (2, N'edgar.torres', 6, N'Edgar', N'Torres Agudelo', N'edgar.torres.g@proyectos-las-torres.com                                                             ', N'456', 1, 0, 0, CAST(N'2025-05-13T00:18:01.770' AS DateTime), 0, N'414026c1-8bf2-41f2-97c2-c1a3b7f656b8', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_CompanyMembershipPermissions]    Script Date: 5/13/2025 8:46:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_CompanyMembershipPermissions] ON [dbo].[CompanyMembershipPermissions]
(
	[CompanyMembershipFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CompanyMemberships]    Script Date: 5/13/2025 8:46:18 PM ******/
ALTER TABLE [dbo].[CompanyMemberships] ADD  CONSTRAINT [IX_CompanyMemberships] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CultureTranslations]    Script Date: 5/13/2025 8:46:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_CultureTranslations] ON [dbo].[CultureTranslations]
(
	[ApplicationFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_Identifier]  DEFAULT (newid()) FOR [ApplicationClient]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_Secret]  DEFAULT (newid()) FOR [ApplicationSecret]
GO
ALTER TABLE [dbo].[AuditEntities] ADD  CONSTRAINT [DF_AuditEntities_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AuditEntities] ADD  CONSTRAINT [DF_AuditEntities_IsSystemEntity]  DEFAULT ((0)) FOR [IsSystemEntity]
GO
ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_Identifier]  DEFAULT (newid()) FOR [CompanyClient]
GO
ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_Secret]  DEFAULT (newid()) FOR [CompanySecret]
GO
ALTER TABLE [dbo].[CompanyEntityAudits] ADD  CONSTRAINT [DF_CompanyEntityAudits_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CompanyMemberships] ADD  CONSTRAINT [DF_CompanyMemberships_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Cultures] ADD  CONSTRAINT [DF_Cultures_IsEnabled]  DEFAULT ((0)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[GeneralTypeGroups] ADD  CONSTRAINT [DF_ListItemGroup_IsSystemType]  DEFAULT ((1)) FOR [IsSystemType]
GO
ALTER TABLE [dbo].[GeneralTypeItems] ADD  CONSTRAINT [DF_ListItems_IsEnabled]  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[MembershipTypes] ADD  CONSTRAINT [DF_Memberships_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_IsEnabled]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_IsVisible]  DEFAULT ((0)) FOR [IsVisible]
GO
ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [DF_Modules_IsApi]  DEFAULT ((0)) FOR [IsApi]
GO
ALTER TABLE [dbo].[Notifications] ADD  CONSTRAINT [DF_NotificationTraces_IsSent]  DEFAULT ((0)) FOR [IsSent]
GO
ALTER TABLE [dbo].[Notifications] ADD  CONSTRAINT [DF_Notifications_IsHtml]  DEFAULT ((0)) FOR [IsHtml]
GO
ALTER TABLE [dbo].[NotificationTemplates] ADD  CONSTRAINT [DF_NotificationTemplates_IsHtml]  DEFAULT ((0)) FOR [IsHtml]
GO
ALTER TABLE [dbo].[Pages] ADD  CONSTRAINT [DF_PagesModules_IsEnabled]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_EmailValidated]  DEFAULT ((0)) FOR [EmailValidated]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RequirePasswordChange]  DEFAULT ((1)) FOR [RequirePasswordChange]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RetryCount]  DEFAULT ((0)) FOR [RetryCount]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ActivationEmailSent]  DEFAULT ((0)) FOR [ActivationEmailSent]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsLocked]  DEFAULT ((1)) FOR [IsLocked]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_ActivityLogs_ListItems] FOREIGN KEY([ActionTypeFk])
REFERENCES [dbo].[GeneralTypeItems] ([Id])
GO
ALTER TABLE [dbo].[ActivityLogs] CHECK CONSTRAINT [FK_ActivityLogs_ListItems]
GO
ALTER TABLE [dbo].[ActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_ActivityLogs_Users] FOREIGN KEY([UserFk])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ActivityLogs] CHECK CONSTRAINT [FK_ActivityLogs_Users]
GO
ALTER TABLE [dbo].[AuditRecords]  WITH CHECK ADD  CONSTRAINT [FK_AuditRecords_AuditEntities] FOREIGN KEY([AuditEntityFk])
REFERENCES [dbo].[AuditEntities] ([Id])
GO
ALTER TABLE [dbo].[AuditRecords] CHECK CONSTRAINT [FK_AuditRecords_AuditEntities]
GO
ALTER TABLE [dbo].[AuditRecords]  WITH CHECK ADD  CONSTRAINT [FK_AuditRecords_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[AuditRecords] CHECK CONSTRAINT [FK_AuditRecords_Companies]
GO
ALTER TABLE [dbo].[AuditRecords]  WITH CHECK ADD  CONSTRAINT [FK_AuditRecords_GeneralTypeItems] FOREIGN KEY([AuditTypeFk])
REFERENCES [dbo].[GeneralTypeItems] ([Id])
GO
ALTER TABLE [dbo].[AuditRecords] CHECK CONSTRAINT [FK_AuditRecords_GeneralTypeItems]
GO
ALTER TABLE [dbo].[AuditRecords]  WITH CHECK ADD  CONSTRAINT [FK_AuditRecords_Users] FOREIGN KEY([UserFk])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AuditRecords] CHECK CONSTRAINT [FK_AuditRecords_Users]
GO
ALTER TABLE [dbo].[Companies]  WITH CHECK ADD  CONSTRAINT [FK_Companies_Countries] FOREIGN KEY([CountryFk])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Companies] CHECK CONSTRAINT [FK_Companies_Countries]
GO
ALTER TABLE [dbo].[Companies]  WITH CHECK ADD  CONSTRAINT [FK_Companies_Cultures] FOREIGN KEY([DefaultCultureFk])
REFERENCES [dbo].[Cultures] ([Id])
GO
ALTER TABLE [dbo].[Companies] CHECK CONSTRAINT [FK_Companies_Cultures]
GO
ALTER TABLE [dbo].[CompanyEntityAudits]  WITH CHECK ADD  CONSTRAINT [FK_CompanyEntityAudits_AuditEntities] FOREIGN KEY([AuditEntityFk])
REFERENCES [dbo].[AuditEntities] ([Id])
GO
ALTER TABLE [dbo].[CompanyEntityAudits] CHECK CONSTRAINT [FK_CompanyEntityAudits_AuditEntities]
GO
ALTER TABLE [dbo].[CompanyEntityAudits]  WITH CHECK ADD  CONSTRAINT [FK_CompanyEntityAudits_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyEntityAudits] CHECK CONSTRAINT [FK_CompanyEntityAudits_Companies]
GO
ALTER TABLE [dbo].[CompanyEntityAudits]  WITH CHECK ADD  CONSTRAINT [FK_CompanyEntityAudits_GeneralTypeItems] FOREIGN KEY([AuditTypeFk])
REFERENCES [dbo].[GeneralTypeItems] ([Id])
GO
ALTER TABLE [dbo].[CompanyEntityAudits] CHECK CONSTRAINT [FK_CompanyEntityAudits_GeneralTypeItems]
GO
ALTER TABLE [dbo].[CompanyMembershipPermissions]  WITH CHECK ADD  CONSTRAINT [FK_CompanyMembershipPermissions_CompanyMemberships] FOREIGN KEY([CompanyMembershipFk])
REFERENCES [dbo].[CompanyMemberships] ([Id])
GO
ALTER TABLE [dbo].[CompanyMembershipPermissions] CHECK CONSTRAINT [FK_CompanyMembershipPermissions_CompanyMemberships]
GO
ALTER TABLE [dbo].[CompanyMemberships]  WITH CHECK ADD  CONSTRAINT [FK_CompanyMemberships_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[CompanyMemberships] CHECK CONSTRAINT [FK_CompanyMemberships_Applications]
GO
ALTER TABLE [dbo].[CompanyMemberships]  WITH CHECK ADD  CONSTRAINT [FK_CompanyMemberships_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyMemberships] CHECK CONSTRAINT [FK_CompanyMemberships_Companies]
GO
ALTER TABLE [dbo].[CompanyMemberships]  WITH CHECK ADD  CONSTRAINT [FK_CompanyMemberships_Memberships] FOREIGN KEY([MembershipTypeFk])
REFERENCES [dbo].[MembershipTypes] ([Id])
GO
ALTER TABLE [dbo].[CompanyMemberships] CHECK CONSTRAINT [FK_CompanyMemberships_Memberships]
GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD  CONSTRAINT [FK_Components_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Components] CHECK CONSTRAINT [FK_Components_Applications]
GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD  CONSTRAINT [FK_PageComponents_ModulePages] FOREIGN KEY([PageFk])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Components] CHECK CONSTRAINT [FK_PageComponents_ModulePages]
GO
ALTER TABLE [dbo].[Cultures]  WITH CHECK ADD  CONSTRAINT [FK_Cultures_Countries] FOREIGN KEY([CountryFk])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Cultures] CHECK CONSTRAINT [FK_Cultures_Countries]
GO
ALTER TABLE [dbo].[Cultures]  WITH CHECK ADD  CONSTRAINT [FK_Cultures_Languages] FOREIGN KEY([LanguageFk])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Cultures] CHECK CONSTRAINT [FK_Cultures_Languages]
GO
ALTER TABLE [dbo].[CultureTranslations]  WITH CHECK ADD  CONSTRAINT [FK_CultureTranslations_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[CultureTranslations] CHECK CONSTRAINT [FK_CultureTranslations_Applications]
GO
ALTER TABLE [dbo].[CultureTranslations]  WITH CHECK ADD  CONSTRAINT [FK_CultureTranslations_Cultures] FOREIGN KEY([CultureFk])
REFERENCES [dbo].[Cultures] ([Id])
GO
ALTER TABLE [dbo].[CultureTranslations] CHECK CONSTRAINT [FK_CultureTranslations_Cultures]
GO
ALTER TABLE [dbo].[GeneralTypeItems]  WITH CHECK ADD  CONSTRAINT [FK_ListItems_ListItemGroup] FOREIGN KEY([ListItemGroupFk])
REFERENCES [dbo].[GeneralTypeGroups] ([Id])
GO
ALTER TABLE [dbo].[GeneralTypeItems] CHECK CONSTRAINT [FK_ListItems_ListItemGroup]
GO
ALTER TABLE [dbo].[MembersipPriceHistory]  WITH CHECK ADD  CONSTRAINT [FK_MembersipPriceHistory_Memberships] FOREIGN KEY([MembershipTypeFk])
REFERENCES [dbo].[MembershipTypes] ([Id])
GO
ALTER TABLE [dbo].[MembersipPriceHistory] CHECK CONSTRAINT [FK_MembersipPriceHistory_Memberships]
GO
ALTER TABLE [dbo].[Modules]  WITH CHECK ADD  CONSTRAINT [FK_Modules_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Modules] CHECK CONSTRAINT [FK_Modules_Applications]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Companies]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_ListItems] FOREIGN KEY([NotificationTypeFk])
REFERENCES [dbo].[GeneralTypeItems] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_ListItems]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_NotificationTemplates] FOREIGN KEY([NotificationTemplateGroupFk], [CultureFk])
REFERENCES [dbo].[NotificationTemplates] ([NotificationTemplateGroupId], [CultureFk])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_NotificationTemplates]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_NotificationTraces_Cultures] FOREIGN KEY([CultureFk])
REFERENCES [dbo].[Cultures] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_NotificationTraces_Cultures]
GO
ALTER TABLE [dbo].[NotificationTemplates]  WITH CHECK ADD  CONSTRAINT [FK_NotificationTemplates_Cultures] FOREIGN KEY([CultureFk])
REFERENCES [dbo].[Cultures] ([Id])
GO
ALTER TABLE [dbo].[NotificationTemplates] CHECK CONSTRAINT [FK_NotificationTemplates_Cultures]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_ModulePages_Modules] FOREIGN KEY([ModuleFk])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_ModulePages_Modules]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Applications]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY([RoleFk])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserRolePermissions_Components] FOREIGN KEY([ComponentFk])
REFERENCES [dbo].[Components] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_UserRolePermissions_Components]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserRolePermissions_Modules] FOREIGN KEY([ModuleFk])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_UserRolePermissions_Modules]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserRolePermissions_Pages] FOREIGN KEY([PageFk])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_UserRolePermissions_Pages]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Applications]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Companies]
GO
ALTER TABLE [dbo].[Sequences]  WITH CHECK ADD  CONSTRAINT [FK_Sequences_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Sequences] CHECK CONSTRAINT [FK_Sequences_Applications]
GO
ALTER TABLE [dbo].[Sequences]  WITH CHECK ADD  CONSTRAINT [FK_Sequences_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Sequences] CHECK CONSTRAINT [FK_Sequences_Companies]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleFk])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserFk])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Companies]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "r"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ur"
            Begin Extent = 
               Top = 7
               Left = 328
               Bottom = 170
               Right = 534
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "u"
            Begin Extent = 
               Top = 7
               Left = 582
               Bottom = 170
               Right = 840
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 888
               Bottom = 170
               Right = 1117
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 1165
               Bottom = 170
               Right = 1394
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_CompanyUserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_CompanyUserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_CompanyUserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cmp"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 303
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 351
               Bottom = 170
               Right = 580
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 628
               Bottom = 170
               Right = 857
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 905
               Bottom = 170
               Right = 1137
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 1185
               Bottom = 170
               Right = 1417
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 3108
         Width = 1200
         Width = 1200
         Width = 5892
         Width = 4044
         Width = 1200
      End
   End
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAllCompanyPermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAllCompanyPermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAllCompanyPermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ar"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ae"
            Begin Extent = 
               Top = 7
               Left = 300
               Bottom = 170
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 542
               Bottom = 170
               Right = 771
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "u"
            Begin Extent = 
               Top = 7
               Left = 819
               Bottom = 170
               Right = 1077
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 7
               Left = 1125
               Bottom = 170
               Right = 1357
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAuditRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAuditRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAuditRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[16] 4[60] 2[2] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 325
               Bottom = 419
               Right = 567
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cm"
            Begin Extent = 
               Top = 7
               Left = 602
               Bottom = 340
               Right = 843
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 313
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 7
               Left = 891
               Bottom = 170
               Right = 1123
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 37
         Width = 284
         Width = 1200
         Width = 2772
         Width = 2028
         Width = 1200
         Width = 2076
         Width = 1716
         Width = 1200
         Width = 1200
         Width = 1704
         Width = 1908
         Width = 1200
         Width = 2088
         Width = 1200
         Width = 1584
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
    ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetCompanyMemberships'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2184
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetCompanyMemberships'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetCompanyMemberships'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetDatabaseTables'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetDatabaseTables'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "li"
            Begin Extent = 
               Top = 7
               Left = 290
               Bottom = 170
               Right = 522
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lig"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 148
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetGeneralTypeItemWithGroups'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetGeneralTypeItemWithGroups'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "rp"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 290
               Bottom = 170
               Right = 519
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 567
               Bottom = 170
               Right = 796
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 844
               Bottom = 170
               Right = 1076
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 175
               Left = 328
               Bottom = 338
               Right = 560
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 120' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetRolePermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'0
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetRolePermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetRolePermissions'
GO
USE [master]
GO
ALTER DATABASE [Vito.Transverse.DB] SET  READ_WRITE 
GO
