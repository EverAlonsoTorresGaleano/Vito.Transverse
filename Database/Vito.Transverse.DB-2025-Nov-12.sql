USE [master]
GO
/****** Object:  Database [Vito.Transverse.DB]    Script Date: 11/13/2025 12:36:01 AM ******/
CREATE DATABASE [Vito.Transverse.DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Vito.Transverse.DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Vito.Transverse.DB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Vito.Transverse.DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Vito.Transverse.DB_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[Entities]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entities](
	[Id] [bigint] NOT NULL,
	[SchemaName] [varchar](75) NOT NULL,
	[EntityName] [varchar](75) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSystemEntity] [bit] NOT NULL,
 CONSTRAINT [PK_AuditEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/13/2025 12:36:01 AM ******/
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
/****** Object:  Table [dbo].[AuditRecords]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditRecords](
	[CompanyFk] [bigint] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserFk] [bigint] NOT NULL,
	[EntityFk] [bigint] NOT NULL,
	[AuditTypeFk] [bigint] NOT NULL,
	[AuditEntityIndex] [varchar](75) NOT NULL,
	[HostName] [varchar](75) NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[DeviceType] [varchar](50) NOT NULL,
	[Browser] [varchar](50) NOT NULL,
	[Platform] [varchar](50) NOT NULL,
	[Engine] [varchar](50) NOT NULL,
	[CultureFk] [varchar](50) NOT NULL,
	[EndPointUrl] [varchar](100) NOT NULL,
	[Method] [varchar](10) NOT NULL,
	[QueryString] [varchar](100) NOT NULL,
	[UserAgent] [varchar](200) NOT NULL,
	[Referer] [varchar](100) NOT NULL,
	[ApplicationId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[AuditChanges] [text] NOT NULL,
 CONSTRAINT [PK_AuditRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
	[CompanyClient] [uniqueidentifier] NOT NULL,
	[CompanySecret] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[Subdomain] [varchar](150) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[DefaultCultureFk] [varchar](5) NOT NULL,
	[CountryFk] [varchar](2) NOT NULL,
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
/****** Object:  Table [dbo].[GeneralTypeItems]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralTypeItems](
	[ListItemGroupFk] [bigint] NOT NULL,
	[OrderIndex] [int] NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_ListItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetAuditRecords]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetAuditRecords]
AS
SELECT ar.CreationDate, ar.CompanyFk, c.NameTranslationKey, ar.UserFk, u.UserName, ar.AuditTypeFk, t.NameTranslationKey AS Expr1, ar.EntityFk, ae.SchemaName, ae.EntityName, ae.IsSystemEntity, ar.HostName, ar.DeviceType, ar.Browser, 
                  ar.Platform, ar.Engine, ar.CultureFk, ar.AuditChanges
FROM     dbo.AuditRecords AS ar INNER JOIN
                  dbo.Entities AS ae ON ar.EntityFk = ae.Id INNER JOIN
                  dbo.Companies AS c ON ar.CompanyFk = c.Id INNER JOIN
                  dbo.Users AS u ON ar.UserFk = u.Id INNER JOIN
                  dbo.GeneralTypeItems AS t ON ar.AuditTypeFk = t.Id
GO
/****** Object:  Table [dbo].[GeneralTypeGroups]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralTypeGroups](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[IsSystemType] [bit] NOT NULL,
 CONSTRAINT [PK_ListItemGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetGeneralTypeItemWithGroups]    Script Date: 11/13/2025 12:36:01 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/13/2025 12:36:01 AM ******/
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
/****** Object:  Table [dbo].[Applications]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
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
/****** Object:  View [dbo].[Vw_CompanyUserRoles]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_CompanyUserRoles]
AS
SELECT c.Id AS CompanyId, c.NameTranslationKey AS CompanyName, a.Id AS ApplicationId, a.NameTranslationKey AS ApplicationName, u.Id AS UserId, u.UserName + u.LastName AS UserName, r.Id AS RoleId, 
                  r.NameTranslationKey AS RoleName, ur.IsActive AS UserRoleIsActive
FROM     dbo.Roles AS r INNER JOIN
                  dbo.UserRoles AS ur ON r.Id = ur.RoleFk INNER JOIN
                  dbo.Users AS u ON u.Id = ur.UserFk INNER JOIN
                  dbo.Applications AS a ON ur.ApplicationFk = a.Id INNER JOIN
                  dbo.Companies AS c ON ur.CompanyFk = c.Id
GO
/****** Object:  Table [dbo].[CompanyMemberships]    Script Date: 11/13/2025 12:36:01 AM ******/
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
/****** Object:  Table [dbo].[MembershipTypes]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembershipTypes](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
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
/****** Object:  View [dbo].[Vw_GetCompanyMemberships]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetCompanyMemberships]
AS
SELECT cm.Id AS CompanyMemberShipId, cm.MembershipTypeFk, t.NameTranslationKey AS MembershipTypeName, t.IsActive AS MembershipTypeIsActive, c.Id AS CompanyId, c.NameTranslationKey AS CompanyName, c.CompanyClient, 
                  c.CompanySecret, c.Subdomain, c.Email, c.IsActive AS CompanyIsActive, c.IsSystemCompany, c.DefaultCultureFk, c.CountryFk, a.Id AS ApplicationId, a.NameTranslationKey AS ApplicationName, a.IsActive AS ApplicationIsActive, 
                  a.ApplicationClient, a.ApplicationSecret
FROM     dbo.Companies AS c INNER JOIN
                  dbo.CompanyMemberships AS cm ON cm.CompanyFk = c.Id AND c.Id = cm.CompanyFk INNER JOIN
                  dbo.Applications AS a ON cm.ApplicationFk = a.Id AND cm.ApplicationFk = a.Id INNER JOIN
                  dbo.MembershipTypes AS t ON cm.MembershipTypeFk = t.Id
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[ApplicationFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
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
/****** Object:  Table [dbo].[Endpoints]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endpoints](
	[ApplicationFk] [bigint] NOT NULL,
	[ModuleFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[PositionIndex] [bigint] NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
	[EndpointUrl] [varchar](75) NOT NULL,
	[Method] [varchar](10) NULL,
	[IsActive] [bit] NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsApi] [bit] NOT NULL,
 CONSTRAINT [PK_EndpointsModules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyMembershipPermissions]    Script Date: 11/13/2025 12:36:01 AM ******/
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
	[EndpointFk] [bigint] NOT NULL,
	[ComponentFk] [bigint] NULL,
	[PropertyValue] [varchar](75) NULL,
 CONSTRAINT [PK_CompanyMembershipPermissions_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetAllCompanyPermissions]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*INNER JOIN Components com ON cmp.ComponentFk=com.Id
WHERE cmp.CompanyFk=1*/
CREATE VIEW [dbo].[Vw_GetAllCompanyPermissions]
AS
SELECT cmp.CompanyMembershipFk, cmp.Id AS CompanyMembershipPermissionsId, c.Id AS CompanyId, c.NameTranslationKey AS CompanyName, a.Id AS ApplicationId, a.NameTranslationKey AS ApplicationName, 
                  a.IsActive AS ApplicationIsActive, m.Id AS ModuleId, m.NameTranslationKey AS ModuleName, m.IsActive AS ModuleIsActive, p.Id AS EndpointId, p.NameTranslationKey AS EndpointName, p.EndpointUrl, p.IsActive AS EndpointIsActive, 
                  cmp.ComponentFk
FROM     dbo.CompanyMembershipPermissions AS cmp INNER JOIN
                  dbo.Applications AS a ON cmp.ApplicationFk = a.Id INNER JOIN
                  dbo.Companies AS c ON cmp.CompanyFk = c.Id INNER JOIN
                  dbo.Modules AS m ON cmp.ModuleFk = m.Id INNER JOIN
                  dbo.Endpoints AS p ON cmp.EndpointFk = p.Id
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 11/13/2025 12:36:01 AM ******/
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
	[EndpointFk] [bigint] NULL,
	[ComponentFk] [bigint] NULL,
	[PropertyValue] [varchar](75) NULL,
	[Obs] [varchar](50) NULL,
 CONSTRAINT [PK_UserRolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_GetRolePermissions]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_GetRolePermissions]
AS
SELECT rp.RoleFk, r.NameTranslationKey AS RoleName, rp.Id AS RolePermissionsId, c.Id AS CompanyId, c.NameTranslationKey AS CompanyName, a.Id AS ApplicationId, a.NameTranslationKey AS ApplicationName, 
                  a.IsActive AS ApplicationIsActive, m.Id AS ModuleId, m.NameTranslationKey AS ModuleName, m.IsActive AS ModuleIsActive, p.Id AS EndpointId, p.NameTranslationKey AS EndpointName, p.EndpointUrl, p.IsActive AS EndpointIsActive, 
                  rp.ComponentFk
FROM     dbo.RolePermissions AS rp INNER JOIN
                  dbo.Applications AS a ON rp.ApplicationFk = a.Id INNER JOIN
                  dbo.Companies AS c ON rp.CompanyFk = c.Id INNER JOIN
                  dbo.Modules AS m ON rp.ModuleFk = m.Id INNER JOIN
                  dbo.Endpoints AS p ON rp.EndpointFk = p.Id INNER JOIN
                  dbo.Roles AS r ON rp.RoleFk = r.Id
GO
/****** Object:  View [dbo].[Vw_GetDatabaseTables]    Script Date: 11/13/2025 12:36:01 AM ******/
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
/****** Object:  Table [dbo].[ActivityLogs]    Script Date: 11/13/2025 12:36:01 AM ******/
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
	[EndPointUrl] [varchar](100) NOT NULL,
	[Method] [varchar](10) NOT NULL,
	[QueryString] [varchar](100) NOT NULL,
	[UserAgent] [varchar](200) NOT NULL,
	[Referer] [varchar](100) NOT NULL,
	[ApplicationId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_UserTraces] PRIMARY KEY CLUSTERED 
(
	[TraceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationLicenseTypes]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationLicenseTypes](
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
	[LicenseFile] [varbinary](max) NULL,
 CONSTRAINT [PK_ApplicationLicenseTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationOwners]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationOwners](
	[Id] [bigint] NOT NULL,
	[CompanyFk] [bigint] NOT NULL,
	[ApplicationFk] [bigint] NOT NULL,
	[ApplicationLicenseTypeFk] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ApplicationOwners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyEntityAudits]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyEntityAudits](
	[CompanyFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[EntityFk] [bigint] NOT NULL,
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
/****** Object:  Table [dbo].[Components]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Components](
	[ApplicationFk] [bigint] NOT NULL,
	[EndpointFk] [bigint] NOT NULL,
	[Id] [bigint] NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[DescriptionTranslationKey] [varchar](85) NOT NULL,
	[ObjectId] [varchar](75) NOT NULL,
	[ObjectName] [varchar](75) NOT NULL,
	[ObjectPropertyName] [varchar](75) NOT NULL,
	[DefaultPropertyValue] [varchar](75) NOT NULL,
	[PositionIndex] [bigint] NULL,
 CONSTRAINT [PK_EndpointComponents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [varchar](2) NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[UtcHoursDifference] [int] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cultures]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cultures](
	[Id] [varchar](5) NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
	[CountryFk] [varchar](2) NOT NULL,
	[LanguageFk] [varchar](2) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Cultures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CultureTranslations]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CultureTranslations](
	[ApplicationFk] [bigint] NOT NULL,
	[CultureFk] [varchar](5) NOT NULL,
	[TranslationKey] [varchar](85) NOT NULL,
	[TranslationValue] [varchar](250) NOT NULL,
 CONSTRAINT [PK_CultureTranslations] PRIMARY KEY CLUSTERED 
(
	[ApplicationFk] ASC,
	[CultureFk] ASC,
	[TranslationKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [varchar](2) NOT NULL,
	[NameTranslationKey] [varchar](85) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembersipPriceHistory]    Script Date: 11/13/2025 12:36:01 AM ******/
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
/****** Object:  Table [dbo].[Notifications]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[CompanyFk] [bigint] NOT NULL,
	[NotificationTemplateGroupFk] [bigint] NOT NULL,
	[CultureFk] [varchar](5) NOT NULL,
	[NotificationTypeFk] [bigint] NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Sender] [varchar](75) NOT NULL,
	[Receiver] [varchar](500) NOT NULL,
	[CC] [varchar](500) NULL,
	[BCC] [varchar](500) NULL,
	[Subject] [varchar](250) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[IsSent] [bit] NOT NULL,
	[SentDate] [datetime] NULL,
	[IsHtml] [bit] NOT NULL,
 CONSTRAINT [PK_NotificationTraces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationTemplates]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationTemplates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NotificationTemplateGroupId] [bigint] NOT NULL,
	[CultureFk] [varchar](5) NOT NULL,
	[Name] [varchar](75) NOT NULL,
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
/****** Object:  Table [dbo].[Pictures]    Script Date: 11/13/2025 12:36:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pictures](
	[CompanyFk] [bigint] NOT NULL,
	[Name] [varchar](75) NOT NULL,
	[Id] [bigint] NOT NULL,
	[EntityFk] [bigint] NOT NULL,
	[FileTypeFk] [bigint] NOT NULL,
	[PictureCategoryFk] [bigint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedByUserFk] [bigint] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateByUserFk] [bigint] NULL,
	[IsActive] [bit] NOT NULL,
	[BinaryPicture] [varbinary](max) NULL,
	[PictureSize] [decimal](18, 5) NOT NULL,
 CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sequences]    Script Date: 11/13/2025 12:36:01 AM ******/
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
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 112, CAST(N'2025-05-26T06:04:13.740' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 113, CAST(N'2025-05-26T06:04:19.333' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 114, CAST(N'2025-05-26T06:04:19.500' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 115, CAST(N'2025-05-26T06:04:24.410' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 116, CAST(N'2025-05-26T06:12:29.387' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 117, CAST(N'2025-05-26T06:12:31.757' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 118, CAST(N'2025-05-26T06:14:21.173' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ActivateAccountAsync', N'GET', N'?activationToken=C5BCEA98-2974-4D43-8110-28D402CF5CE2@1@414026C1-8BF2-41F2-97C2-C1A3B7F656B8', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 119, CAST(N'2025-05-26T06:14:41.630' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 120, CAST(N'2025-05-26T06:15:23.223' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/NotificationsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 121, CAST(N'2025-05-26T06:15:33.610' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 122, CAST(N'2025-05-26T06:26:37.530' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/NotificationsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 123, CAST(N'2025-05-26T06:26:41.093' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 124, CAST(N'2025-05-26T06:26:43.777' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/AuditRecordsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 125, CAST(N'2025-05-26T06:26:46.480' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/CompanyEntityAuditsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 126, CAST(N'2025-05-26T06:26:49.717' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Audit/v1/EntityListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 127, CAST(N'2025-05-26T06:26:52.213' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 128, CAST(N'2025-05-26T06:27:13.643' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/CacheList', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 129, CAST(N'2025-05-26T06:27:20.460' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 130, CAST(N'2025-05-26T06:27:57.417' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/AllApplicationListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 131, CAST(N'2025-05-26T06:27:59.727' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ApplicationListAsync', N'GET', N'?Companyid=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 132, CAST(N'2025-05-26T06:28:02.127' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/CompanyMemberhipAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 133, CAST(N'2025-05-26T06:28:04.697' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/AllCompanyListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 134, CAST(N'2025-05-26T06:28:07.403' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/RoleListAsync', N'GET', N'?CompanyId=2', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 135, CAST(N'2025-05-26T06:28:10.260' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/RolePermissionListAsync', N'GET', N'?roleId=4', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 136, CAST(N'2025-05-26T06:28:12.870' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ModuleListAsync', N'GET', N'?ApplicationId=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 137, CAST(N'2025-05-26T06:28:15.630' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/EndpointsListAsync', N'GET', N'?moduleId=10', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 138, CAST(N'2025-05-26T06:28:18.537' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ComponentListAsync', N'GET', N'?EndpointId=21', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 139, CAST(N'2025-05-26T06:28:22.607' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserRolesListAsync', N'GET', N'?userId=4', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 140, CAST(N'2025-05-26T06:28:25.613' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserPermissionListAsync', N'GET', N'?userId=4', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 141, CAST(N'2025-05-26T06:28:36.927' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 142, CAST(N'2025-05-26T06:28:48.577' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/CacheList', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 143, CAST(N'2025-05-26T06:29:10.967' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/Cache', N'DELETE', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 144, CAST(N'2025-05-26T06:29:10.997' AS DateTime), N'localhost:5237', N'Desktop', 416, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/Cache', N'DELETE', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 145, CAST(N'2025-05-26T06:29:16.620' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 146, CAST(N'2025-05-26T06:29:43.417' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 147, CAST(N'2025-05-26T06:29:56.227' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 148, CAST(N'2025-05-30T21:52:03.273' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 149, CAST(N'2025-05-30T21:52:05.590' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 150, CAST(N'2025-05-30T22:34:55.647' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 151, CAST(N'2025-05-30T22:34:58.123' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 152, CAST(N'2025-05-30T22:35:20.993' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 153, CAST(N'2025-05-30T22:54:30.743' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 154, CAST(N'2025-05-30T22:54:33.147' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 155, CAST(N'2025-05-30T22:55:29.100' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/NotificationsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 156, CAST(N'2025-05-30T22:55:35.193' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 157, CAST(N'2025-05-30T22:55:39.787' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/CompanyEntityAuditsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 158, CAST(N'2025-05-30T22:55:52.433' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 159, CAST(N'2025-05-30T22:55:55.843' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 160, CAST(N'2025-05-30T22:57:17.800' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ActivateAccountAsync', N'GET', N'?activationToken=C5BCEA98-2974-4D43-8110-28D402CF5CE2@1@414026C1-8BF2-41F2-97C2-C1A3B7F656B8', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 161, CAST(N'2025-05-31T05:16:56.687' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserListAsync', N'GET', N'?companyId=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 162, CAST(N'2025-05-31T05:21:32.847' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserListAsync', N'GET', N'?companyId=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 163, CAST(N'2025-05-31T05:21:40.040' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/Cache', N'DELETE', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 164, CAST(N'2025-05-31T05:21:40.067' AS DateTime), N'localhost:5237', N'Desktop', 416, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/Cache', N'DELETE', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 165, CAST(N'2025-05-31T05:21:45.320' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserListAsync', N'GET', N'?companyId=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 166, CAST(N'2025-06-04T00:06:57.733' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 167, CAST(N'2025-06-04T00:07:10.507' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 168, CAST(N'2025-06-04T00:07:23.083' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 169, CAST(N'2025-06-04T00:07:25.337' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 170, CAST(N'2025-06-04T00:07:49.200' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 171, CAST(N'2025-06-04T00:08:07.893' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 172, CAST(N'2025-06-04T00:08:57.210' AS DateTime), N'localhost:5237', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 173, CAST(N'2025-06-04T00:51:25.500' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/Cache', N'DELETE', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 174, CAST(N'2025-06-04T00:51:25.520' AS DateTime), N'localhost:5237', N'Desktop', 416, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/Cache', N'DELETE', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 175, CAST(N'2025-06-04T00:51:33.767' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 176, CAST(N'2025-06-04T00:51:44.787' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/cacheAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 177, CAST(N'2025-06-04T00:51:50.107' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Health/v1/databaseAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 178, CAST(N'2025-06-05T18:17:18.810' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 179, CAST(N'2025-06-05T18:17:39.087' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 180, CAST(N'2025-06-05T18:21:04.013' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Health/v1/databaseAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 181, CAST(N'2025-06-05T18:21:59.687' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ActivateAccountAsync', N'GET', N'?activationToken=C5BCEA98-2974-4D43-8110-28D402CF5CE2@1@414026C1-8BF2-41F2-97C2-C1A3B7F656B8', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 182, CAST(N'2025-06-05T18:23:18.407' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 183, CAST(N'2025-06-05T18:23:19.937' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 184, CAST(N'2025-06-05T18:23:41.843' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/NotificationsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 185, CAST(N'2025-06-05T18:23:56.877' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 186, CAST(N'2025-06-05T18:24:01.643' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/AuditRecordsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 187, CAST(N'2025-06-05T18:24:07.450' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/CompanyEntityAuditsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 188, CAST(N'2025-06-05T18:27:12.453' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Audit/v1/EntityListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 189, CAST(N'2025-06-05T18:27:16.297' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/CacheList', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 190, CAST(N'2025-06-05T18:27:22.380' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Cache/v1/CacheByKey', N'DELETE', N'?CacheName=EndpointListByRoleId9', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 191, CAST(N'2025-06-05T18:27:56.650' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 192, CAST(N'2025-06-05T18:28:00.373' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/cacheAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 193, CAST(N'2025-06-05T18:28:04.137' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Health/v1/databaseAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 194, CAST(N'2025-06-05T18:28:26.540' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/SendActivationEmailAsync', N'GET', N'?companyId=1&userId=4', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 4, 195, CAST(N'2025-06-05T18:28:26.760' AS DateTime), N'localhost:5237', N'Desktop', 405, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/SendActivationEmailAsync', N'GET', N'?companyId=1&userId=4', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 196, CAST(N'2025-06-05T18:28:44.297' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/AllApplicationListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 197, CAST(N'2025-06-05T18:29:26.523' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ApplicationListAsync', N'GET', N'?Companyid=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 198, CAST(N'2025-06-05T18:29:29.743' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/CompanyMemberhipAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 199, CAST(N'2025-06-05T18:29:33.027' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/AllCompanyListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 200, CAST(N'2025-06-05T18:29:36.227' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/RoleListAsync', N'GET', N'?CompanyId=2', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 201, CAST(N'2025-06-05T18:29:40.007' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/RolePermissionListAsync', N'GET', N'?roleId=9', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 202, CAST(N'2025-06-05T18:29:43.397' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ModuleListAsync', N'GET', N'?ApplicationId=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 203, CAST(N'2025-06-05T18:29:46.470' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/EndpointsListAsync', N'GET', N'?moduleId=10', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 204, CAST(N'2025-06-05T18:29:49.777' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/ComponentListAsync', N'GET', N'?EndpointId=21', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 205, CAST(N'2025-06-05T18:29:53.310' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserRolesListAsync', N'GET', N'?userId=6', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 206, CAST(N'2025-06-05T18:29:56.287' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserPermissionListAsync', N'GET', N'?userId=6', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 207, CAST(N'2025-06-05T18:29:59.993' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/UserListAsync', N'GET', N'?companyId=1', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 208, CAST(N'2025-06-05T19:17:00.947' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 209, CAST(N'2025-06-05T19:17:37.853' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 210, CAST(N'2025-06-05T19:18:04.143' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 211, CAST(N'2025-06-05T19:21:14.457' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 212, CAST(N'2025-06-05T19:21:28.933' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 213, CAST(N'2025-06-05T19:47:48.863' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 214, CAST(N'2025-06-05T19:49:44.520' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 215, CAST(N'2025-06-05T19:51:23.973' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 216, CAST(N'2025-06-05T20:52:09.013' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 217, CAST(N'2025-06-05T20:52:49.400' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/cacheAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 218, CAST(N'2025-06-05T20:52:53.413' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Health/v1/databaseAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 219, CAST(N'2025-06-05T21:01:12.740' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Health/v1/databaseAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 220, CAST(N'2025-06-05T21:06:23.857' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 221, CAST(N'2025-06-05T21:06:41.867' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 222, CAST(N'2025-06-05T21:10:41.200' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 223, CAST(N'2025-06-05T21:16:24.783' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 224, CAST(N'2025-06-05T21:18:19.440' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 225, CAST(N'2025-06-05T21:18:42.217' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 226, CAST(N'2025-06-05T21:19:33.370' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 227, CAST(N'2025-06-05T21:21:38.243' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 228, CAST(N'2025-06-05T21:29:06.990' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 229, CAST(N'2025-06-05T21:49:25.323' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/allasync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 230, CAST(N'2025-06-05T21:50:30.533' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/AuditRecordsListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 231, CAST(N'2025-06-05T21:50:42.647' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 232, CAST(N'2025-06-05T21:52:54.000' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 233, CAST(N'2025-06-05T21:53:01.863' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/audit/v1/ActivityLogListAsync', N'GET', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 234, CAST(N'2025-06-05T22:23:33.963' AS DateTime), N'localhost:5237', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 235, CAST(N'2025-06-05T22:23:36.160' AS DateTime), N'localhost:5237', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 236, CAST(N'2025-10-20T22:41:55.897' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 237, CAST(N'2025-10-20T22:42:21.040' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 238, CAST(N'2025-10-20T22:43:09.017' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 239, CAST(N'2025-10-20T22:43:33.973' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 240, CAST(N'2025-10-20T22:44:36.227' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 241, CAST(N'2025-10-20T22:55:34.977' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 242, CAST(N'2025-10-20T22:56:06.867' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 243, CAST(N'2025-10-20T22:57:22.727' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 244, CAST(N'2025-10-20T22:59:34.973' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 245, CAST(N'2025-10-20T22:59:48.103' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 246, CAST(N'2025-10-20T23:01:51.660' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 247, CAST(N'2025-10-20T23:02:00.200' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 248, CAST(N'2025-10-20T23:02:31.190' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 249, CAST(N'2025-10-20T23:03:06.663' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 250, CAST(N'2025-10-20T23:04:15.917' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 251, CAST(N'2025-10-21T00:38:30.357' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 252, CAST(N'2025-10-21T00:38:35.327' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 253, CAST(N'2025-10-21T00:59:20.547' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 254, CAST(N'2025-10-21T00:59:26.660' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 255, CAST(N'2025-10-28T23:12:26.697' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 256, CAST(N'2025-10-28T23:12:31.093' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 257, CAST(N'2025-10-28T23:43:39.600' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 258, CAST(N'2025-10-28T23:43:42.153' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 259, CAST(N'2025-10-28T23:43:44.967' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 260, CAST(N'2025-10-28T23:43:45.020' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 261, CAST(N'2025-11-12T05:07:09.620' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.1', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 262, CAST(N'2025-11-12T05:07:16.007' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 263, CAST(N'2025-11-12T05:13:26.140' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Chrome v141.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/applications/v1', N'GET', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/141.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 264, CAST(N'2025-11-12T05:24:07.397' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.1', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 265, CAST(N'2025-11-12T05:24:16.713' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 266, CAST(N'2025-11-12T05:25:08.157' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 267, CAST(N'2025-11-12T05:26:41.037' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 268, CAST(N'2025-11-12T05:26:58.443' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/1', N'GET', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 269, CAST(N'2025-11-12T05:28:00.037' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/1', N'GET', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 270, CAST(N'2025-11-12T05:36:30.147' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 271, CAST(N'2025-11-12T05:36:31.627' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 272, CAST(N'2025-11-12T05:44:46.617' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/0', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 273, CAST(N'2025-11-12T05:54:07.533' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/health/v1/', N'GET', N'', N'PostmanRuntime/7.49.1', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 274, CAST(N'2025-11-12T06:16:38.530' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 275, CAST(N'2025-11-12T06:16:40.873' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 276, CAST(N'2025-11-12T06:19:50.853' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 277, CAST(N'2025-11-12T06:19:51.097' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 278, CAST(N'2025-11-12T06:28:24.037' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 279, CAST(N'2025-11-12T06:28:25.507' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 280, CAST(N'2025-11-12T06:31:19.680' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 281, CAST(N'2025-11-12T06:31:19.887' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 282, CAST(N'2025-11-12T06:31:40.537' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 283, CAST(N'2025-11-12T06:31:40.623' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 284, CAST(N'2025-11-12T06:31:45.807' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 285, CAST(N'2025-11-12T06:31:45.857' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 286, CAST(N'2025-11-12T06:32:02.157' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 287, CAST(N'2025-11-12T06:32:02.477' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 288, CAST(N'2025-11-12T06:32:13.467' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 289, CAST(N'2025-11-12T06:32:13.497' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 290, CAST(N'2025-11-12T06:32:16.570' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 291, CAST(N'2025-11-12T06:32:16.607' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 292, CAST(N'2025-11-12T06:32:43.743' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 293, CAST(N'2025-11-12T06:32:43.840' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 294, CAST(N'2025-11-12T06:32:45.227' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 295, CAST(N'2025-11-12T06:32:45.373' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 296, CAST(N'2025-11-12T06:33:11.187' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 297, CAST(N'2025-11-12T06:33:11.403' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 298, CAST(N'2025-11-12T06:33:13.903' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 299, CAST(N'2025-11-12T06:33:49.750' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 300, CAST(N'2025-11-12T06:33:49.780' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 301, CAST(N'2025-11-12T06:33:52.810' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 302, CAST(N'2025-11-12T06:34:52.180' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/0', N'GET', N'?companyId=1;&userId=1;;', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 303, CAST(N'2025-11-12T06:34:56.280' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 304, CAST(N'2025-11-12T06:34:58.650' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/0', N'GET', N'?companyId=1;&userId=1;;', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 305, CAST(N'2025-11-12T06:35:08.750' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/0', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 306, CAST(N'2025-11-12T06:35:22.883' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 307, CAST(N'2025-11-12T06:35:42.343' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/0/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 308, CAST(N'2025-11-12T06:36:07.943' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/0/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 309, CAST(N'2025-11-12T06:37:14.643' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/1', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 310, CAST(N'2025-11-12T06:40:38.133' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/ByCompany/1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 311, CAST(N'2025-11-12T06:42:21.437' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 312, CAST(N'2025-11-12T06:43:11.467' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules/1', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 313, CAST(N'2025-11-12T07:00:19.667' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 314, CAST(N'2025-11-12T07:00:43.427' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 315, CAST(N'2025-11-12T07:02:12.030' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 316, CAST(N'2025-11-12T07:02:12.277' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 317, CAST(N'2025-11-12T07:02:15.063' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 318, CAST(N'2025-11-12T07:02:18.197' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/1', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 319, CAST(N'2025-11-12T07:02:20.673' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/ByCompany/1/', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 320, CAST(N'2025-11-12T07:02:32.783' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 321, CAST(N'2025-11-12T07:03:44.887' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules/1', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 322, CAST(N'2025-11-12T07:05:49.883' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules/1/endpoints', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 323, CAST(N'2025-11-12T07:09:41.880' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/Modules/1/endpoints', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 324, CAST(N'2025-11-12T07:10:42.970' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/applications/v1/endpoints/1/Components', N'GET', N'', N'', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 325, CAST(N'2025-11-12T17:07:01.907' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 326, CAST(N'2025-11-12T17:07:04.077' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 327, CAST(N'2025-11-12T17:25:01.880' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/secuences', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 328, CAST(N'2025-11-12T17:29:46.967' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/secuences/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 329, CAST(N'2025-11-12T17:30:00.840' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/secuences/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 330, CAST(N'2025-11-12T17:32:44.743' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/secuences/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 331, CAST(N'2025-11-12T19:10:34.097' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/secuences/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 332, CAST(N'2025-11-12T19:11:02.287' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 333, CAST(N'2025-11-12T19:11:28.213' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/modules', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 334, CAST(N'2025-11-12T19:12:34.003' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Auditories/v1/CompanyEntityAudits', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 335, CAST(N'2025-11-12T19:14:29.940' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/companies/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 336, CAST(N'2025-11-12T19:15:11.677' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/companies/v1/memberships', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 337, CAST(N'2025-11-12T19:15:45.403' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/companies/v1/membershiptypes', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 338, CAST(N'2025-11-12T19:16:18.977' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/secuences', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 339, CAST(N'2025-11-12T19:16:41.240' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/cultures', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 340, CAST(N'2025-11-12T19:16:52.300' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/languages', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 341, CAST(N'2025-11-12T19:17:03.137' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/countries', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 342, CAST(N'2025-11-12T19:17:29.710' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypeGroups', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 343, CAST(N'2025-11-12T19:17:40.397' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypeitems', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 344, CAST(N'2025-11-12T19:20:39.253' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypeitems', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 345, CAST(N'2025-11-12T19:21:16.660' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/notificationtemplates', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 346, CAST(N'2025-11-12T19:38:14.833' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypeGroups', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 347, CAST(N'2025-11-12T21:10:14.933' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypeGroups', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 348, CAST(N'2025-11-12T21:10:46.460' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/roles/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 349, CAST(N'2025-11-12T21:11:29.967' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/roles/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 350, CAST(N'2025-11-12T21:13:09.343' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 351, CAST(N'2025-11-12T21:14:50.690' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'?companyId=2', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 352, CAST(N'2025-11-12T21:14:57.323' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 353, CAST(N'2025-11-12T21:15:20.230' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/roles/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 354, CAST(N'2025-11-12T21:15:35.593' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 355, CAST(N'2025-11-12T21:16:20.057' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 356, CAST(N'2025-11-12T21:17:43.033' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 357, CAST(N'2025-11-12T21:17:44.130' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 358, CAST(N'2025-11-12T21:17:45.000' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 359, CAST(N'2025-11-12T21:17:45.893' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 360, CAST(N'2025-11-12T21:17:46.720' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 361, CAST(N'2025-11-12T21:18:40.017' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 362, CAST(N'2025-11-12T21:20:05.237' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/notificationtemplates/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 363, CAST(N'2025-11-12T21:20:54.133' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypeitems/dropdown/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 364, CAST(N'2025-11-12T21:22:32.923' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/generaltypegroups/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 365, CAST(N'2025-11-12T21:22:50.030' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/countries/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 366, CAST(N'2025-11-12T21:23:06.587' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/languages/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 367, CAST(N'2025-11-12T21:23:23.110' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/cultures/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 368, CAST(N'2025-11-12T21:26:35.613' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/companies/v1/membershiptypes/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 369, CAST(N'2025-11-12T21:26:50.387' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/companies/v1/memberships/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 370, CAST(N'2025-11-12T21:27:31.820' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/companies/v1/drodown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 371, CAST(N'2025-11-12T21:28:27.877' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/auditories/v1/entities/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 372, CAST(N'2025-11-12T21:29:06.787' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 373, CAST(N'2025-11-12T21:29:41.107' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/modules/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 374, CAST(N'2025-11-12T21:30:08.290' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/modules/1/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 375, CAST(N'2025-11-12T21:30:13.430' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/modules/1/endpoints/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 376, CAST(N'2025-11-12T21:30:33.273' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/modules/1/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 377, CAST(N'2025-11-12T21:31:27.570' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/endpoints/1/components/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 378, CAST(N'2025-11-12T21:33:03.740' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/cache/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 379, CAST(N'2025-11-12T21:33:19.063' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/cache/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 380, CAST(N'2025-11-12T21:33:20.547' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/cache/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 381, CAST(N'2025-11-12T21:34:03.773' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/auditories/v1/auditrecords', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 382, CAST(N'2025-11-12T21:34:39.677' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/auditories/v1/activitylogs', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 383, CAST(N'2025-11-12T21:35:14.950' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/auditories/v1/auditrecords', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 384, CAST(N'2025-11-12T21:35:37.013' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/auditories/v1/notifications', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 385, CAST(N'2025-11-12T21:36:17.807' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/master/v1/notificationtemplates/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 386, CAST(N'2025-11-12T22:37:56.617' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 387, CAST(N'2025-11-12T22:38:01.947' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 388, CAST(N'2025-11-13T00:41:14.237' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 389, CAST(N'2025-11-13T00:44:41.237' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 390, CAST(N'2025-11-13T00:44:53.617' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 391, CAST(N'2025-11-13T00:45:00.133' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 392, CAST(N'2025-11-13T00:46:33.970' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 393, CAST(N'2025-11-13T00:46:49.013' AS DateTime), N'localhost:7295', N'Desktop', 411, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 394, CAST(N'2025-11-13T00:47:02.080' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 395, CAST(N'2025-11-13T00:57:42.650' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 396, CAST(N'2025-11-13T01:26:22.730' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 397, CAST(N'2025-11-13T01:26:29.537' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 398, CAST(N'2025-11-13T01:27:08.707' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 399, CAST(N'2025-11-13T01:27:59.487' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 400, CAST(N'2025-11-13T01:45:11.847' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 401, CAST(N'2025-11-13T01:45:19.117' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 402, CAST(N'2025-11-13T01:45:46.357' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 403, CAST(N'2025-11-13T01:45:51.297' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 404, CAST(N'2025-11-13T01:59:13.037' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 405, CAST(N'2025-11-13T01:59:16.817' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 406, CAST(N'2025-11-13T01:59:38.977' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 407, CAST(N'2025-11-13T01:59:45.590' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 408, CAST(N'2025-11-13T02:00:23.723' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 409, CAST(N'2025-11-13T02:00:28.320' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 410, CAST(N'2025-11-13T02:00:29.660' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 411, CAST(N'2025-11-13T02:00:34.873' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 412, CAST(N'2025-11-13T02:00:44.773' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 413, CAST(N'2025-11-13T02:01:35.830' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 414, CAST(N'2025-11-13T02:12:46.030' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 415, CAST(N'2025-11-13T02:22:05.090' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'api/applications/v1/{applicationId}', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 416, CAST(N'2025-11-13T02:25:56.147' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'api/applications/v1/{applicationId}', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 417, CAST(N'2025-11-13T02:28:14.290' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/{applicationId}', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 418, CAST(N'2025-11-13T02:29:50.910' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/{applicationId}', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 419, CAST(N'2025-11-13T02:31:04.530' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/Modules/{moduleId}/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 420, CAST(N'2025-11-13T02:31:06.797' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/Modules/{moduleId}/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 421, CAST(N'2025-11-13T02:32:19.653' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/Modules/{moduleId}/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 422, CAST(N'2025-11-13T02:33:11.390' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/Modules/{moduleId}/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 423, CAST(N'2025-11-13T02:34:12.933' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Master/v1/NotificationTemplates/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 424, CAST(N'2025-11-13T02:42:06.717' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Master/v1/NotificationTemplates/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 425, CAST(N'2025-11-13T02:42:09.110' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Master/v1/NotificationTemplates/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 426, CAST(N'2025-11-13T02:42:10.467' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Master/v1/NotificationTemplates/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 427, CAST(N'2025-11-13T02:42:18.813' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 428, CAST(N'2025-11-13T02:42:21.410' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 429, CAST(N'2025-11-13T02:42:30.123' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Auditories/v1/AuditRecords', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 430, CAST(N'2025-11-13T02:42:51.560' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Auditories/v1/AuditRecords', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 431, CAST(N'2025-11-13T02:43:07.660' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/endpoints/{endpointId}/Components/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 432, CAST(N'2025-11-13T02:44:02.837' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Master/v1/Cultures/Active/DropDown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 433, CAST(N'2025-11-13T02:58:31.727' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 434, CAST(N'2025-11-13T02:58:49.627' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 435, CAST(N'2025-11-13T02:59:18.450' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/Modules/1/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 436, CAST(N'2025-11-13T03:05:20.583' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/applications/v1/Modules/1/endpoints/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 437, CAST(N'2025-11-13T04:15:45.937' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/Roles', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 438, CAST(N'2025-11-13T04:16:48.317' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/Roles', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 439, CAST(N'2025-11-13T04:17:48.970' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/users/v1/permissions/6', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 440, CAST(N'2025-11-13T04:36:10.877' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Chrome v142.0.0.0', N'Windows v10.0 [x64]', N'Blink', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/142.0.0.0 Safari/537.36', N'https://localhost:7295/api-docs/index.html?urls.primaryName=V1', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 441, CAST(N'2025-11-13T04:51:19.823' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 442, CAST(N'2025-11-13T04:51:25.917' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 443, CAST(N'2025-11-13T04:53:08.193' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 444, CAST(N'2025-11-13T04:53:58.170' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 445, CAST(N'2025-11-13T04:54:05.463' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (1, 2, 446, CAST(N'2025-11-13T04:54:13.270' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 2)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 447, CAST(N'2025-11-13T04:54:17.820' AS DateTime), N'localhost:7295', N'Desktop', 413, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 448, CAST(N'2025-11-13T04:54:40.673' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 449, CAST(N'2025-11-13T04:54:47.113' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 450, CAST(N'2025-11-13T04:54:48.730' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 451, CAST(N'2025-11-13T04:55:29.093' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 452, CAST(N'2025-11-13T04:59:37.020' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 453, CAST(N'2025-11-13T05:03:50.663' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 454, CAST(N'2025-11-13T05:06:11.607' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 455, CAST(N'2025-11-13T05:08:19.397' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 456, CAST(N'2025-11-13T05:09:04.583' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 457, CAST(N'2025-11-13T05:10:07.390' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 458, CAST(N'2025-11-13T05:10:37.810' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 459, CAST(N'2025-11-13T05:12:00.507' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 460, CAST(N'2025-11-13T05:15:51.730' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 461, CAST(N'2025-11-13T05:24:01.323' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 462, CAST(N'2025-11-13T05:24:43.040' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 463, CAST(N'2025-11-13T05:24:53.007' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 464, CAST(N'2025-11-13T05:25:27.057' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 465, CAST(N'2025-11-13T05:26:04.560' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 466, CAST(N'2025-11-13T05:26:23.710' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Applications/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 467, CAST(N'2025-11-13T05:26:34.910' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 468, CAST(N'2025-11-13T05:26:45.810' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 469, CAST(N'2025-11-13T05:31:11.210' AS DateTime), N'localhost:7295', N'Desktop', 417, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 470, CAST(N'2025-11-13T05:33:08.240' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 471, CAST(N'2025-11-13T05:33:21.770' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 472, CAST(N'2025-11-13T05:33:38.217' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/dropdown', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
INSERT [dbo].[ActivityLogs] ([CompanyFk], [UserFk], [TraceId], [EventDate], [DeviceName], [DeviceType], [ActionTypeFk], [IpAddress], [Browser], [Platform], [Engine], [CultureId], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId]) VALUES (2, 6, 473, CAST(N'2025-11-13T05:33:46.680' AS DateTime), N'localhost:7295', N'Desktop', 418, N'::1', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/companies/v1/dropdown/', N'GET', N'', N'PostmanRuntime/7.50.0', N'', 1, 9)
GO
SET IDENTITY_INSERT [dbo].[ActivityLogs] OFF
GO
INSERT [dbo].[ApplicationLicenseTypes] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive], [LicenseFile]) VALUES (1, N'ApplicationLicenseType_EULA', N'ApplicationLicenseType_EULA_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL)
GO
INSERT [dbo].[ApplicationOwners] ([Id], [CompanyFk], [ApplicationFk], [ApplicationLicenseTypeFk], [CreatedDate], [CreatedByUserFk], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, 1, 1, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[ApplicationOwners] ([Id], [CompanyFk], [ApplicationFk], [ApplicationLicenseTypeFk], [CreatedDate], [CreatedByUserFk], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, 1, 2, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[Applications] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [ApplicationClient], [ApplicationSecret], [CreationDate], [CreatedByUserFk], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, N'Application_VitoTorresSoft_Transverse', N'Application_VitoTorresSoft_Transverse_Dsc', N'879ffb6a-4124-41c3-bd24-76c3b2f96109', N'96471380-f3f3-4120-9e25-a4e5c9701c23', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Applications] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [ApplicationClient], [ApplicationSecret], [CreationDate], [CreatedByUserFk], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, N'Application_VitoTorresSoft_RealState', N'Application_VitoTorresSoft_RealState_Dsc', N'eb2d4ffc-dc34-435f-8983-ecd42481143f', N'ce09510a-7c12-4121-9f8b-ca0506ca302c', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[AuditRecords] ON 
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 10, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-05-26T16:04:19.487' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=26/05/2025 11:04:19\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 11, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-05-26T16:12:31.683' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=26/05/2025 11:12:31\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 12, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-05-31T07:52:05.513' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=31/05/2025 2:52:05\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 13, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-05-31T08:34:58.010' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=31/05/2025 3:34:57\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 14, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-05-31T08:54:33.023' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=31/05/2025 3:54:32\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 15, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-06-04T00:07:25.197' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=4/06/2025 12:07:24\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 16, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-06-05T18:23:19.840' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=5/06/2025 6:23:19\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (1, 17, 4, 27, 703, N'4', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/SendActivationEmailAsync', N'GET', N'?companyId=1&userId=4', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-06-05T18:28:26.740' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 1:54:51\u202Fa.\u00A0m. | After=5/06/2025 6:28:26\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 18, 6, 27, 703, N'6', N'localhost:5237', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/TokenAsync', N'POST', N'', N'PostmanRuntime/7.44.0', N'', 1, 9, CAST(N'2025-06-05T22:23:36.053' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=5/06/2025 10:23:35\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 19, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9, CAST(N'2025-10-20T23:02:00.097' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=20/10/2025 11:01:59\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 20, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9, CAST(N'2025-10-21T00:38:35.240' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=21/10/2025 12:38:34\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 21, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9, CAST(N'2025-10-21T00:59:26.547' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=21/10/2025 12:59:26\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 22, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9, CAST(N'2025-10-28T23:12:31.017' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=28/10/2025 11:12:30\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 23, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9, CAST(N'2025-10-28T23:43:42.080' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=28/10/2025 11:43:41\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 24, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.0', N'', 1, 9, CAST(N'2025-10-28T23:43:45.017' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=28/10/2025 11:43:45\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 25, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.1', N'', 1, 9, CAST(N'2025-11-12T05:07:15.930' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 5:07:15\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 26, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.49.1', N'', 1, 9, CAST(N'2025-11-12T05:24:16.620' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 5:24:16\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 27, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T05:36:31.617' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 5:36:31\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 28, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:16:40.767' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:16:40\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 29, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:19:51.083' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:19:51\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 30, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:28:25.497' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:28:25\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 31, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:31:19.877' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:31:19\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 32, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:31:40.603' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:31:40\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 33, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:31:45.853' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:31:45\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 34, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:32:02.463' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:32:02\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 35, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:32:13.493' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:32:13\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 36, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:32:16.603' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:32:16\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 37, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:32:43.837' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:32:43\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 38, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:32:45.370' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:32:45\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 39, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:33:11.390' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:33:11\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 40, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T06:33:49.773' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 6:33:49\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 41, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Unknown v0.0', N'Unknown v0.0 [Others]', N'Unknown', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'', N'', 1, 9, CAST(N'2025-11-12T07:02:12.267' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 7:02:12\u202Fa.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 42, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'es-CO', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-12T17:07:03.970' AS DateTime), N'[{"key":"LastAccess","value":"Before= 23/05/2025 2:16:31\u202Fp.\u00A0m. | After=12/11/2025 5:07:03\u202Fp.\u00A0m."}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 43, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-12T22:38:01.797' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/12/2025 10:38:01 PM"}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 44, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-13T00:46:48.897' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/13/2025 12:46:48 AM"}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 45, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-13T00:57:42.623' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/13/2025 12:57:42 AM"}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 46, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-13T01:26:29.447' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/13/2025 1:26:29 AM"}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 47, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-13T02:58:49.517' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/13/2025 2:58:49 AM"}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 48, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-13T04:51:25.817' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/13/2025 4:51:25 AM"}]')
GO
INSERT [dbo].[AuditRecords] ([CompanyFk], [Id], [UserFk], [EntityFk], [AuditTypeFk], [AuditEntityIndex], [HostName], [IpAddress], [DeviceType], [Browser], [Platform], [Engine], [CultureFk], [EndPointUrl], [Method], [QueryString], [UserAgent], [Referer], [ApplicationId], [RoleId], [CreationDate], [AuditChanges]) VALUES (2, 49, 6, 27, 703, N'6', N'localhost:7295', N'::1', N'Desktop', N'Others v0.0', N'Others v0.0 [Others]', N'Others', N'en-US', N'/api/Oauth2/v1/Token', N'POST', N'', N'PostmanRuntime/7.50.0', N'', 1, 9, CAST(N'2025-11-13T04:54:17.793' AS DateTime), N'[{"key":"LastAccess","value":"Before= 5/23/2025 2:16:31 PM | After=11/13/2025 4:54:17 AM"}]')
GO
SET IDENTITY_INSERT [dbo].[AuditRecords] OFF
GO
INSERT [dbo].[Companies] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CompanyClient], [CompanySecret], [CreationDate], [CreatedByUserFk], [Subdomain], [Email], [DefaultCultureFk], [CountryFk], [IsSystemCompany], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, N'Company_VitoTorresSoft', N'Company_VitoTorresSoft_Dsc', N'55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8', N'ba3a564f-946a-4b07-b44c-bf8ea21e808c', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, N'vito.torres.soft', N'eeatg844@hotmail.com', N'es-CO', N'CO', 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Companies] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CompanyClient], [CompanySecret], [CreationDate], [CreatedByUserFk], [Subdomain], [Email], [DefaultCultureFk], [CountryFk], [IsSystemCompany], [Avatar], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, N'Company_ProyectosLasTorresSAS', N'Company_ProyectosLasTorresSAS_Dsc', N'c5bcea98-2974-4d43-8110-28d402cf5ce2', N'8010e5e4-5f3f-4cd9-9d4c-b9babbecc740', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, N'proyectos-las-torres', N'proyectos-las-torres@hotmail.com', N'es-CO', N'CO', 0, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 1, 1, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 2, 1, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 3, 1, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 4, 1, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 5, 1, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 6, 1, 101, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 7, 2, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 8, 2, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 9, 2, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 10, 5, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 11, 5, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 12, 5, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 13, 27, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 14, 27, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (1, 15, 27, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 16, 2, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 17, 2, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 18, 2, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 19, 5, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 20, 5, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 21, 5, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 22, 27, 702, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 23, 27, 703, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyEntityAudits] ([CompanyFk], [Id], [EntityFk], [AuditTypeFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [IsActive]) VALUES (2, 24, 27, 704, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 1, 1, 1, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 2, 2, 21, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 3, 2, 22, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 4, 2, 23, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 5, 2, 24, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 6, 2, 25, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 7, 2, 26, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 8, 2, 27, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 9, 3, 31, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 10, 3, 32, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 11, 3, 33, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 12, 3, 34, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 13, 3, 35, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 14, 3, 36, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 15, 4, 41, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 16, 4, 42, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 17, 5, 51, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 18, 5, 52, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 19, 5, 53, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 20, 5, 54, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 21, 5, 55, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 22, 5, 56, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (1, 1, 1, 23, 5, 57, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 24, 6, 61, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 25, 7, 71, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 26, 7, 72, NULL, NULL)
GO
INSERT [dbo].[CompanyMembershipPermissions] ([CompanyMembershipFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue]) VALUES (2, 1, 2, 27, 8, 81, NULL, NULL)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, 1, 1, 5, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, 1, 2, 5, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (3, 2, 1, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[CompanyMemberships] ([Id], [CompanyFk], [ApplicationFk], [MembershipTypeFk], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (4, 2, 2, 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Components] ([ApplicationFk], [EndpointFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [ObjectId], [ObjectName], [ObjectPropertyName], [DefaultPropertyValue], [PositionIndex]) VALUES (1, 1, 1, N'VitoTransverse_ModuleMembership_PageApplications_NewApplicationButton', N'VitoTransverse_ModuleMembership_PageApplications_NewApplicationButton_Dsc', N'NewApplicationButton', N'NewApplicationButton', N'enabled', N'false', 0)
GO
INSERT [dbo].[Components] ([ApplicationFk], [EndpointFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [ObjectId], [ObjectName], [ObjectPropertyName], [DefaultPropertyValue], [PositionIndex]) VALUES (1, 2, 2, N'VitoTransverse_ModuleMembership_PageCompanies_NewCompanyButton', N'VitoTransverse_ModuleMembership_PageCompanies_NewCompanyButton_Dsc', N'NewCompanyButton', N'NewCompanyButton', N'enabled', N'false', 0)
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
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 1, 1, N'VitoTransverse_Api_V1_GET_applications', N'VitoTransverse_Api_V1_GET_applications_Dsc', N'api/applications/v1', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 2, 1, N'VitoTransverse_Api_V1_POST_applications', N'VitoTransverse_ModuleMembership_PageApplicationsList_Dsc', N'api/applications/v1', N'POST
', 1, 1, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 3, 3, N'VitoTransverse_Api_V1_PUT_applications/v1', N'VitoTransverse_Api_V1_PUT_applications/v1_Dsc', N'api/applications/v1', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 4, 4, N'VitoTransverse_Api_V1_GET_applications/v1/dropdown', N'VitoTransverse_Api_V1_GET_applications/v1/dropdown_Dsc', N'api/applications/v1/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 5, 5, N'VitoTransverse_Api_V1_GET_applications/v1/{applicationId}', N'VitoTransverse_Api_V1_GET_applications/v1/{applicationId}_Dsc', N'api/applications/v1/{applicationId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 6, 6, N'VitoTransverse_Api_V1_DELETE_applications/v1/Delete/{applicationId}', N'VitoTransverse_Api_V1_DELETE_applications/v1/Delete/{applicationId}_Dsc', N'api/applications/v1/Delete/{applicationId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 7, 7, N'VitoTransverse_Api_V1_GET_applications/v1/ByCompany/{companyId}', N'VitoTransverse_Api_V1_GET_applications/v1/ByCompany/{companyId}_Dsc', N'api/applications/v1/ByCompany/{companyId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 8, 8, N'VitoTransverse_Api_V1_GET_applications/v1/Roles/permissions', N'VitoTransverse_Api_V1_GET_applications/v1/Roles/permissions_Dsc', N'api/applications/v1/Roles/permissions', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 9, 9, N'VitoTransverse_Api_V1_GET_applications/v1/Roles/{applicationId}', N'VitoTransverse_Api_V1_GET_applications/v1/Roles/{applicationId}_Dsc', N'api/applications/v1/Roles/{applicationId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 10, 10, N'VitoTransverse_Api_V1_GET_applications/v1/Modules', N'VitoTransverse_Api_V1_GET_applications/v1/Modules_Dsc', N'api/applications/v1/Modules', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 11, 11, N'VitoTransverse_Api_V1_POST_applications/v1/Modules', N'VitoTransverse_Api_V1_POST_applications/v1/Modules_Dsc', N'api/applications/v1/Modules', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 12, 12, N'VitoTransverse_Api_V1_PUT_applications/v1/Modules', N'VitoTransverse_Api_V1_PUT_applications/v1/Modules_Dsc', N'api/applications/v1/Modules', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 13, 13, N'VitoTransverse_Api_V1_GET_applications/v1/Modules/dropdown', N'VitoTransverse_Api_V1_GET_applications/v1/Modules/dropdown_Dsc', N'api/applications/v1/Modules/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 14, 14, N'VitoTransverse_Api_V1_GET_applications/v1/Modules/{moduleId}', N'VitoTransverse_Api_V1_GET_applications/v1/Modules/{moduleId}_Dsc', N'api/applications/v1/Modules/{moduleId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 15, 15, N'VitoTransverse_Api_V1_DELETE_applications/v1/Modules/Delete/{moduleId}', N'VitoTransverse_Api_V1_DELETE_applications/v1/Modules/Delete/{moduleId}_Dsc', N'api/applications/v1/Modules/Delete/{moduleId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 16, 16, N'VitoTransverse_Api_V1_GET_applications/v1/Modules/{moduleId}/endpoints', N'VitoTransverse_Api_V1_GET_applications/v1/Modules/{moduleId}/endpoints_Dsc', N'api/applications/v1/Modules/{moduleId}/endpoints', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 17, 17, N'VitoTransverse_Api_V1_GET_applications/v1/Modules/{moduleId}/endpoints/dropdown', N'VitoTransverse_Api_V1_GET_applications/v1/Modules/{moduleId}/endpoints/dropdown_Dsc', N'api/applications/v1/Modules/{moduleId}/endpoints/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 18, 18, N'VitoTransverse_Api_V1_GET_Components', N'VitoTransverse_Api_V1_GET_Components_Dsc', N'api/applications/v1/endpoints/{endpointId}/Components', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 19, 19, N'VitoTransverse_Api_V1_GET_Components/dropdown', N'VitoTransverse_Api_V1_GET_Components/dropdown_Dsc', N'api/applications/v1/endpoints/{endpointId}/Components/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 20, 20, N'VitoTransverse_Api_V1_GET_applications/v1/Components/{componentId}', N'VitoTransverse_Api_V1_GET_applications/v1/Components/{componentId}_Dsc', N'api/applications/v1/Components/{componentId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 21, 21, N'VitoTransverse_Api_V1_POST_applications/v1/Components', N'VitoTransverse_Api_V1_POST_applications/v1/Components_Dsc', N'api/applications/v1/Components', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 22, 22, N'VitoTransverse_Api_V1_PUT_applications/v1/Components', N'VitoTransverse_Api_V1_PUT_applications/v1/Components_Dsc', N'api/applications/v1/Components', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 23, 23, N'VitoTransverse_Api_V1_DELETE_applications/v1/Components/Delete/{componentId}', N'VitoTransverse_Api_V1_DELETE_applications/v1/Components/Delete/{componentId}_Dsc', N'api/applications/v1/Components/Delete/{componentId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 24, 24, N'VitoTransverse_Api_V1_GET_applications/v1/endpoints/{endpointId}', N'VitoTransverse_Api_V1_GET_applications/v1/endpoints/{endpointId}_Dsc', N'api/applications/v1/endpoints/{endpointId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 25, 25, N'VitoTransverse_Api_V1_POST_applications/v1/endpoints', N'VitoTransverse_Api_V1_POST_applications/v1/endpoints_Dsc', N'api/applications/v1/endpoints', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 26, 26, N'VitoTransverse_Api_V1_PUT_applications/v1/endpoints', N'VitoTransverse_Api_V1_PUT_applications/v1/endpoints_Dsc', N'api/applications/v1/endpoints', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, 27, 27, N'VitoTransverse_Api_V1_DELETE_applications/v1/endpoints/Delete/{endpointId}', N'VitoTransverse_Api_V1_DELETE_applications/v1/endpoints/Delete/{endpointId}_Dsc', N'api/applications/v1/endpoints/Delete/{endpointId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 28, 1, N'VitoTransverse_Api_V1_GET_Auditories/v1/AuditRecords', N'VitoTransverse_Api_V1_GET_Auditories/v1/AuditRecords_Dsc', N'api/Auditories/v1/AuditRecords', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 29, 2, N'VitoTransverse_Api_V1_GET_CompanyEntityAudits', N'VitoTransverse_Api_V1_GET_CompanyEntityAudits_Dsc', N'api/Auditories/v1/CompanyEntityAudits', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 30, 3, N'VitoTransverse_Api_V1_POST_Auditories/v1/CompanyEntityAudits', N'VitoTransverse_Api_V1_POST_Auditories/v1/CompanyEntityAudits_Dsc', N'api/Auditories/v1/CompanyEntityAudits', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 31, 4, N'VitoTransverse_Api_V1_PUT_Auditories/v1/CompanyEntityAudits', N'VitoTransverse_Api_V1_PUT_Auditories/v1/CompanyEntityAudits_Dsc', N'api/Auditories/v1/CompanyEntityAudits', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 32, 5, N'VitoTransverse_Api_V1_GET_CompanyEntityAudits', N'VitoTransverse_Api_V1_GET_CompanyEntityAudits_Dsc', N'api/Auditories/v1/CompanyEntityAudits/{companyEntityAuditId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 33, 6, N'VitoTransverse_Api_V1_DELETE_CompanyEntityAudits', N'VitoTransverse_Api_V1_DELETE_CompanyEntityAudits_Dsc', N'api/Auditories/v1/CompanyEntityAudits/Delete/{companyEntityAuditId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 34, 7, N'VitoTransverse_Api_V1_GET_Auditories/v1/ActivityLogs', N'VitoTransverse_Api_V1_GET_Auditories/v1/ActivityLogs_Dsc', N'api/Auditories/v1/ActivityLogs', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 35, 8, N'VitoTransverse_Api_V1_GET_Auditories/v1/Notifications', N'VitoTransverse_Api_V1_GET_Auditories/v1/Notifications_Dsc', N'api/Auditories/v1/Notifications', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 36, 9, N'VitoTransverse_Api_V1_GET_Auditories/v1/Entities', N'VitoTransverse_Api_V1_GET_Auditories/v1/Entities_Dsc', N'api/Auditories/v1/Entities', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 37, 10, N'VitoTransverse_Api_V1_POST_Auditories/v1/Entities', N'VitoTransverse_Api_V1_POST_Auditories/v1/Entities_Dsc', N'api/Auditories/v1/Entities', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 38, 11, N'VitoTransverse_Api_V1_PUT_Auditories/v1/Entities', N'VitoTransverse_Api_V1_PUT_Auditories/v1/Entities_Dsc', N'api/Auditories/v1/Entities', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 39, 12, N'VitoTransverse_Api_V1_GET_Auditories/v1/Entities/dropdown', N'VitoTransverse_Api_V1_GET_Auditories/v1/Entities/dropdown_Dsc', N'api/Auditories/v1/Entities/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 40, 13, N'VitoTransverse_Api_V1_GET_Auditories/v1/Entities/{entityId}', N'VitoTransverse_Api_V1_GET_Auditories/v1/Entities/{entityId}_Dsc', N'api/Auditories/v1/Entities/{entityId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, 41, 14, N'VitoTransverse_Api_V1_DELETE_Auditories/v1/Entities/Delete/{entityId}', N'VitoTransverse_Api_V1_DELETE_Auditories/v1/Entities/Delete/{entityId}_Dsc', N'api/Auditories/v1/Entities/Delete/{entityId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 42, 1, N'VitoTransverse_Api_V1_GET_Cache/v1', N'VitoTransverse_Api_V1_GET_Cache/v1_Dsc', N'api/Cache/v1', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 43, 2, N'VitoTransverse_Api_V1_DELETE_Cache/v1/clear', N'VitoTransverse_Api_V1_DELETE_Cache/v1/clear_Dsc', N'api/Cache/v1/clear', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, 44, 3, N'VitoTransverse_Api_V1_DELETE_Cache/v1/delete/{cacheKey}', N'VitoTransverse_Api_V1_DELETE_Cache/v1/delete/{cacheKey}_Dsc', N'api/Cache/v1/delete/{cacheKey}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 45, 1, N'VitoTransverse_Api_V1_GET_Companies/v1', N'VitoTransverse_Api_V1_GET_Companies/v1_Dsc', N'api/Companies/v1', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 46, 2, N'VitoTransverse_Api_V1_POST_Companies/v1', N'VitoTransverse_Api_V1_POST_Companies/v1_Dsc', N'api/Companies/v1', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 47, 3, N'VitoTransverse_Api_V1_PUT_Companies/v1', N'VitoTransverse_Api_V1_PUT_Companies/v1_Dsc', N'api/Companies/v1', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 48, 4, N'VitoTransverse_Api_V1_GET_Companies/v1/dropdown', N'VitoTransverse_Api_V1_GET_Companies/v1/dropdown_Dsc', N'api/Companies/v1/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 49, 5, N'VitoTransverse_Api_V1_GET_Companies/v1/{companyId}', N'VitoTransverse_Api_V1_GET_Companies/v1/{companyId}_Dsc', N'api/Companies/v1/{companyId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 50, 6, N'VitoTransverse_Api_V1_DELETE_Companies/v1/Delete/{companyId}', N'VitoTransverse_Api_V1_DELETE_Companies/v1/Delete/{companyId}_Dsc', N'api/Companies/v1/Delete/{companyId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 51, 7, N'VitoTransverse_Api_V1_GET_Companies/v1/Memberships', N'VitoTransverse_Api_V1_GET_Companies/v1/Memberships_Dsc', N'api/Companies/v1/Memberships', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 52, 8, N'VitoTransverse_Api_V1_PUT_Companies/v1/Memberships', N'VitoTransverse_Api_V1_PUT_Companies/v1/Memberships_Dsc', N'api/Companies/v1/Memberships', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 53, 9, N'VitoTransverse_Api_V1_GET_Companies/v1/Memberships/dropdown', N'VitoTransverse_Api_V1_GET_Companies/v1/Memberships/dropdown_Dsc', N'api/Companies/v1/Memberships/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 54, 10, N'VitoTransverse_Api_V1_GET_Companies/v1/Memberships/{membershipId}', N'VitoTransverse_Api_V1_GET_Companies/v1/Memberships/{membershipId}_Dsc', N'api/Companies/v1/Memberships/{membershipId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 55, 11, N'VitoTransverse_Api_V1_DELETE_Companies/v1/Memberships/Delete/{membershipId}', N'VitoTransverse_Api_V1_DELETE_Companies/v1/Memberships/Delete/{membershipId}_Dsc', N'api/Companies/v1/Memberships/Delete/{membershipId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 56, 12, N'VitoTransverse_Api_V1_GET_Companies/v1/MembershipTypes', N'VitoTransverse_Api_V1_GET_Companies/v1/MembershipTypes_Dsc', N'api/Companies/v1/MembershipTypes', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 57, 13, N'VitoTransverse_Api_V1_POST_Companies/v1/MembershipTypes', N'VitoTransverse_Api_V1_POST_Companies/v1/MembershipTypes_Dsc', N'api/Companies/v1/MembershipTypes', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 58, 14, N'VitoTransverse_Api_V1_PUT_Companies/v1/MembershipTypes', N'VitoTransverse_Api_V1_PUT_Companies/v1/MembershipTypes_Dsc', N'api/Companies/v1/MembershipTypes', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 59, 15, N'VitoTransverse_Api_V1_GET_Companies/v1/MembershipTypes/dropdown', N'VitoTransverse_Api_V1_GET_Companies/v1/MembershipTypes/dropdown_Dsc', N'api/Companies/v1/MembershipTypes/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 60, 16, N'VitoTransverse_Api_V1_GET_Companies/v1/MembershipTypes/{membershipTypeId}', N'VitoTransverse_Api_V1_GET_Companies/v1/MembershipTypes/{membershipTypeId}_Dsc', N'api/Companies/v1/MembershipTypes/{membershipTypeId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, 61, 17, N'VitoTransverse_Api_V1_DELETE_MembershipTypes', N'VitoTransverse_Api_V1_DELETE_MembershipTypes_Dsc', N'api/Companies/v1/MembershipTypes/Delete/{membershipTypeId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 62, 1, N'VitoTransverse_Api_V1_GET_Health/v1', N'VitoTransverse_Api_V1_GET_Health/v1_Dsc', N'api/Health/v1', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 63, 2, N'VitoTransverse_Api_V1_GET_Health/v1/cache', N'VitoTransverse_Api_V1_GET_Health/v1/cache_Dsc', N'api/Health/v1/cache', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 64, 3, N'VitoTransverse_Api_V1_GET_Health/v1/database', N'VitoTransverse_Api_V1_GET_Health/v1/database_Dsc', N'api/Health/v1/database', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 65, 4, N'VitoTransverse_Api_V1_GET_Health/v1/Detect', N'VitoTransverse_Api_V1_GET_Health/v1/Detect_Dsc', N'api/Health/v1/Detect', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, 66, 5, N'VitoTransverse_Api_V1_GET_Health/v1/Ping', N'VitoTransverse_Api_V1_GET_Health/v1/Ping_Dsc', N'api/Health/v1/Ping', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 67, 1, N'VitoTransverse_Api_V1_GET_Localizations/v1', N'VitoTransverse_Api_V1_GET_Localizations/v1_Dsc', N'api/Localizations/v1', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 68, 2, N'VitoTransverse_Api_V1_POST_Localizations/v1', N'VitoTransverse_Api_V1_POST_Localizations/v1_Dsc', N'api/Localizations/v1', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 69, 3, N'VitoTransverse_Api_V1_PUT_Localizations/v1', N'VitoTransverse_Api_V1_PUT_Localizations/v1_Dsc', N'api/Localizations/v1', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 70, 4, N'VitoTransverse_Api_V1_GET_Localizations/v1/{messageKey}/All', N'VitoTransverse_Api_V1_GET_Localizations/v1/{messageKey}/All_Dsc', N'api/Localizations/v1/{messageKey}/All', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 71, 5, N'VitoTransverse_Api_V1_GET_Localizations/v1/ByCulture/{cultureId}', N'VitoTransverse_Api_V1_GET_Localizations/v1/ByCulture/{cultureId}_Dsc', N'api/Localizations/v1/ByCulture/{cultureId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 72, 6, N'VitoTransverse_Api_V1_GET_Localizations/v1/{messageKey}/WithParams', N'VitoTransverse_Api_V1_GET_Localizations/v1/{messageKey}/WithParams_Dsc', N'api/Localizations/v1/{messageKey}/WithParams', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 73, 7, N'VitoTransverse_Api_V1_GET_Localizations/v1/{messageKey}', N'VitoTransverse_Api_V1_GET_Localizations/v1/{messageKey}_Dsc', N'api/Localizations/v1/{messageKey}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, 74, 8, N'VitoTransverse_Api_V1_DELETE_Localizations/v1/Delete', N'VitoTransverse_Api_V1_DELETE_Localizations/v1/Delete_Dsc', N'api/Localizations/v1/Delete', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 75, 1, N'VitoTransverse_Api_V1_GET_Master/v1/Secuences', N'VitoTransverse_Api_V1_GET_Master/v1/Secuences_Dsc', N'api/Master/v1/Secuences', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 76, 2, N'VitoTransverse_Api_V1_POST_Master/v1/Secuences', N'VitoTransverse_Api_V1_POST_Master/v1/Secuences_Dsc', N'api/Master/v1/Secuences', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 77, 3, N'VitoTransverse_Api_V1_PUT_Master/v1/Secuences', N'VitoTransverse_Api_V1_PUT_Master/v1/Secuences_Dsc', N'api/Master/v1/Secuences', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 78, 4, N'VitoTransverse_Api_V1_GET_Master/v1/Secuences/{secuenceId}', N'VitoTransverse_Api_V1_GET_Master/v1/Secuences/{secuenceId}_Dsc', N'api/Master/v1/Secuences/{secuenceId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 79, 5, N'VitoTransverse_Api_V1_DELETE_Master/v1/Secuences/Delete/{secuenceId}', N'VitoTransverse_Api_V1_DELETE_Master/v1/Secuences/Delete/{secuenceId}_Dsc', N'api/Master/v1/Secuences/Delete/{secuenceId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 80, 6, N'VitoTransverse_Api_V1_GET_Master/v1/Cultures', N'VitoTransverse_Api_V1_GET_Master/v1/Cultures_Dsc', N'api/Master/v1/Cultures', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 81, 7, N'VitoTransverse_Api_V1_POST_Master/v1/Cultures', N'VitoTransverse_Api_V1_POST_Master/v1/Cultures_Dsc', N'api/Master/v1/Cultures', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 82, 8, N'VitoTransverse_Api_V1_PUT_Master/v1/Cultures', N'VitoTransverse_Api_V1_PUT_Master/v1/Cultures_Dsc', N'api/Master/v1/Cultures', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 83, 9, N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/dropdown', N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/dropdown_Dsc', N'api/Master/v1/Cultures/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 84, 10, N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/UtcDate', N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/UtcDate_Dsc', N'api/Master/v1/Cultures/UtcDate', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 85, 11, N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/Active', N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/Active_Dsc', N'api/Master/v1/Cultures/Active', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 86, 12, N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/Active/DropDown', N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/Active/DropDown_Dsc', N'api/Master/v1/Cultures/Active/DropDown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 87, 13, N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/{cultureId}', N'VitoTransverse_Api_V1_GET_Master/v1/Cultures/{cultureId}_Dsc', N'api/Master/v1/Cultures/{cultureId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 88, 14, N'VitoTransverse_Api_V1_DELETE_Master/v1/Cultures/Delete/{cultureId}', N'VitoTransverse_Api_V1_DELETE_Master/v1/Cultures/Delete/{cultureId}_Dsc', N'api/Master/v1/Cultures/Delete/{cultureId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 89, 15, N'VitoTransverse_Api_V1_GET_Master/v1/Languages', N'VitoTransverse_Api_V1_GET_Master/v1/Languages_Dsc', N'api/Master/v1/Languages', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 90, 16, N'VitoTransverse_Api_V1_POST_Master/v1/Languages', N'VitoTransverse_Api_V1_POST_Master/v1/Languages_Dsc', N'api/Master/v1/Languages', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 91, 17, N'VitoTransverse_Api_V1_PUT_Master/v1/Languages', N'VitoTransverse_Api_V1_PUT_Master/v1/Languages_Dsc', N'api/Master/v1/Languages', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 92, 18, N'VitoTransverse_Api_V1_GET_Master/v1/Languages/dropdown', N'VitoTransverse_Api_V1_GET_Master/v1/Languages/dropdown_Dsc', N'api/Master/v1/Languages/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 93, 19, N'VitoTransverse_Api_V1_GET_Master/v1/Languages/{languageId}', N'VitoTransverse_Api_V1_GET_Master/v1/Languages/{languageId}_Dsc', N'api/Master/v1/Languages/{languageId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 94, 20, N'VitoTransverse_Api_V1_DELETE_Master/v1/Languages/Delete/{languageId}', N'VitoTransverse_Api_V1_DELETE_Master/v1/Languages/Delete/{languageId}_Dsc', N'api/Master/v1/Languages/Delete/{languageId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 95, 21, N'VitoTransverse_Api_V1_GET_Master/v1/Countries', N'VitoTransverse_Api_V1_GET_Master/v1/Countries_Dsc', N'api/Master/v1/Countries', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 96, 22, N'VitoTransverse_Api_V1_POST_Master/v1/Countries', N'VitoTransverse_Api_V1_POST_Master/v1/Countries_Dsc', N'api/Master/v1/Countries', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 97, 23, N'VitoTransverse_Api_V1_PUT_Master/v1/Countries', N'VitoTransverse_Api_V1_PUT_Master/v1/Countries_Dsc', N'api/Master/v1/Countries', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 98, 24, N'VitoTransverse_Api_V1_GET_Master/v1/Countries/dropdown', N'VitoTransverse_Api_V1_GET_Master/v1/Countries/dropdown_Dsc', N'api/Master/v1/Countries/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 99, 25, N'VitoTransverse_Api_V1_GET_Master/v1/Countries/{countryId}', N'VitoTransverse_Api_V1_GET_Master/v1/Countries/{countryId}_Dsc', N'api/Master/v1/Countries/{countryId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 100, 26, N'VitoTransverse_Api_V1_DELETE_Master/v1/Countries/Delete/{countryId}', N'VitoTransverse_Api_V1_DELETE_Master/v1/Countries/Delete/{countryId}_Dsc', N'api/Master/v1/Countries/Delete/{countryId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 101, 27, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeGroups', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeGroups_Dsc', N'api/Master/v1/GeneralTypeGroups', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 102, 28, N'VitoTransverse_Api_V1_POST_Master/v1/GeneralTypeGroups', N'VitoTransverse_Api_V1_POST_Master/v1/GeneralTypeGroups_Dsc', N'api/Master/v1/GeneralTypeGroups', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 103, 29, N'VitoTransverse_Api_V1_PUT_Master/v1/GeneralTypeGroups', N'VitoTransverse_Api_V1_PUT_Master/v1/GeneralTypeGroups_Dsc', N'api/Master/v1/GeneralTypeGroups', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 104, 30, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeGroups/dropdown', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeGroups/dropdown_Dsc', N'api/Master/v1/GeneralTypeGroups/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 105, 31, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeGroups/{generalTypeGroupId}', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeGroups/{generalTypeGroupId}_Dsc', N'api/Master/v1/GeneralTypeGroups/{generalTypeGroupId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 106, 32, N'VitoTransverse_Api_V1_DELETE_GeneralTypeGroups', N'VitoTransverse_Api_V1_DELETE_GeneralTypeGroups_Dsc', N'api/Master/v1/GeneralTypeGroups/Delete/{generalTypeGroupId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 107, 33, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems_Dsc', N'api/Master/v1/GeneralTypeItems', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 108, 34, N'VitoTransverse_Api_V1_POST_Master/v1/GeneralTypeItems', N'VitoTransverse_Api_V1_POST_Master/v1/GeneralTypeItems_Dsc', N'api/Master/v1/GeneralTypeItems', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 109, 35, N'VitoTransverse_Api_V1_PUT_Master/v1/GeneralTypeItems', N'VitoTransverse_Api_V1_PUT_Master/v1/GeneralTypeItems_Dsc', N'api/Master/v1/GeneralTypeItems', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 110, 36, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems/dropdown/{groupId}', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems/dropdown/{groupId}_Dsc', N'api/Master/v1/GeneralTypeItems/dropdown/{groupId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 111, 37, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems/ByGroupId/{groupId}', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems/ByGroupId/{groupId}_Dsc', N'api/Master/v1/GeneralTypeItems/ByGroupId/{groupId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 112, 38, N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems/{generalTypeItemId}', N'VitoTransverse_Api_V1_GET_Master/v1/GeneralTypeItems/{generalTypeItemId}_Dsc', N'api/Master/v1/GeneralTypeItems/{generalTypeItemId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 113, 39, N'VitoTransverse_Api_V1_DELETE_GeneralTypeItems', N'VitoTransverse_Api_V1_DELETE_GeneralTypeItems_Dsc', N'api/Master/v1/GeneralTypeItems/Delete/{generalTypeItemId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 114, 40, N'VitoTransverse_Api_V1_GET_Master/v1/NotificationTemplates', N'VitoTransverse_Api_V1_GET_Master/v1/NotificationTemplates_Dsc', N'api/Master/v1/NotificationTemplates', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 115, 41, N'VitoTransverse_Api_V1_POST_Master/v1/NotificationTemplates', N'VitoTransverse_Api_V1_POST_Master/v1/NotificationTemplates_Dsc', N'api/Master/v1/NotificationTemplates', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 116, 42, N'VitoTransverse_Api_V1_PUT_Master/v1/NotificationTemplates', N'VitoTransverse_Api_V1_PUT_Master/v1/NotificationTemplates_Dsc', N'api/Master/v1/NotificationTemplates', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 117, 43, N'VitoTransverse_Api_V1_GET_Master/v1/NotificationTemplates/dropdown', N'VitoTransverse_Api_V1_GET_Master/v1/NotificationTemplates/dropdown_Dsc', N'api/Master/v1/NotificationTemplates/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 118, 44, N'VitoTransverse_Api_V1_GET_NotificationTemplates', N'VitoTransverse_Api_V1_GET_NotificationTemplates_Dsc', N'api/Master/v1/NotificationTemplates/{notificationTemplateId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, 119, 45, N'VitoTransverse_Api_V1_DELETE_NotificationTemplates', N'VitoTransverse_Api_V1_DELETE_NotificationTemplates_Dsc', N'api/Master/v1/NotificationTemplates/Delete/{notificationTemplateId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 120, 1, N'VitoTransverse_Api_V1_GET_Media/v1/pictures', N'VitoTransverse_Api_V1_GET_Media/v1/pictures_Dsc', N'api/Media/v1/pictures', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 121, 2, N'VitoTransverse_Api_V1_POST_Media/v1/pictures', N'VitoTransverse_Api_V1_POST_Media/v1/pictures_Dsc', N'api/Media/v1/pictures', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 122, 3, N'VitoTransverse_Api_V1_PUT_Media/v1/pictures', N'VitoTransverse_Api_V1_PUT_Media/v1/pictures_Dsc', N'api/Media/v1/pictures', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 123, 4, N'VitoTransverse_Api_V1_GET_Media/v1/pictures/ByName/{pictureName}', N'VitoTransverse_Api_V1_GET_Media/v1/pictures/ByName/{pictureName}_Dsc', N'api/Media/v1/pictures/ByName/{pictureName}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 124, 5, N'VitoTransverse_Api_V1_GET_Media/v1/pictures/ByNameWildCard/{pictureNameWildCard}', N'VitoTransverse_Api_V1_GET_Media/v1/pictures/ByNameWildCard/{pictureNameWildCard}_Dsc', N'api/Media/v1/pictures/ByNameWildCard/{pictureNameWildCard}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 125, 6, N'VitoTransverse_Api_V1_GET_Media/v1/pictures/{pictureId}', N'VitoTransverse_Api_V1_GET_Media/v1/pictures/{pictureId}_Dsc', N'api/Media/v1/pictures/{pictureId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, 126, 7, N'VitoTransverse_Api_V1_DELETE_Media/v1/pictures/Delete/{pictureId}', N'VitoTransverse_Api_V1_DELETE_Media/v1/pictures/Delete/{pictureId}_Dsc', N'api/Media/v1/pictures/Delete/{pictureId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 9, 127, 1, N'VitoTransverse_Api_V1_POST_Oauth2/v1/Token', N'VitoTransverse_Api_V1_POST_Oauth2/v1/Token_Dsc', N'api/Oauth2/v1/Token', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 128, 1, N'VitoTransverse_Api_V1_GET_Users/v1', N'VitoTransverse_Api_V1_GET_Users/v1_Dsc', N'api/Users/v1', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 129, 2, N'VitoTransverse_Api_V1_POST_Users/v1', N'VitoTransverse_Api_V1_POST_Users/v1_Dsc', N'api/Users/v1', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 130, 3, N'VitoTransverse_Api_V1_PUT_Users/v1', N'VitoTransverse_Api_V1_PUT_Users/v1_Dsc', N'api/Users/v1', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 131, 4, N'VitoTransverse_Api_V1_GET_Users/v1/dropdown', N'VitoTransverse_Api_V1_GET_Users/v1/dropdown_Dsc', N'api/Users/v1/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 132, 5, N'VitoTransverse_Api_V1_GET_Users/v1/{userId}', N'VitoTransverse_Api_V1_GET_Users/v1/{userId}_Dsc', N'api/Users/v1/{userId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 133, 6, N'VitoTransverse_Api_V1_DELETE_Users/v1/Delete/{userId}', N'VitoTransverse_Api_V1_DELETE_Users/v1/Delete/{userId}_Dsc', N'api/Users/v1/Delete/{userId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 134, 7, N'VitoTransverse_Api_V1_PATCH_Users/v1/password', N'VitoTransverse_Api_V1_PATCH_Users/v1/password_Dsc', N'api/Users/v1/password', N'PATCH', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 135, 8, N'VitoTransverse_Api_V1_GET_Users/v1/activate/{activationToken}', N'VitoTransverse_Api_V1_GET_Users/v1/activate/{activationToken}_Dsc', N'api/Users/v1/activate/{activationToken}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 136, 9, N'VitoTransverse_Api_V1_GET_Users/v1/SendActivationEmail/{userId}', N'VitoTransverse_Api_V1_GET_Users/v1/SendActivationEmail/{userId}_Dsc', N'api/Users/v1/SendActivationEmail/{userId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 137, 10, N'VitoTransverse_Api_V1_GET_Users/v1/UserRoles', N'VitoTransverse_Api_V1_GET_Users/v1/UserRoles_Dsc', N'api/Users/v1/UserRoles', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 138, 11, N'VitoTransverse_Api_V1_POST_Users/v1/UserRoles', N'VitoTransverse_Api_V1_POST_Users/v1/UserRoles_Dsc', N'api/Users/v1/UserRoles', N'POST', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 139, 12, N'VitoTransverse_Api_V1_PUT_Users/v1/UserRoles', N'VitoTransverse_Api_V1_PUT_Users/v1/UserRoles_Dsc', N'api/Users/v1/UserRoles', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 140, 13, N'VitoTransverse_Api_V1_GET_Users/v1/UserRoles/{userId}/{roleId}', N'VitoTransverse_Api_V1_GET_Users/v1/UserRoles/{userId}/{roleId}_Dsc', N'api/Users/v1/UserRoles/{userId}/{roleId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 141, 14, N'VitoTransverse_Api_V1_DELETE_Users/v1/UserRoles/Delete/{userId}/{roleId}', N'VitoTransverse_Api_V1_DELETE_Users/v1/UserRoles/Delete/{userId}/{roleId}_Dsc', N'api/Users/v1/UserRoles/Delete/{userId}/{roleId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 142, 15, N'VitoTransverse_Api_V1_GET_Users/v1/Permissions/{userId}', N'VitoTransverse_Api_V1_GET_Users/v1/Permissions/{userId}_Dsc', N'api/Users/v1/Permissions/{userId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 143, 16, N'VitoTransverse_Api_V1_GET_Users/v1/Roles', N'VitoTransverse_Api_V1_GET_Users/v1/Roles_Dsc', N'api/Users/v1/Roles', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 144, 17, N'VitoTransverse_Api_V1_PUT_Users/v1/Roles', N'VitoTransverse_Api_V1_PUT_Users/v1/Roles_Dsc', N'api/Users/v1/Roles', N'PUT', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 145, 18, N'VitoTransverse_Api_V1_GET_Users/v1/Roles/dropdown', N'VitoTransverse_Api_V1_GET_Users/v1/Roles/dropdown_Dsc', N'api/Users/v1/Roles/dropdown', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 146, 19, N'VitoTransverse_Api_V1_GET_Users/v1/Roles/{roleId}', N'VitoTransverse_Api_V1_GET_Users/v1/Roles/{roleId}_Dsc', N'api/Users/v1/Roles/{roleId}', N'GET', 1, 0, 1)
GO
INSERT [dbo].[Endpoints] ([ApplicationFk], [ModuleFk], [Id], [PositionIndex], [NameTranslationKey], [DescriptionTranslationKey], [EndpointUrl], [Method], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, 147, 20, N'VitoTransverse_Api_V1_DELETE_Users/v1/Roles/Delete/{roleId}', N'VitoTransverse_Api_V1_DELETE_Users/v1/Roles/Delete/{roleId}_Dsc', N'api/Users/v1/Roles/Delete/{roleId}', N'DELETE', 1, 0, 1)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (1, N'dbo', N'ActivityLogs', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (2, N'dbo', N'Applications', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (3, N'dbo', N'AuditEntities', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (4, N'dbo', N'AuditRecords', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (5, N'dbo', N'Companies', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (6, N'dbo', N'CompanyEntityAudits', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (7, N'dbo', N'CompanyMembershipPermissions', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (8, N'dbo', N'CompanyMemberships', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (9, N'dbo', N'Components', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (10, N'dbo', N'Countries', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (11, N'dbo', N'Cultures', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (12, N'dbo', N'CultureTranslations', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (13, N'dbo', N'GeneralTypeGroups', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (14, N'dbo', N'GeneralTypeItems', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (15, N'dbo', N'Languages', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (16, N'dbo', N'MembershipTypes', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (17, N'dbo', N'MembersipPriceHistory', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (18, N'dbo', N'Modules', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (19, N'dbo', N'Notifications', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (20, N'dbo', N'NotificationTemplates', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (21, N'dbo', N'Pages', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (22, N'dbo', N'RolePermissions', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (23, N'dbo', N'Roles', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (24, N'dbo', N'Sequences', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (25, N'dbo', N'sysdiagrams', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (26, N'dbo', N'UserRoles', 1, 0)
GO
INSERT [dbo].[Entities] ([Id], [SchemaName], [EntityName], [IsActive], [IsSystemEntity]) VALUES (27, N'dbo', N'Users', 1, 0)
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
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (8, N'GeneralType_FileType', 1)
GO
INSERT [dbo].[GeneralTypeGroups] ([Id], [NameTranslationKey], [IsSystemType]) VALUES (9, N'GeneralType_PictureCategoryType', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (1, 1, 101, N'NotificationType_Email', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (1, 2, 102, N'NotificationType_SMS', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 1, 201, N'DocumentType_BornRegistry', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 2, 202, N'DocumentType_DNI', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 3, 203, N'DocumentType_ForeingDNI', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 4, 204, N'DocumentType_CompanyId', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (2, 5, 205, N'DocumentType_Passport', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (3, 1, 301, N'GenderList_Undefined', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (3, 2, 302, N'GenderList_Female', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (3, 3, 303, N'GenderList_Male', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 1, 401, N'OAuthActionType_Undefined', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 2, 402, N'OAuthActionType_CreateNewApplication', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 3, 403, N'OAuthActionType_CreateNewCompany', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 5, 404, N'OAuthActionType_CreateNewUser', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 6, 405, N'OAuthActionType_SendActivationEmail', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 7, 406, N'OAuthActionType_ActivateUser', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 8, 407, N'OAuthActionType_LoginFail_Company_ClientOrSecretNotFound', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 14, 408, N'OAuthActionType_LoginFail_CompanyMembershipNotFound ', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 10, 409, N'OAuthActionType_LoginFail_Application_ClientOrSecretNoFound', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 12, 410, N'OAuthActionType_LoginFail_User_LoginOrPasswordInvalid', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 18, 411, N'OAuthActionType_LoginFail_UserUnauthorized', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 15, 412, N'OAuthActionType_LoginSuccessByAuthorizationCode', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 16, 413, N'OAuthActionType_LoginSuccessByClientCredentials', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 17, 414, N'OAuthActionType_ChangeUserPassword', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 19, 415, N'OAuthActionType_Logoff', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 20, 416, N'OAuthActionType_ClearCache', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 21, 417, N'OAuthActionType_ApiRequestUnauthorized', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (4, 22, 418, N'OAuthActionType_ApiRequestSuccessfully', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (5, 1, 501, N'LocationType_State', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (5, 2, 502, N'LocationType_City', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (5, 3, 503, N'LocationType_Neighborhood', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (6, 1, 601, N'SequenceType_RoleName', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 1, 701, N'EntityAuditType_Read', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 2, 702, N'EntityAuditType_AddRow', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 3, 703, N'EntityAuditType_UpdateRow', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (7, 4, 704, N'EntityAuditType_DeleteRow', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (8, 1, 801, N'FileType_Png', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (8, 2, 802, N'FileType_Gif', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (8, 3, 803, N'FileType_Jpg', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 1, 901, N'PictureCategoryType_System', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 2, 902, N'PictureCategoryType_Project', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 3, 903, N'PictureCategoryType_Property', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 4, 904, N'PictureCategoryType_ProjectRoom', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 5, 905, N'PictureCategoryTypePropertyRoom', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 6, 906, N'PictureCategoryType_Company', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 7, 907, N'PictureCategoryType_Sponsor', 1)
GO
INSERT [dbo].[GeneralTypeItems] ([ListItemGroupFk], [OrderIndex], [Id], [NameTranslationKey], [IsEnabled]) VALUES (9, 8, 908, N'PictureCategoryType_PageIcon', 1)
GO
INSERT [dbo].[Languages] ([Id], [NameTranslationKey]) VALUES (N'en', N'Language_English')
GO
INSERT [dbo].[Languages] ([Id], [NameTranslationKey]) VALUES (N'es', N'Language_Spanish')
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (1, N'Membership_Free', N'Membership_Free_Dsc', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (2, N'Membership_Standard', N'Membership_Standard_Dsc', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (3, N'Membership_Enterprise', N'Membership_Enterprise_Dsc', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (4, N'Membership_Prime', N'Membership_Prime_Dsc', CAST(N'2025-04-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-04-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[MembershipTypes] ([Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [StartDate], [EndDate], [LastUpdateDate], [LastUpdateByUserFk], [IsActive]) VALUES (5, N'Membership_Owner', N'Membership_Owner_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 1, N'VitoTransverse_ApiApplication', N'VitoTransverse_ApiApplication_Dsc', 1, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 2, N'VitoTransverse_ApiAudit', N'VitoTransverse_ApiAudit_Dsc', 2, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 3, N'VitoTransverse_ApiCache', N'VitoTransverse_ApiCache_Dsc', 3, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 4, N'VitoTransverse_ApiCompanies', N'VitoTransverse_ApiCompanies_Dsc', 4, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 5, N'VitoTransverse_ApiHealth', N'VitoTransverse_ApiHealth_Dsc', 5, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 6, N'VitoTransverse_ApiLocalization', N'VitoTransverse_ApiLocalization_Dsc', 6, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 7, N'VitoTransverse_ApiMaster', N'VitoTransverse_ApiMaster_Dsc', 7, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 8, N'VitoTransverse_ApiMEdia', N'VitoTransverse_ApiMedia_Dsc', 8, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 9, N'VitoTransverse_ApiOAuth2', N'VitoTransverse_ApiOAuth2_Dsc', 9, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 10, N'VitoTransverse_ApiUsers', N'VitoTransverse_ApiUsers_Dsc', 10, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (1, 11, N'VitoTransverse_ApiX', N'VitoTransverse_ApiX_Dsc', 11, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 12, N'VitoRealState_Api1', N'VitoRealState_Api1_Dsc', 1, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 13, N'VitoRealState_Api2', N'VitoRealState_Api2_Dsc', 2, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 14, N'VitoRealState_Api3', N'VitoRealState_Api3_Dsc', 3, 1, 0, 1)
GO
INSERT [dbo].[Modules] ([ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [PositionIndex], [IsActive], [IsVisible], [IsApi]) VALUES (2, 15, N'VitoRealState_Api4', N'VitoRealState_Api4_Dsc', 4, 1, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 
GO
INSERT [dbo].[Notifications] ([CompanyFk], [NotificationTemplateGroupFk], [CultureFk], [NotificationTypeFk], [Id], [CreationDate], [Sender], [Receiver], [CC], [BCC], [Subject], [Message], [IsSent], [SentDate], [IsHtml]) VALUES (1, 1, N'es-CO', 101, 44, CAST(N'2025-05-21T10:41:57.763' AS DateTime), N'eeatg844@gmail.com', N'eeatg844@hotmail.com                                                                  ', NULL, NULL, N'Activación Cuenta Vito.ePOS - email: eeatg844@hotmail.com                                                                  ', N'Hola <br/> Bienvenid@ a e-POS <br/> Correo de activaciion para:eeatg844@hotmail.com                                                                  <br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''http://casasdemiciudad.somee.com/api/Oauth2/v1/ActivateAccountAsync?token=55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8@4@1356de0a-e0ca-4049-8c77-703fbee4126b''> Activar Cuenta<a/>', 0, NULL, 1)
GO
INSERT [dbo].[Notifications] ([CompanyFk], [NotificationTemplateGroupFk], [CultureFk], [NotificationTypeFk], [Id], [CreationDate], [Sender], [Receiver], [CC], [BCC], [Subject], [Message], [IsSent], [SentDate], [IsHtml]) VALUES (1, 1, N'es-CO', 101, 45, CAST(N'2025-05-21T22:37:05.450' AS DateTime), N'eeatg844@gmail.com', N'eeatg844@hotmail.com                                                                  ', NULL, NULL, N'Activación Cuenta Vito.ePOS - email: eeatg844@hotmail.com                                                                  ', N'Hola <br/> Bienvenid@ a e-POS <br/> Correo de activaciion para:eeatg844@hotmail.com                                                                  <br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''http://casasdemiciudad.somee.com/api/Oauth2/v1/ActivateAccountAsync?token=55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8@4@1356de0a-e0ca-4049-8c77-703fbee4126b''> Activar Cuenta<a/>', 0, NULL, 1)
GO
INSERT [dbo].[Notifications] ([CompanyFk], [NotificationTemplateGroupFk], [CultureFk], [NotificationTypeFk], [Id], [CreationDate], [Sender], [Receiver], [CC], [BCC], [Subject], [Message], [IsSent], [SentDate], [IsHtml]) VALUES (1, 1, N'es-CO', 101, 46, CAST(N'2025-05-23T06:54:52.203' AS DateTime), N'eeatg844@gmail.com', N'eeatg844@hotmail.com                                                                  ', NULL, NULL, N'Activación Cuenta Vito.ePOS - Usuario Ever Alonso Torres Galeano', N'Hola <br/> Bienvenid@ Ever Alonso Torres Galeano a e-POS <br/> Correo de activaciion para:eeatg844@hotmail.com                                                                  <br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''http://casasdemiciudad.somee.com/api/Oauth2/v1/ActivateAccountAsync?token=55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8@4@1356de0a-e0ca-4049-8c77-703fbee4126b''> Activar Cuenta<a/>', 0, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[NotificationTemplates] ON 
GO
INSERT [dbo].[NotificationTemplates] ([Id], [NotificationTemplateGroupId], [CultureFk], [Name], [SubjectTemplateText], [MessageTemplateText], [IsHtml]) VALUES (2, 1, N'en-US', N'EMAIL_USER_ACTIVATION', N'Activación Cuenta Vito.ePOS - Usuario {{FULL_NAME}}', N'Hola <br/> Bienvenid@ {{FULL_NAME}} a e-POS <br/> Correo de activaciion para:{{EMAIL}}<br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''http://casasdemiciudad.somee.com/api/Oauth2/v1/ActivateAccountAsync?token={{APPLICATION_CLIENTID}}@{{USER_ID}}@{{ACTIVATION_ID}}''> Activar Cuenta<a/>', 1)
GO
INSERT [dbo].[NotificationTemplates] ([Id], [NotificationTemplateGroupId], [CultureFk], [Name], [SubjectTemplateText], [MessageTemplateText], [IsHtml]) VALUES (1, 1, N'es-CO', N'EMAIL_USER_ACTIVATION', N'Activación Cuenta Vito.ePOS - Usuario {{FULL_NAME}}', N'Hola <br/> Bienvenid@ {{FULL_NAME}} a e-POS <br/> Correo de activaciion para:{{EMAIL}}<br/><br/> Presion Click en el enlace Para Activar la cuenta <br/><br/> <a href=''http://casasdemiciudad.somee.com/api/Oauth2/v1/ActivateAccountAsync?token={{APPLICATION_CLIENTID}}@{{USER_ID}}@{{ACTIVATION_ID}}''> Activar Cuenta<a/>', 1)
GO
SET IDENTITY_INSERT [dbo].[NotificationTemplates] OFF
GO
INSERT [dbo].[Pictures] ([CompanyFk], [Name], [Id], [EntityFk], [FileTypeFk], [PictureCategoryFk], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [LastUpdateByUserFk], [IsActive], [BinaryPicture], [PictureSize]) VALUES (1, N'LaPic', 2, 1, 803, 901, CAST(N'2025-05-21T12:21:48.293' AS DateTime), 1, NULL, NULL, 1, 0x89504E470D0A1A0A0000000D49484452000000B4000000B40802000000B2AF9165000000097048597300000B1300000B1301009A9C180000200049444154789CEC7D77BC5D55B1FFCC2ABB9E7A7B7A48808424845E2245401141BA4AF5218AF529F04004F4E90FDB532C581015B0E0531404A42320107A0BC550841408E9E5DEDC72CAAEABCCEF8F731302211A2088FAEEF7733E373BF7EEBDCEDA6BBE7BD6AC9959B3519C76278C6004AF05F17677E02D00AD3BC0B7B317FF06789BC8C1FF9EE41080010080D98CD61080110007E900A3975BD66FAA8FAF06ADEBB6DE72B433F9CB5C361BD0FA1F060B6037F9C74D92031138222220BE050FE0E69083AF3BF8BB40006E01007C014C8249872FDBB21D5F4F0EDC422D13BCBAA1969C5EA3717CD5BF5B04444400C6D2A638F9DAE4101CADA534D1A02C58DAF294DE1C72B4BAA636A33524901CB80B59023C056501DEB20711B75CB3026043B9AC3F6C3DCDAD2F32086000B2E1F1DA82BA10111882608ECB39436D68B80F1B2892579383083C8745F5BC52713F326BF43E93ABA34A8EC3D996EB54AB679B7DCE664A02119001122001E1EBB8F06D04C22B0662FD61EB61C475071B4A6CCBDD546EECAA7A7EFFA2C13FFC65CDD05016969C54D95789E515E4200257B2A896BD6787AEB30E9CB0F3D8427BE88CD875FFBEA0F74EADBE7F66D7F7EE5EF2E7A77A83929BBD921FAF2087E49834F28376ECBAE0E86DA78F2A0090B6C6BCE6AC82EBD83D827F597084F6D039704AFBE88AFB39803F3FD3E7FA72787E01800DC981009AA01CCA33DF3561FAA842A635207086E2B5184044C8903669CAFC2B0111896833EDEECDBCE5569BAD8337D5B9B7120490190304D3BB0B67EE3FE1D1C5434DF30A9BEA657270CED2667ED607A6EC39A1AC2D71C6F8A6C5DFBAE77FE63B7F5DD8FC1B792BCE7CBB80009C3363495BDA7342F9AC83267DE9BAF91E77B41D36715E260743805C1F3ABDA3E40B65493206FF0A7738823709C150592A79E2D0291D5F8AFFCACACE7AF3F795CB10C48158118DD812FFB780004430102930AF90FCABD7A8828D288BFF8B4004C15E2DF82DEDC018C1BF3468DD4F02F8F70CBC8DE00D836F10B8B023E418C17A300017C05917EC4C47C83182F5200005605E8EE08CD81C23D82446C831824D62841C23D824466C8E11AC0301E8751F0018D11C23F81B1821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C60931821C708368911728C6003D0069F7FD17D2B64C9D2FA7A99C8188E1415D90240000740AEAB03A8FFD5C84144C65821381FAE13300C630C3040C0E1EA6F802F97317D2DDAE048F1A28DD12287F857238725DB1239634C08BE766DEF3DF7DC658CC9F2ACA3A3F3E0830FE59C6B30D69A963E64C8C8AE2B10F85A25EF18BE3C9F8E10E5656800FB72BDF97F017218324460AC7185B378E9E2E79E7972CE13CF5D7DD33D699E1BA3274F1CB37CD9CA495B4DD867BF035CE92AAD90A125FBB745BE7E565AAF63462832BC1D728352F9AF9B1CB4D193F8268775E3065FFE1311222322C184607C7060ED253FFFF5EFAEFC63501EED774EA7DC940A0109FDCD1FFEC691F6CBE7F49F78ECB152480232C39A86885E5D10F155C53369B83C3C707CC53C051BF0E6EDC53FB413AFFCAAD74D8E0D2CC1750D22BEE11B20A25791838010D05A8B8800281004134B962D5EB470C1C38F3E76F39D73AA13F70D3C1135FB394132349402EBD97A56FFE0C0F77F723907337AD4E8DDF69C15FA0540D0560300916D316C53FD6CFDF2B5DE1DF077D4CF3F062D43EA6DE9C91698563616F09B6B6EFD782047BE7CC5D2452F2CB8F39E472FFBDFDF54474F6D1F3B63A0BF6F7020AB94CA8EC31A894ED39C477DA3DBCA1AB7FBEF6F5E2C293BEFCB678FEAACEE396BAF202818D22DA123221020C30DAD8DBFD711DAF849F8C7A3A5DB36566C6FD197AD5FC7C23F95CD414444C4180300C10400AC5AB5FC821FFDEC8FD7DFD4356ACAE8A987642A1958DB6F885C21559AA70AC990C740193E548F09A9346A874006DFFBF1FFF6AE59FAA5B34FFBE84927B99EDF5221C61857BA4B962D59BA74B123454B970022100D33679DE640406490667AFB9933CBC5D23F033F00C0D06BBF958821DB624A8500CCBA0F00FC9390637D9970222242C1D8AAD52B172D9877EB9DF7FEFEC687C76C7BB0C7534B2957164D16942A42B88DA15A9AAA52E87AAE1F6B4A9A4DCF11C55221AA0D2A77ECB8ED77FACE8F7F5B08FC6D266DB5FD4E3B847E298E6257BAB7DF7ADB77BEFF23C72F0BC98D329C33C698329A2C302104800124AB3917B5FEE5375C7FDD0EDBEF608CE1FC1FF2D4BE2158B25B921FAFC4DB4F8E9635D0521B824B045AB264F177BFFFA35B6E9BDD337646CF98EDB2E61A908EB596AC2164491C33960128217866591E27A07207411B53AB0F2159B0AA39B0B463DC2E17FFFA8655CB179EF6998F7EE2948F978A2500A856C2D1E3A735A88B49145A01302924D72AB7288590803910B31AB81CE571D779FB076773B0E1CA6BF327CDCDC1DB79FFC34622A2D24A30C1185BD3BB72E1F37FBDE9B6BB6FBC6741D736EF4AA3816AD9C91435A2CC222F79BE60AC16D5B452E562894BDE88D32C27DF7183C04F72D3AC0F564AC552B118E7A611D5252F4FDAF1C81F5C7C9D14CEAE3BCDD863CF7D859451A391B2B0D256E28C37922CC9F28E3008054551BDA6207045E0B819E3E6AD783FD5BF1ADE3672B498D162BDE40E676CE1C279E77FF7070F3EF654D7B8EDAB5D13D2669F201E45B102206068559A1AC605A06339452A67CA529E3BC88D657133D3405C08952A6340998C594B2218EC5B59EC9971CDAD73BEF5DD0BCFFBF23965CF78813B9498348E3830438C814AB3CC68D0C438586D31554A23A056FF16EF0B7953781BC8B15E61E4792698E0D259BD72E90B0BE7FFEE9A5BEE7D7C65CF56FBAD1D582D982A15CB96A05EAF018A62E02277A354E7795C080B8EE3D41BCD38D7C530F01C1EE72A49524F8A527B67D2683492C47565A51C68C228D68CEA992D774F7DCF4597DD58960D37E8E9082A7992AB2CF57CC72D94A2663D49300CDC62C893546589922E70CE47E235FF5072AC7FE74D96674208D7F119E233CF3EF5BDEFFDE0AF0B96CAEAE4A06D4C1EF50922B498A6092760C82C63D612E9941BCD38572A27AD98B55C706589726D2DB98C11509A26860C63CC026A6DB54A05010166792A184FA8B83A2B17B8CB2936CA5804B404CA680B9C8151B906610135470680C6FC7BBC6AE8CDE01F478ED632D51863AC0DDC00005E5A347FD5CAE53FBAF4F78FFFB5D63D6A469A35B4D2B1A652E06BE0B546DDE3AC522AE72ACEE2D4220B5C87BB61AD514F555A29960301511427065C215D3FCCAC69D40683A0D0560EB2344EE214182B780E09AF565B2B89B557ABDAAAA8112B60D2739870B33C4BEB03C5629B9498643A8E332EA81CF80A7C60FC9FC001F636E31F418EF50A234912291C4FBA0BE7FFB57760E06797FC7AE98ADEC1BCEC95BA046740486490490564C970C63460A61459D00C0151035A9D3340E22227C30C223289408899B5C618CE05B3649431C00C5ACE9822229D2330CB79A272469A181A444108D61059CE1823D2CA589519060C39294BDCFC4DB7FEFF15FC23C8D15218D6D8D00F01E08107EEBDE0873F69C66AC5905058E9EC68CFE3C6DABE5E2728948A8ED1A611A502A1542A584DB5468D4B2FF43C0ED4CCB589EB61A12845D86836726D0BA1EF7A90E54A652917A25CEDC89ACD8146D3F59C62C137DA2499021B15CB6DA475238A88B1A2EF71A4385359167BAEE755BBA2DA606A2070B01C7889B6499673C7322018B139DEBAA6898821D346E74A798EE73A72DEF373876ACDFFF9DEC5F35728219C62A15C90C2A8D4101097640D1112320719A2B5448024040724B0C600178C31C188882C09640689C81A62008C014322AB1532609C0101276B8009CE0018110110476611C81A831C39E3C4615D10877306D2050B08441C0111E9956F04DE8C9BFDF73360DF1272B4468A21D3569385821F02C05DB3FF7CD1CF7E89DC5DB8322F56467BAE4095244933CB8DE707E57298E7AAD6CC25D852E86AE0F57ADD15AC522E66B94E326D751EFA5C14CBF57A33B6CD42187AA1172559922A574ACF0B33950E0D0D14FCB0AD18265AD79B39A2097D87DC72AD36E808512C0406288E73ADF3D0E34E1836E23C19585329B7798C52458349EA3A3C70DD1C5D8BAF2FBBAC7532B55E81F636CD475B9C9D5B981CEB1DE1C668A58DE7B8DCE1F39F7F6AA8117DE3DB172FEF031940A5DA91258DCC084FBAC83D14B940E44044960121436DC1B4626F96814120CBC9328E6491742B588B888C01A1B5D07AB7BC25C6044ACF222300668D45E2C88DC1758E436488C61A0E96710042A35BCDB4468040A7880888ADD7EE126196E7C698F50EDCCDB9FD7F8628EE16C49624C77AD56AAD05C0D00B00E8E63FDD70C9A5BF2EB68D5A1D153B7A2A9156D6C4612148739D24117364A958D069DA3F9448294AA154C49B71CA18148B25527670A80E128B8E202EF354A974D00B0BC0451AC7466BCF939E2794B6491A3329CBA5629E46FD8DD46558085C4318A70AB37AA1586556D51A912128F842081165268922CF9741A51A371B99054FB052E8649A1A71EA4AE332130601E75C5B9366A9E492736EC96EEAE9B4D62243CE38D0DB96B5BDC5A9B965C8B1DEAF658C31D6B88EA7F37CD9E2858B972DFFDAB77EBA6200AB6D039D3D13FB572D56E814428F1001D0203032CC5A036401913B8C31AE3563C0C12210720066015CE292808881450B48C898216B00814BC600100C125AC5ACE6044844C2658C5B6D180290610096A1064226884B4D8464392324C388C00211A2909C215AC50D81B0E8846B7BD7788E6CEBE809FC401BA38D165CFCED350C02B6F251B6C8A8BEEDD832E440446BADB59633EE388E52D9CD37DF74F5357FCC201CCCABD3A64DEA1D5C33B06659A9D46E40A7994E53E538D2F503ADB25A33725CAFADE4A83C8F1A960B1E7A9E35A651AF3942568AA5DCE838CD014048E984A5348E00E3300811214955929294DCF103A3F25AA3E93B5E5BB19029D5881264A2201D2B45B33EC0B82C8621824DD2DC1005524A29A33C4DD3A8E8FB9E802CD7515321A742E0E5C86B51DB4F7F7985C3F489271CB7DDB6DB84C54A10165ABE3BC6D8C6E6672B1F6738B5E5ED5B036F59B3630B90A3A53010992365A3369467C9238F3DF68DEF5EB2AAC18B65E8E81A3538B05A196B2CD364050302D4C638C424E30AB9D6B92F2D673C23ADACB506A4E116B82152DA1A40C6C01259635DC124F76200D49A5B42C12C80364672904C58C6954AA4900E4A2083DA2007231930A18D9564258006044BCC5AEB58CBA505AE4D96A3EF225882DC58573A56306B4913BFFF2FAB8DCEE2FCEAD05587BCF790230E3F9C73C118B3D6B6924E5E732860A3C4C47F5DBC2972B4E2EC00C0186FD46A60CD4DB7FCE9EEBBEFEA1DC2BAE9DC76FAF8B4516B36060979E03A2CE0799A6A4229B9EF055A519EA45208BF5A35595A6B24DCF18A4566AC8D33CD19952B5532B619478CF1822B2D63569B2C6A0661C881A7499AA7C67565E0FAC6509EA44CB06AB5DDA459BD1973D729869E218A95468A4BD536A66D334E0D62E009C179AC4C16A7BE23C3204893A49E932358A9E86BAD9B492618B5552B64DA0DB2A7170FC4516DC9AAAB025FBE63D6DEC0B81F86D61A00DC1445FE6DF006C9B15EAF121010EA3CFBCD6F7EFDFCF3CF2EEA63CFCEEB2B954A5EA11C0DAC66D2B316C166243D648E52B9B63C909C01199DEB34971E493FD0D62449D3E725741C52CA5AC508903CB0A0B28C49CF958291893265545A741D26446E55A6AC701C0729337992E5AE158E2714C659D27459C97AAED639294D36C7A068D1645A1132728521637345796E050A16D83CCF09057339838C107263B8A6A0A4B2463D4EBC2018DD336949DFDA1F5C72D5EDB7DFB6E32EB34E3CEEB84C2BD771374C5DDBFC71837F9D97C0BF1172B4EED05A0B003A555CF04B2EFEE9E5D73FD0D4AE748251E3B666CC5AA34839265341E008E6459956CDA65728730465314A538743502DE5C6361B7529457B678731A03383D67ABE8F28D2241188ED6D154D98694BB92E381CC38E5CA5998ABCC00BB9349AF22C1340954A511B68368602DFE9EC68CF2DC64AA335455F020BA3A8291DD656296864796E29CD3C8789423956266E0E164A051F406BDD4C94E41894839C306A0C059EDBE957724371B35E2E8603B1BAE5E1D58FCFBB2574E0B0238F5159CE2467AD74C3BF376000B03EA5090880B7B65EBD098EBC465EFD96C71B210722E62A67C0A490E75FF09D81C1BE079E196CD8B6B054ACF7AFF454562C57B34C37B31CC916C031C2CB8CA62C0F3C9FC0A838B3DA2ADF258B79AEB3B8298B45C1DC2C6926A91652862ED3D6C471EA71E679BED5A98E730066A5900CD33433360F9C3207132751A6D1771D6999D62A8E231799083CA3B324CE84743CC908314923ABA45776D0A82C4E3471CE5D6651E52A8F9B81E333665596E81CD0772D5A429EC6918328029FB48AB28C195D08FC5275FCD2FED517FCEC0F8FCC7974F49849A79F7EBAB564AC1142586B37BD464180615F3C020C73E28D49769813D61A638133CE586B831FBC25BEFED7BF35C15AA554E00500F0B5AF7DE9BA3BFE0ADC115E39B00069B3DADE09DAC6CD841895439784AB32432A2F7B127D5729ADC87AAE90A1CCB4A12CF31C5E08BA4CAEA328629C55CA010152AE38B26A5B1535C4716619950A1E21D3DAAA342A940A1C98CE749AE5AEE7068C5B6BB5CEA4609D9D5D94A928C9116D7B39244B99B1A0D36A7B276A93A4DA8229863E633C573AC9B2C0750A851E9D65CA30C72F1643C8B4B664D0EAB6AE1E48559C28445B2EF91698CD15A0E9EC6CCF55E9C67B96B575F4AF5975FAA831934E3FFDCC4C658CFD8D444EB2161843952C7BF04F8F2E5EB5F5C99FDD0994B652BC6E9B6598528C89F593D95BA83F5E1F3908487229B9FCC2B96702C135B317A2D759F1BD5C29AD5324E37BBE261D4511F3022939A8344F3346A499E7B86E94D47292A5D0E56095B156251284905EA6E3469C044141226A6BF34C31329E5B2166A2B809AEEF0824A43C37366DBA9C31C78FB346A674A9587204A4CAC649EA49E6948384E26692BA6EE01028209D2AB2A9E3F916284A23CB0497828894B1364F187A9E17C4E9609CA942B12A5C5459428A88591914729B34935438AE231811A546A3CD3D5962D22319F6D6D975F72E2F7A2B93243EEBAC738737476D28A797ED0BA332450CD366DF92050BE7ADAC020018A3C9686D9071E9B882ADF7C4222258B2565B24A38D31205C4F82CE953216919817BAF37F7CE4D973261C7BFA79C7EDDAC6ADB1C8D95BC190D7418E56C7EBCDA12F7CE1CBF7CC5D238528B58D46A0DC6844EBFA810596E7090256DA2AAD9DF00CB85B28A1B18436CD92B05029A13116324D8E60E4942C519646BE94E5AED022580312C9F59C5C91CA3262586AABA0B5CA0258721C0E7E87D60AB3B4522858A29C30D65622560B0583984411E34EB5E2923568AC4082C00516AA3C05C072A94448A809C93852905301B269D4708252102843048A1CBF681C40062A8A50F28A5B446B53430CADE7BAC402638C3671A95A6164923C68A443CFCF7B6ED89A407AC5680D5BECD658E1FA0200BCCE1DA66DF3CCCA950000460BA7C01D070000B432C01983D60401000C19930C400CA7BDE719382E97ADFF58809EC3BF76DE3B9D31130A1C0090E1A6F68BBF49BC6EC596E7EA89279E88120228388E9BE569A3D1CC142002478A1BF53C4AA4F40CD946A35E8F12D4A990A0F2B4591B9452302EE2B8596F265A5B8E0CB55659425CD412B5E0C5254F3DBFF0E9854BFB076A0836899B592392DCB54051D488925C5B9282599DD56A432931721C9DC65923CE0D71C139D9466330CDB340BAA8D460B3D9CC0C00B89225513D8F222E1C04A8478D5A9A5B4B0E476BF2E6D00027604E9065E94023CED288312D04341B8336CBA570B55579D4C832438012214FE3A856978408A48DB549C6B868E98C0DA71522442063B445C6F9FC1B7E78E236DDE3A74E997ECC17AF79694A2700F042EFE3BFFBE44ED326EFFD896FDD6B3967CC6A4348DA0040FF73AB5E9A7DEFE2177E73F627F69DB8FB7157BEE8B8FDD77EFAA0495377FAE4A5B35F02804AF3E9E57DAB5724AD1D26AFD2586F066FB23E072208C7E9AC7431AB53A54227F05DD2D682B59C41A9DA4686D23C6628DB4AE50C9111813681EF7B6E68746E8057C2A24232D67A82845F58B8B4D9575FF31F471CB8CF1E1FCA6C16F885E79E7FE196D90F119524E3B9CA10657BB1A45A5BBF74E67B9EF40B0C3829EDF9A11F9035A495E20CABED5D4436CE522BBD92E35A6B1812E8AC546E036B33950BC0B662290722B264B5EB48D9D185D6641916FDC0F7D112314BCCE8527B173336CF73645EB1E412590306C80685D00F8B46670A5920004ABEEB3888F82AB7282290B68C0B932CBFEB7FAE7A604EC769BF38DF59F4DC634F348B8306207AF44797FCF0F2F4F88F9DD1FFC05DBFFCD025850B3FB44B4F01B4CA1597A25459F1A74BCFFDF1FCB1EF3CF6EC3327FFFAFB27EC7CE98E877EE0E4CFEFF8C035CF3EFDF8B3FB6E35B3EFBE394F6DAFC6EC3EB3C7E160B75034870178000E0C57B7C8DFD06AC5751DE6B903FDB524C90BC58AEF71AB4D9667DA52B55245C87B07EA8E17960397914D334559CAC3B05CF47AD7AC329697CA65973187F1F98B56F89EFCD80947CDDA79CAD6137A264F1AD76A7F876993E62F5E75F7437F1937AA73F9B2158E1B944A05664DAE54334D83B01004A23E389866AA502E49CE759EE5594A5CB455AB71B33114259E5F28FB22B34625792DCBDA3A3B9099A1B5FD8CBBE55291A1C95393A611F7BDB672796D7F5FAA1816425762AC08E234B5AAA3BB3B8EEAB5C1BA1B968BA1679551499E18E5174A0547ACAA0D20F7822060C25179D6E2C3864344640C63029AB5BF5C3327B3633FFBE5530FEB30C992B197DFFAF41280ECB1BB1E9A4B27FCF2EC33DAE1235B5F78EEE5573D7460CFA1C53112748600D2E6CCA96EBBCB01279D78E8A4D57AEE2D3FC531FB7DE4F803DCEDD77CF6C6BE796B61A6E331CED7A9AA2D38A170D8B0EEC91BD01CC8194FD3ACE007AE9416C9E68673293CAE554E3AB7C80AC55030D4563164D2710DE7C8186579392CE5563306DAE838D1EFDA67F783DEB9EB2107CEEAEA2803C0A34F3CFFA77BE71CB8F72EEFD86D467777671CE592B05A2A1A44A31502731D1F091990D1CAF55C2E2507026BB9E34926C82AA3325F38ACC82D82319A7181AE708463959608D5622107345631CEB9E3010ACB599CE5A117BA9E056AAD0E05F89EB09ECD729F4B5E2E1A40300A39E75E80C61862A9D69572C95A66C992D18CB70C87574CFB642C0A0E49BA78EE5277EA845D0FEB00AD539564B9B6024DB476D4813D35E7B1730EBEF0D9FA0BABFD5D8EE8F0020640962103504D2C57B7DD6FB719132445A673FA8E476EBBCB641F7422AA8A204B00B82BB8D8D2FBF05A2518CCCB6FA47E23E44084FEFEDE425BBB1396D246AD9E681104054700885AA3EE08B758AE647914359B4C069EE74A21F2345D5B4FCB6D9DA1845A7F5F96D3078F7EEF29C71C3879D2180058BAB2EFA6DB1FB9E6E6D996689F5DB727AB5D4E59D21C6A463228A14AE37AC30A2F0830F0DC246A0EC67158AAFA2E8B1A43B906C70F7D47DADCD487861CBF10140A495CCFE2149D903BA2E83ACDA1A11858B15A754DDE6C342C3AAEE7F9819326E9D040BDAB5AE592C78D5A9C90EB0752F210716870C0F5C3A0584EE346BD117137F05D292436A22453AABBBD9A1A556FE6942761D8F6EA754A6BA000004848743C6ED1A42ACE0998F0CA40E0C8DE079E7904B73978EA8CEDF5F403763FFAF8BDC7541980E59CB5A444A4D224CF0989D22C4FD33C2722CA14A2CB1980B5F0B2F9BA0561DFDC466AA34DA3D6F42B934D9611E4C01D2F144046594B4CB85E20C0A65946C4422F5480A033E05C08C7F5AC2595242633ECB8A3F6FFCC2947F6745600E0F9F92F5D7AE5ED37DCFA80C9D2CF9C7CF89EBB4C4B120D86B981CB39E659868CA4EF13A2B54673977BBE9767A09422CEA5EB7240D2D65814D2F17D02CA934C720E5E602DA1C9091DD7F794322ACB9081F47C00602637B9905286BE8C72E53060C2F53800193006B8F402DF5ACAB34C3011FABE45D22A432E0AAECC18446966010B41D830F1B2A54B375EAD20436D887961CFB4AEC5D70EC164BECF9E250FE6AF59F6DC6C6FBFCFFAA31A73EA2B476D7DEE659F6B9DDF5C5DCF4AA11B08B204800C9131002004C610B1E5FA42649C736400809CB3B7DE07FFBAC91106C58F9DF2D19FFCE66605151B45E887A19099CAB334E302C230D0593CD4ACBB4E500E03AE9324CE1417AE17942B9535BDAB8DE11F3EEED0CF7CE47D6D957296A93F5C7FE7EDF73CF1D4BC457996ECB3DB4E07ECBB5B21F45E5CBA7265DFDA42A1EC39EEDA810174FC4218804EB35CA74ABB41104A276A34E21CC2B0E0739BA5719EB76C80304FA2A8D9F00BA174A45699314A691316CB5264CD66D30AA7E0078C4C92652A49DC30F48B95DAD090B6CAF50ABEA466B3A93411A362B19C361B51B3E1FBA1EB794AE7699A59AD8B850277DCC1C14126035F259D5E7AEC7127B4F6ED6DB85A41E48CAC81A03AE3C8773E7EC58DDFFCF0A9133B94535BB0C8D96ED6EA26DBE1B86F9E0157DCF2A94FFF19C124E1D67BBFEF03C7EC5970C918E200A4B2B836D048520B403AAB0F34E2440100A9FADA21D3D400D9C060BD11E76FF1CE1ACEF63869DD11AA447F64DF7113DA3C4BB031315B36B99472A79D76CA1BCB1E79E271E1761380561A98C398B0C8B4329640080160B5D59608507024634D9C5BABF5D187EE7FE6278E6AAB968DB697FEEE4F5FFDC1AFD70E355DC9C9D2078F7CD7C1EF9E65ADBDF9EE47AEBDF96E4F7083CCB4225BC690250B0C481B6D2D32D65A3F12019928D583F524CF5287330B8C38B3168451C08465024CCE5ADBE75B25508CB61688734003DA00729D2743B524CB53C6B800669181519A18E3008C290B99B644DC119210328564019993AAA42AFA4FFFC4B11F3CF604630D63AC9536BB6EA09031B016A4DF31757A41AF79F4AEB9ABF371BBEEF7EEF77F6E271FC78C1F3F75DAB69D4BEEBCFBE995BD91EC98B1CF217B6CDBE1722040C63910911B766C356D6277874396F1CA56D3B71ADD138235C69F306DEB09633CA58BDB4E9E38A1A7C019BE692F694BDC4B07D3CBEE5B263D690CB5261714A7DDD93AC3112C1E48EEFBD23BF6995CD196C46BF9DC08485B0D06A49417FDF8FB97DFF458D46C308692492E796EC12459A91478E5AE5A23CF93260A5FBABE005B8F9A599A9D7CCCFB4EFFE451C542D0A8C7975F7BD70F7E7E0D43A3B5CD357DE0907D3E75D261DB4E1E37AAB7774D00002000494441546FE1F2AFFDE8B70F3C3A774C4729CE6CB15C4ED3E6DADE4142C95DB7E80BB2DA1A5B2E973867B57A4D6B98B4D5C49D676E531F1ABCEFE127B5E1E56A298E6A59A6B9908EEBAA2CAF0D0D58CBC362510A40AB8849265D47B03C8955AEA74FDB66E6B4892B56ADB9F3A167CBBE4F9C31D0B57A23088BA520405D6F34FAC9A0904203A854713020DD8E6AF09F27BDF7A8A34F68C64DDFF7890811388A614F27D070B8C31AC3362EE14046E13AA7D6DB8F96B8EF7F7168DF6F3C14B4F9B91EDEB6FFFAA69556AEAE6536CBF3CF9E7A669E7F2B6A469EE76BAB5BF146CED9A2450B9E5AB85CC9B1800856A77984C01D87EFB3DB0E9FF8C861C5424084F73E34F7EB3FFC4DB9E0ED307D9B2849B69D34FEB48F7D60C2D8EE5ABD79DBECC7E6CF7F694C5735C995CAB2152BD6747696F7DA6DFBA0540C3DD9DF3FF4F4BC4596284EB5EFB9719233C68F3A68CF8F9E70F0CA55834F3EB360D5AA01C70F8C06029666D960FFE0365B8FDF6EDBB155DF0FCA95854B57BC306F11630620635C24590E8C1D7FE401471CBCD78B8B973FF8C457EA695C7004712684C8B2BC414E85F5EDB7F3988ECED1C66A86C83933069224DA7EFB1D8E3AFA8446D4088380D645C45E1EA896DF03011863562BA5752B91923181C80463C21AA595B10004C8851482B502BC880864B5D6845C700E6474CB9526181A955B645C08528A9031C15B6E7778EDF52CBD5CD5E80DE175C7565A190C889464F1999FFBC2C6E7FCE29797DE76DF6F7A266F856E85818E9A49B3599F3A75AB2F9E794A575B19C0CE5FB0FCEA5BEEAF96FC2993C79F74ECE107EEB7B36400005AEB3B1F78E2EADBEE8F931C881377468F6D2F06CE41FBED79F231EFF1021700AEFDD37DCF2C5CE6BBA194303030289873D07EBBECBFCF4CA574FF607FF7A8516B6A4912D77DC747C8C78D1D57F6DD230ED9FB8883DF197802002EBBFACEEFBDB8DC733943A8D79B86F08883DEB9EFAC1999CAE314AAA5427DB0AEB4855CB57776E7717D284EE2E6E0373E78D6AC59FB6E7CA7699E8661D8DAF982ADF4D175E5E700D68B0B8171E170F92A092172E9BC4A79AC9722322E656B5B150113729D8F8B09C95A2749F9EA6B5E21A7575075386043AF7B9BD6EBD61CEBA343523A71160F2FEF09ACB508CC91328A1ACC11A956423581CB24B792CB3D76DE7EFCE84E4DBAAFAFF6E35F5DF3E0E34F6D3379E2A9A7BC7FFFBD774AF35491758537FBFEC72FBCE4EA35BD83ED95F28A55ABC78F1D75CCA1FB7EF8FDEF71433757F98AD5BD9DED6DABFA06B234F1A4D61A8686860EDAFF1DA77DEC835B8D1B45045D9D6D68F2AC59777CB96628DD7A7CF771EFDBF7C3C71F08C0E3385ABEBABFABBD7D6DFF601EC792879658A399EFBFDFAE677FEAE872A9840CC67657D324B61694253498440DA315474E444383834AA928899CE158484B7ECC91D25A0B88006409390E0BABF524B7923886C7AC95D201809B9B87F17209CC0D4FDE5C1D307CDEABD35C37EBDA0DF006F3395A3F5DC75DFFCB163904E7885808FC6A3118EA1BD28695CAC50F1E3CEB9C533F64AC4E33F5EBABEEBCFEB67BC68F197DD67F1EBFFF3B66A62AE38C3BCC9BFDE0935FFFFE6F96ADEA1B3B6ECC9A3503DB4D1E7FC6A78F3DE4DDB388EC8217972D5AB4BCABBB6D4C4F575BA1E070489462E41D71F0FEDF38EBC320F9B5B73FBCDF1EDB13D95CA516841396A78C0ABEFBDF9FDC71E63640F0DCC2254B972EEF19D53DB6A73B101CD15A6315E1A187BCF3DB5FFC48234AAFBEF5C163DFB7B7749821D0D26977A54218AA374AAED7D1568E9427A523A574ADBB21396083279200F946B5A1D62759BCEA60F344446400399AB46FE9BC97D6E8CAC4ED26F7849C5E73A5B0D1B5AD828C3A59BDE099654DD13365FB7165C9DE80E6D8B25990AD791653A533A573AD86A268EB093DC71FF51E6B0C43F1C863CFFDF457D796C2C2CC6DB7DA61CA0400E2888CF15B66CF39EF7BBF59BEBABFA7AB63ED406DEAE4D15F3CF3A443DE3DABD688FEF2D48273BF71C9B5B31F993071141165990280BEBE81695B8FFBD4478EA8B497CFBFF0F2F3CEBF746070089125511A721C37BAEB67DF397BC799DB0CD61A7F79FAC553CFFED1234F2F1C3FBECB5A4AB35C19EA1BAAED327DF2E73E76B410ECDB17FEFEBC6FFF1C00ACA528352A4D9B696C55C64DD6CC92469AA7796ECD6B156B7B99194C30CED5EAE7E63EF6D0BDF7CCBEFBEEBBEFBE67F65D77DDF3CC8AB50A4CEDC5B90FDD7EC7EC879F5DD49B0080D19BB5FC24A3B50580FA4B7F3CEFA03D66EDFF5F973FDF002032F6EF5F4D561B0050430B7FF5E13DF6DCE380EF3D3298B73ABCD9721CC65BB01D1291542E00AAE54AA0CD94C963C78FEDD256AF5CBDE68F37DC5B709D4913C67DECE4433BDA4B00A0B4BDF696BB7EF0D32BE2241D3BAA3B4DF38F1CFDAE533FF17EDFF316AF587DED9FEEBBECCADBDFB3CFCE5F3BFBC361507862EEF3F73EFC174079E0FE7B9DF5C90F4CE8E9F8C1CFAFBDEAA67B779E36695477FB8AD5FDB912279D78F8B9A71E4B44CF2F5A72C36D0FFEEFEF6F39F9D8433F75F2E1E56238FBFE271E7F767EA554DE71FBC9677EFCFDC5A2FFCD0BAFBCF9AE87779A311900FAFA6B8E10A9CA1040595969EF5671A4D28873E472E3516A855D09800163A6FFA907AFFEDA61FF756D1DDA3BAA8E0B364F6C78F4772FB9E0C37B2FF8FDF99FFDCA8DCB773AF1ACAF7EE9ECF78DCF33251967AD2A80040844D4F292205FF70B18AE7149404C16CADDEDFE4077D96D2D9281ACB51BD83588886CDD8EBC16592D594B00C064B1BBEC8763DB3DFE1A93CCE6E0ADD82B4B86602849555CDF6D87691F3CF45D0000161F9B3BFFDADBEFAEB4B7AF1E1CBCF5EEC7BCC04B2273FB5D8FDEF8E77B1A49D2DE565DB0646521F0B69E3C3EF0FDBF2E5CF2ADEFFFF6FADBEFFDE489879D76F251A11F2E59B5EAD77FB8F5CAEB67EFBADBCC533F7EF4AE3B6EFBE7BB1FFDFD1F6F77393FF6C8775BA03FDE7CB71B3A471D3A8B885E58B0FC8C2F5DF8E4730B3EFDA1C34F7AFFBBCBC570D14B4B2EFBC3AD57DF78EF21FBCF3AEB93274EDF6EDC3537CDBEE2FAD9A5C03FF688036B8DC6EFAEFEF3C49EB686AAACEDEB53490D10914C66D0A68ACCC6D52611892C32898C7AE75C7AFA7BCFB8AAD9B5EB81E75F76D989DB74F964091922E702D8EE5FBCFCD1732C20639C03A01F6E30330DCF332F97186D0999807121011050B0D0C13CCD7203ADBDB88CBDCAC5D0CA721E566244008C7381082059E8619EE5B97AC3B532B73C3988A010FAA5C0EFADF5767794A64F196BACA9379A4B5E583976F428DF9351145D79DD5DBFBFE17EC11154868CB7B5B54BC6BACAE1B8319DDD1D9534CB2FF9EDCD37DCF6F0B9A79DF49113DE177A0E20CCFDCB0B0B5E5C356DBBAD8E3B6CAF195B8FD1C6ECBFF72E5F3E23BD63F66393278E9E7DFF138F3F35FFBF3F7BDC94ADC6379AC9E7BFF9B3792F2D3DEBD3277CE6E423079B4D00B8FDDEBFAC5A5BDF7EBBAD8F78DF9ED3B71B972B75F87BF701C25BFEFCD0CCA95BDD78FB434F3E3BEFC26F9E7EE35D73AEB9767693B13889C230E82A149B79F01ACF1C5922C6198378D163177DF2CCAB06CB3BFCE8FAEB4ED97EB46335322E19D0F004A0A3B52B56AE8CDDB1E3C67515316FF6AF59B1BAE1758E1FD35DE01610F266DFF2656B4DD83366747B2000006D5E5BBDFCA5A56B62F459B4F8996535E29C813100C0A46DAE58BA64455F4369CB8474BC9EC9DB8CAAF89C8C26648859DABFECC5A54343997575EDA5F9030611DF7892D85B410EAB559EE5B9D2D6150E2203ABFB06EB0F3DF93C58C80D186D35A164E071908E97A6F98AD57DA347F57CEA94A30E3960F731A3BAD3341D37AA7ADAC78F38E7D4139E7D7ED1CA55FDEFDE67E73D779FDE56AE6C3F6DE24B2B57FEF5856533A74EF63D71C8BBF6D867D68ECFCC5BD4562C7CEF2BFFF9E8532FAE5CB9B65C2D7676B79DB5DF899FFA8FC3EE7EF869C978777BE5B0F7EC3573DA36D3A74E58B868E9834F3EFB8E9DA61B6B8F3864EFBD779F396FE1B2F1E346FDECBB9FBFF7C1277EF5DBEB88C0711D8A759CA48E088DD1D69A57DF220201676086563CFEF38BE626C5A9C79C77E2D4EE901B65091186B53FE78C56FCF9E2D33E75E193DB9F72D155DF3DB2B8FCCE9F9F7ECA058F6F77C2A5D75D786855A3E02B6EFFD65167FCB6778773AFF8C967F71BE7A39E77D3855F38E75BB7AF649DE3463971EFCA657D2A9CE8A2D200A8079FFFE369A77CF58145CDB0DA598896BDD8E839E8AC6F9F7FC611DB7A0092658FFCEF57CEF9C24F1FC3F6D15DD224FDAB56E45CBAFC0DDB955B9E1CC6DA2C53813261E8F9A5020000B2DE81A1479F5938AA236C36E34A50ACB8DC10983C4972DDC868DBADC69C7ACA9153B69D74C3AD0F85BE77D4A17B7DFAA4233DD759F8E2D273BFFEF3BDF69A79C0BE3B7777B47577B4BDB478F9777F72153279CE678E9BB9DD44DFE3BEE7EDBDDB8CFEDE815BEE9CF3FB9BEE1DE8EDFBE449877DE74B9FAA960ACF2D7CE9DCF32EFAE27F7D08918D1DDD397674E7FC17967FFBC77F08437FD2E851A37ADA01784F4F7BA55258B662CDF5B73DF89B3FDC9A839056A769DAD6D1A5D278B059E7441B05C71187E3F3599ABCB47010BCF13BEEB7678133459631C1101189B19679CE314D68B06E39470086A44DDEDF6F5F36029081E91FB051AAC0055C76DB8F3FFF5FDFB81976FEFC255FF9C8AEA370CDC2ABFEE7335FBD27D15C0AB0267E6A6D79A7F79DFB858376F48B05A61FFAE969675DF49DF3C78F1A73FE29BB9BA77E77DEE99FBFE099A9C77FF9EBFFEFE8AD205AF3C22F8E3FFAE2FEDCBCE100CC162507010070CE2D614660811B3BFC9E14D711BECB9ACDA6EF7B8643546F369A4D2212AEB7CB8CADCF3BE343E56278CEF9BFBAFD9E395B8DE94A8D3EEEB0031E7A7CDEAFAFBCF9BE479E7CF701BBF89E536FA48F3CF1CCCF7E7BC3C297562771F39B3F48FFF394F74F9E301A8D99FFC2E26F5DF4FB9796F456DA4A17FFF6FAEECEB659BBCD98F3F8DC4BAFB8E3E9854B2C272178A399DD79FFA3BFBCF2F6A54B5736E3F4EC6F5C7CCEA73F58285718C00B2F2E39F32B3FCE2CF72593D221ABB29C426B082C5932C6C04693F6FABC2F44C601093C89C490B5EAD7E32B8643722740C7F724022093D20B9913BA7CDD494C7A05870F70B7BDC8CC63D7FDF8DB37AF9AF4895FFEE4AB274CF701604269DA94627047AC0C6780E0EFB0D3BB76D9BD580C5AD74E3B66BF3F5E7EEF43CFAED08CF5FEF9CAEFFD644EE9C0AFFFE8279F7F475500C0D870C6F60266B7F617BD216C5172200080B5D6F79DA2EFAC1968526E10B1914453268D3FF5631FFCED957F1AA8D5F550DC56AD7674748E1FDD71D23107EDBDDBF4E52BD67CE1FC5FDCF7C8339DEDD52849AFF8E31D37DEFA40EFDABA31F9365B8F5FF8E2B21BFEFCE0C373FE7AC7BD730C8194C238CEC2C52BCFFBCEAFAC514898181AAC476EE8A74A018A1F5F76DD0F7F794D12450AE48489E31E9AF3BC23DDBBEF9BFBF0E37F8D552205F881FFD4738BCF3CEF9294AC4A73C9786A40A0069464C1F702EEC260AD51F09DF6B64294CB8DB72D21AC0FCF1332223B14478640028085E122C2C3CA855AFBBB54CBA8256BB5B2469B97DDA8649532290BFC3059F0D8ED4FF486338E3F62AFA9BE4D9254CABC1129D3728A1220CA6A593607973CFDEC92D583CA2DA48F2EECCBD0AB761593DEA71EB9F3A96CFCA1EF79DFACAAC8A29C7926A9A56CBD4FEE0D61CB4F2B0800C891330376796FDFE040AD582918634E38E20001F8FB9BEFA916FD43F7DF73E79953C78C69EFE9A8DEFBF0DC0B2EB9F2D9798B47B797AC0565459CA46BD70E19A5FCD00F8BA5279F59F0D4DC798D38D7603AAAD5244A381008D93B508F8606B541AF18F67456AC3283430341B134D8CC1A43839CA0502E570BDE23739E7BE0E127071BB1E3174A9E17471117AE147CD99AFE2C4FE366B3DCD639BAA7AB3E3410A5C6758DE50C905B939366449C5EA3741CD1BAC81A59A12D27BB60E92064E38CE0629D031C377056616B22020086C8399031C60082B5464589520442BAC0A3DEFEDE94553ABB2BD25A835C0A911B9D5B0200D200C0F2DEE77E7FC6C7BF7FFF2AD5D9D35E709AABE73D93D0C44AC1D4FAFB7AFBA0382AECACA021E33A2ECF20CF32785349846FC15296204D626DA1DADEF197A7E77FE767577CE9F493C242E0B639A79C78F0DEEF981906DEA471DD60ED33F3165F71ED5D37DFF1D0E2652B8B850232C75A45CC0AC48E8E0A439E65F1D0E0A0E0C21AE3046E49609C24C29502781227AEE3B48F1F6780549E46B51A70B75C2EA749669918DDD56D119224565996113796AAA530B7106726F40A867496E741E875B4571898A499AC5DDBCFA55B0921D3799A19606EB95CB1593A58AB83D6AF5E3DC270AE1680131627ED34C6DC5BAB3F3FB7E9EEDCA9B324D5C83863D668626EE0330222BB6E99495A6BA518771D075592685908C776974301799E9329B475748776FEC09ABEC42018152BCEBD72D927D3A70C73215F7AD3D73EF7952B1F32077DFEF3C7ECDC55708297AEF99F6F5CB1B0BF09E5F6CAE85194AD49D7F46A3ECED6F2B4C059677B01ACD59BE137DB04B6BCCD0188602C68ED4A1135E22B6EBA1F893EFEA1C32DA2EBC89E8E521CEBC7E7CEBFE3BE276FBDEBC1B5FD75CF0BDA4AA538CF9AC042DF01AD54AEF2BCE90521670E9804A56C2B9733A592243326E35238C2CD298BA208803863CA902504AB182F0AD4711CD5893C57322EB43540BAADBDDDA459B33104D2350E07E064288922A3B4EF07699E6B022E0119121368C9A884FB55CBB9D58AD98DF784B4B4BC35C00BA3773DF9B4FD2F3BE7FE3B2EB8F89EBDCE7BF794B0E0BEF25CC6A557E09EE74A04402E9810160DF73AA4CF2424CB162F5B9BB1A024B5CDBDAD264F1BE7DDF2D4DDF73FBBE6886D26846500D2699C0BBF5C92BE47E963D75C7FDB928EE37FFBABEF7CA80700C0DEF1D84F18776D02E58EEDB6DB6A74F6C8A30F3C32F7737BEC5A1600A0570D644EE0173CFE4695C796D51CC3FB415DDF9352346B036110700BD7DEF6D082979623F220F0D22C8F1A519AE8B5CD344EEAAEE09C21E7BCE0042AD759D464D2F3021F5496C6B1239D6AA59C6AD3181C04EE48D7F300B3344B321D848183A092D45A609EE748AEF3346A343CD7690FAA4996E938B68EEFFA3EA974B07FC011B2DA56CDB58E93845004BE1B3A90662A4A2219863E6296A58DDC0A37F03DAE74DE6C347C4FB6574B512A5EBD92050044403264993776C6C9DF3CF7E1132FBAF19B9F3C65E04B5F3C62EBB20BDA10100463A6CED8BACBC4D1E03253EFE86F2800D1B6F58C3D762BDC70CF75177CF79D7C8FFA9D3FFFE90DF7FFE5A9A1B863B0BFB75FCDD8E1B0930FBFF69CAB7EFEFFCEE9569FDCAF7B70EE1DBFB8E88FCF0FE51D8D7A3D86711D3377EABCF296A76FF9E5E56DEF189FDFFBBFBFB8F2FA3B86CC585ABBB29F6DBFC7E1A7EE73EB176EBFF0AC33DACF3B7E52BEF2A9EBCEFFE9FDB538E9AB0FAF57DEEAA8ECDFC170A9354B8C8370ACA12CCD0AC530704B0F3EF6D75CE972A180C8932CE588D5F6B6B13D5D499C0CD59B41E07B41A0356952561B2119E3C2925146B9C263840A1959C38C765C07D0289DBBE8702E7354C62AA134F3A4104E9A251284E08231A3408151D64A477A49DA345603F31997088A59C3B4462918B7499A7BAEEBB93C538C9496461117C2F1A3B86E2C0F98E49C016EAC980900395943643AF7FCDC0517CDD8FD07A75F74F50FBEFDECD8A2078620D7B4ED07CE3A77F2BB824A5BCFC4895327B4070C00C3C97B1DFAE18FDEBEE09A39BFFCCEB7EF1C7C6A41C3AB4C98E1A5C589ED0C3475EF7DD27F7FBDC6CEBFF8BE5F7E77604E7BBC70FE73BDFEA4A9D33B3BCB94A0B7FB473FFEF1FB96FFEA8ECBBED7FBD0F8ECF92796D9D2C449D5AD277489D4F249477CEC9B2FD6BE7AE9EFAEFEFEB75E1AA36B4B9F5F01DD93B7F37A3AFC37EA06DBB29A830080014BD2344B13D7778878143501F994ADC65BC1B238E588CCEB26ADB3285DDD3BE8095E2A96E22C4BFA079C20708290F23C8952C630087C65ECE0E0A0908EF403D04A2BDDC833D77184E335A2180864107A52EA346F361347F042A198E4593CD02FBDD00942CAF234CD95A5200894A5DAD010E352FABE302ACB7293832744A15088D3248D34775CDF0FC8E8669C11996231CC945A3B30002A7B2D1FE370D22F90E5D6C0A4830FFDFCB4F67D9F7969DEEAA636C018288BDD33C78700D55DDF7FD6A5339AA5993B54000864DBD4F79E7349C7412F0CAE5D35C03F3663BB896D982E5D6B3AA74CEB71D05230F398D37F38FD9D4F3CBB60652377DFDF35AAC3578485D153A784805EF7FB4EFFD1D8FD5E7871CDCA818638E5D363DA1DD3484BE3674EED04325EC75EA79FF78BBDF69BBB62D9EA26F3AA3D13DA646AA9B2F576450900F0FA4BCDBC15813700D268B5E34862A21929A56C238AC272258A63635481C011023891466BADE7B984D0CC134EC697AE46C86DAA8C0939735C4FA54DB4DA1501724C40A549EEBBE80781D1799E250E92236546364B336D74D12D0283284B3868CFF12C58A34C9EC705519082EB3C41AB5C1EA07014914E73234C31282BA55496BAAEE74837171C7464AD757D1F1936558C761305E086F35A1880556933931366ED3561D65E1B9FD7BEDDEEFB6D0700AD401A0096474F79D7E8291B9E3275DD81354693EC9EB9E72133F77C8DAFB40ABBB6DDF9C06D777EAD61B7DA18574ED8EFA009AF43567F1B6F85FB9C02D793AE5BFFFFED9D4B8F24C771C723225FF5EA9ED9593EBCB42CDB1045ACE0830109F68D067CB0EF0224CBF267100C50D0890701BCF044C89F4357838021C3020F8200F9E2076CC2F6852001C2F47277A6BBAAEB99AFF0A1BB7766965BCB59B257BB9CCDDFA9663AABBA2AF3DF99595591F1AF5B2383514A29E15DDFD475552DA2F3E3689D7482B0C8736FC7A65E65A6582C8EEC64FBB603452653D18BB66B336516D5F164EDD40D2058085554E568C7D878637249CA0FD60B8B429479E1FDB8AA575596178BE368A7BEEB88582A234559779B52EBA25C046BA761000142C8A22C821BD6EB3393E54A4876AEF7BD50688C418066BD52C61C2D6F0CA39A7B8CB47BDB85A44D1E5CB7190382B8BF821A4819A324053B391F4869AD240222783759B7F564C06D505D64104A6B2948088CDE8D83DBBD9BDF47ED486D942452E0A6D1857029FC8F84525A0A9202839BFA696FF7B0DF57994C8ADF4224D8E7B14D0405828491AA0E768A7151962854E3DCD075B93699329377769A0A9367991E00DA668D28B272813EBA7120A045B10802D7930D61582E33A1D4D4F731C68AA4CE8B33E7DA61ACA492463BEFACB5B93145A67AC4BA5F4B12BAA880E3D877528952026565DB8F7D188A8516DAD8BE8D3E669934C6F810576DAD95C9941E839BEC9893CAB22A043B0C0349A34A3D21F1236E0677CFA70065566AA487C4C7486DE4C5182194CAC8F9D86224A9B2D9CF4999EC11FB0A658AD98F9FF62BFBDDACB81FFAD1DA222FAC73ED38491894CEA8A8BAB691A654C6488A1C43DFD6A8755E2EACED7DB75126D3A49C8B6DDF49415951F869D8B4B5CA2B9D69E77D3F39E7D75A6504380E3D85A88D2662E763D3D652E9B25A3ADBB9AED5C6E446DB08DD30483B9545E5DDD0B61B9597DA18EFDC689D77562B9395CBB16F83CE49991CD83185611414F2C5B1F7B65E9F71F40F891E7FD8B56FB34C3ECB79481FF761E9E1F3E11189006843D442E63A0F314C818DE0AA348C304E3D00085D02E2142342AC72ADB576D3C0CE662A334A4D21D8104A456591730C7E1C94D48532916070C1101C958608FD381073613221688ACC1C17B9D446073B46673395674A3A00EB7DA1A9CC33E4E8A75E0A91EB4C20BB10917859182168B00370CC4C2109AC1D62E42C534C387AF6E1827FF73CDB6C5FDB40E36796C76DCAC3CF39620C46E852A9AEAB51148B72119DB3A3034D79B5C06063B0089A4C6E64E468A7C9653ACF94B4CE8FD69334C7A50EDEF6933342DDA8165370D6592459E5257B373917582D8BCA47E7BC1FA2543A37CAF8307593CD456696C2FA3038474A2D3313C3D04D9311EA78B1B0C15AE704299D57C2FBE0AD6361F285011BBD1B22489D2D4905B6FD640B9D55A519260D5758948A8808B8737F7D568D5A1ED7B9F8B03D07020011994C0261D74D5DB376B6234153E0FAF41423B2CA3B1B574D33761B45CC40EB7ADD743DCB0C98366D5B6FD6314C28653F0CA7F5DA9214A487615A378D1D5BA184F571B55AB7CE93CC838BEB66D37535620054CDBAAEBB3E0A2340345DD734B577ADD0A6EF87555D0F91046576B4ABA6B643AB2479A6F5D9BDD1792DF2C9C3B069FB6E1321A0CA9BBADEF47D4411F94ABED4B8774DE05D6CD6B3C8E336E7E1E71C21463B59462A16A564F4C091625655463047475E2EF3DC1BC3317060A9F472B9E018BDB342A9E572B94B9427685155C14DEC2D932CCA220706EF9DE7BC2CB424E4E8BDD3797E9C9918BD774C461D1D551C39784B421E2FABC00CDE07C6E5A20ADE11DB80322F0A030C21B08F2ACB8EC12384E0ADD459AE9567E6C0A8C4D1D122C610FC8478A564B4BCCFFE0EF04432303D150ED773E02EBCB9C873EB43B3AE331211C13ADF371B1CC7A22AEA76B36A5AEF1D11B808ED66E3BA2ECF4C8470B65AF793456444E8A7A96F6A01A8B4AAEBBA6E7B8048C036F266BDC6C9E69999A6E16CDD4CCE49048ED0F6DDD46C7293C7185775BD19C66D2A83C987CDD9A942924A349B76B5E92207027081DB4DE3BBBE2ACBD1F677EB8DF70E115C80C18E5D735A2803A4FA7AC3E14A13D22F30A23FFB1CACE760DECDD2EFDCB96341BD74EBD6D9C7FF45E5CD0C052AE150A0831B6565914048E0A0B5D6129931782884110B0D52020011964511830F8C1AC45155795248C080B9115A6224011EAA2C579910822244A99492140204CF85CC4421A2D41123095514E8B40C1114A8E3B274249198198D365A02B38C0E16A6B28651E8084E2A25091CF3E0215782CA85914B79DDD358CF71B0CB4644C6C8CC3FFCEBBFF9F33F7DF5BFFFF39FF31BAFB4773F8E14A4CE7CB739BDF7294BAD9498BABA5BAF83ED94D618C2E9DD4F7AE74D9E073776ABD3A96B508094AAABEFAE37B5CC32416168EAA93EF3C1EAACB05D7BEFF4D380A48DB4C3A65F9FF9A9974A11F0EAEE279D1D4D99831F87BAB6ED19131B9D6DEA7BEB6645C6088A63D30CF5598C83364570C3DD7B9F780493693B34FDBAF6438D522A45F5EA7F9BC67DFD45B1BAFBE1DDD3355C50FFF3C3C17A0E428A107DF4AFBDF6ADB7DEFCF19B3F7DFBD7FFF1FE1F7FE7F5B3BBFF274C7EF3C57CF011380814C7378A00CA074B84E55129ABE3102244AACA9BB17A21C488C0C64869CA29780632DAE42F1DF900818314E2F8855726EF9959002E6FFC0E33B9105140B52855B1F421C4288A52D3E2251B02036B49272FFFC1E47D0454D2642F2E8DF1C22C000004E9494441547DC4C081041D9DBC6C962F708C046279C32028EB1D121512E95675530F1FBEFFDEF7BFF7DDD75FFF331FBD949F63B672FD38E4B042480CECBC7BF59BB7DF7EEBCDB7DEFEBBF77EFDEE8B37ABB37130266324377408A48C8924BC77E0ADD49A84F2E31883575AA352DE87E8AC10249409C1FB6992428A4C8788C14E04419A8C01DD38C2364C9CA4731EE224A522A9BD1D830DD248A1B40F1CECD40A102A8FC1BB6914424AAD19D1598B1CA4D240D28F3D47509942219DF31CDC460869CA0FEA0F7FF857DFFDD18FDE38393971C17D66BDE3F5E7F0365E8CEC827BF5B5DB3F7DF38D6FBFFBF7AFFCDED7A671DCAD0BDECEDAB64E0B08C0977D8D709733E4FC1F00970DF5F6DB08BBFD2EDC9EEDF6C3071E335CDE651BF2B73D1386FDB7F3E5DD909905D1A6EBBEFFBD1F9C9C9C4C7E1242F075F47F7C348F2D0EC299690AEE4665121439C618BFF9DAED377E7CFBE185BF3A8418A490DB4EF1FA8BE342865AC02F904DF011E600FBBA132800E14BC4C43F2B20A220717FFBE99ECC1307011480DCF7DCFE499A0E135DC58BE499E6FA0BE222086000D4FE16F6898A039EB7CABD06788008B08F99FD6D78D927BE1AF05E1CFBE9C073FAEC2FF1702E77F4491C8959923812B3A4394762CF76CEE1CF5D1352CF9198258923314B12476296248EC42C491C8959923812B32471246649E248CC92C49198258923314B12476296248EC42C491C8959923812B32471246649E248CC92C49198258923314B12476296248EC42C491C8959923812B32471242EC097B230A4752B893D04900168806DD2099BC491B888D82B030052CF913867BBE22D9CAF784BE2485C205C48FB9426A4894BA4140C892B928695C405E2A5CC3E491C893DFCA038D2B09298258923314B12476296248EC42C491C8959923812B32471246649E248CC92C49198258923314B12476296248EC42C0F8AC3C7AFBABB52E28BC00C3E3ED8F0F440919342213E67F6A9CF3D0C800827A50271A9E5CF5FD94706D0F2DDF7EFFDE1495618E1631484A91BB9DE2062888C80CDE8DFFD9F7B50C88BAE8DE7E20821CA42BDF30F1FFCC9D7177F79FBE6E4436010F47097B69D85ECB590CEF642AE684777C54BBE5F39CFB2CB1D03841081C148F99B8FEA777EF181342AD87375C88B452542DDB99FFDF2A3DF3D327F74AB02601F836778C830830091AF89B5FBB6BD0F2BF4FB477B867F3F02C1080180EFDF697FF6DE47F5C6995CF90B052E4582B9C0F942FFE2DF3E65869FFCC5EF7FFB6BD5CD52CB6B2281C467E1D3CEFECBC7ED3BEF7DF48FFFFE69B134938B171B1BE5DFFED3A5E20C99A6AEB1C7C7E607DF79F9F56FDCB8B5D45A1CFA8EF72A7ADB96B9E20F0F11900019B60EE857DFF12972DFBFFD01EE7BB6EFFA6C3E0FDC3BDC45D9103F69ECAF3E58FDFC5FEFACD753B9D4A38BC8E7BEA1F059716C910263643B067011E243C7952F87F83C7DE0BE537357381A322801C2804610082E02EC97601C1C3CDC61E5E541E7FEE6368A73FB45010102C0B4AB2FFFD9A37C5110811024692388D007DE9DC34327A417F1811121CB25164F664A7515716CD7E55DA53A10404400803C075210C6C3572500F0FEB4FDD57ABEAB1CD08FE7A208FB86C1070A1140B1FBEBA04DC1CC1C2078F6337A9F8D3E6706BFD3F513F801F215C4B1ADA9F0C862E785B727E981DCF9910F2E8E7850710040E04BE298ADE9273646C6477DF89496265CA5C91F0F0488308D00176A32CDA4BF1CD771DD4AD2C481F87F7D51A0E6EBC8651F0000000049454E44AE426082, CAST(1026.00000 AS Decimal(18, 5)))
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (2, 1, 1, 1, 9, 127, NULL, NULL, N'Anonymous User - Get Token Company=1 App=1')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 2, 1, NULL, NULL, NULL, N'Api User -  Company=1 App=1')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 3, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 4, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 5, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 6, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 7, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 8, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 9, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 10, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 11, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (3, 1, 1, 12, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 13, 1, NULL, NULL, NULL, N'Api User - Company=1 App=2')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 14, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 15, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 16, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 17, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 18, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 19, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 20, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 21, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 22, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (6, 1, 2, 23, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 24, 1, NULL, NULL, NULL, N'Api User -  Company=2 App=1')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 25, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 26, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 27, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 28, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 29, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 30, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 31, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 32, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 33, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (7, 2, 1, 34, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 35, 1, NULL, NULL, NULL, N'Api User -  Company=2 App=2')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 36, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 37, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 38, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 39, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 40, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 41, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 42, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 43, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 2, 44, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (8, 2, 1, 45, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 46, 1, NULL, NULL, NULL, N'Custom -  Company=1 App=1')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 47, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 48, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 49, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 50, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 51, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 52, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 53, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 54, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 55, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (4, 1, 1, 56, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 57, 1, NULL, NULL, NULL, N'Custom -  Company=1 App=2')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 58, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 59, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 60, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 61, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 62, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 63, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 64, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 65, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 66, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (5, 1, 2, 67, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 68, 1, NULL, NULL, NULL, N'Custom -  Company=2 App=1')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 69, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 70, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 71, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 72, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 73, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 74, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 75, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 76, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 77, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (9, 2, 1, 78, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 79, 1, NULL, NULL, NULL, N'Custom -  Company=2 App=2')
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 80, 2, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 81, 3, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 82, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 83, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 84, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 85, 7, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 86, 8, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 87, 9, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 2, 88, 10, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[RolePermissions] ([RoleFk], [CompanyFk], [ApplicationFk], [Id], [ModuleFk], [EndpointFk], [ComponentFk], [PropertyValue], [Obs]) VALUES (10, 2, 1, 89, 11, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 1, N'Vito-Transverse_Role_System', N'Vito-Transverse_Role_System_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 2, N'Vito-Transverse_Role_UnknownUser', N'Vito-Transverse_Role_UnknownUser_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 3, N'Vito-Transverse_Role_ApiUser', N'Vito-Transverse_Role_ApiUser_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 1, 4, N'Vito-Transverse_Role_0000000001', N'Vito-Transverse_Role_0000000001_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 2, 5, N'Vito-RealState_Role_0000000001', N'Vito-RealState_Role_0000000001_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (1, 2, 6, N'Vito-RealState_Role_ApiUser', N'Vito-RealState_Role_ApiUser_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 1, 7, N'LaTorres-Transverse_Role_ApiUser', N'LaTorres-Transverse_Role_ApiUser_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 2, 8, N'LaTorres-RealState_Role_ApiUser', N'LaTorres-RealState_Role_ApiUser_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 1, 9, N'LaTorres-Transverse_Role_0000000001', N'LaTorres-Transverse_Role_0000000001_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Roles] ([CompanyFk], [ApplicationFk], [Id], [NameTranslationKey], [DescriptionTranslationKey], [CreationDate], [CreatedByUserFk], [IsActive], [LastUpdateDate], [LastUpdateByUserFk]) VALUES (2, 2, 10, N'LaTorres-RealState_Role_0000000001', N'LaTorres-RealState_Role_0000000001_Dsc', CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, 1, NULL, NULL)
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (1, 1, 601, 1, N'Vito-Transverse_Role_', 1, N'0000000000')
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (1, 2, 601, 2, N'Vito-RealState_Role_', 1, N'0000000000')
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (2, 1, 601, 3, N'LaTorres-Transverse_Role_', 1, N'0000000000')
GO
INSERT [dbo].[Sequences] ([CompanyFk], [ApplicationFk], [SequenceTypeFk], [Id], [SequenceNameFormat], [SequenceIndex], [TextFormat]) VALUES (2, 2, 601, 4, N'LaTorres-Transverse_Role_', 1, N'0000000000')
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
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'system', 1, N'System', N'User', N'system@vito-torres-soft.com', N'system', 0, 0, 0, NULL, 0, N'55c26aaf-79b0-42e9-9adc-1f3fcba6d6d8', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 0, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'unknown', 2, N'Unknown', N'User', N'unknown@vito-torres-soft.com', N'unknown-user', 0, 0, 0, NULL, 0, N'bf3e34a8-b74e-49e3-9100-86d32e35d46c', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'api-user', 3, N'API', N'User', N'api.user@vito-torres-soft.com', N'api-user', 1, 0, 0, CAST(N'2025-05-06T23:53:01.237' AS DateTime), 0, N'6f8a48ad-0045-4c0b-8117-f65a60ba384c', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (1, N'ever.torresg', 4, N'Ever Alonso', N'Torres Galeano', N'eeatg844@hotmail.com', N'123', 0, 1, 0, CAST(N'2025-05-23T06:54:51.083' AS DateTime), 0, N'1356de0a-e0ca-4049-8c77-703fbee4126b', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (2, N'api-user', 5, N'API', N'User', N'api.user@proyectos-las-torres.com', N'api-user', 1, 0, 0, CAST(N'2025-05-07T00:05:58.563' AS DateTime), 0, N'15ea4ef1-8fca-42f1-b25e-fc52be4e4712', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Users] ([CompanyFk], [UserName], [Id], [Name], [LastName], [Email], [Password], [EmailValidated], [RequirePasswordChange], [RetryCount], [LastAccess], [ActivationEmailSent], [ActivationId], [IsLocked], [LockedDate], [CreationDate], [CreatedByUserFk], [LastUpdateDate], [UpdatedByUserFk], [Avatar], [IsActive]) VALUES (2, N'edgar.torres', 6, N'Edgar', N'Torres Agudelo', N'edgar.torres.g@proyectos-las-torres.com', N'456', 1, 1, 0, CAST(N'2025-05-23T19:16:31.960' AS DateTime), 0, N'414026c1-8bf2-41f2-97c2-c1a3b7f656b8', 0, NULL, CAST(N'2025-01-01T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_CompanyMembershipPermissions]    Script Date: 11/13/2025 12:36:01 AM ******/
CREATE NONCLUSTERED INDEX [IX_CompanyMembershipPermissions] ON [dbo].[CompanyMembershipPermissions]
(
	[CompanyMembershipFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CompanyMemberships]    Script Date: 11/13/2025 12:36:01 AM ******/
ALTER TABLE [dbo].[CompanyMemberships] ADD  CONSTRAINT [IX_CompanyMemberships] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CultureTranslations]    Script Date: 11/13/2025 12:36:01 AM ******/
CREATE NONCLUSTERED INDEX [IX_CultureTranslations] ON [dbo].[CultureTranslations]
(
	[ApplicationFk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Pictures]    Script Date: 11/13/2025 12:36:01 AM ******/
CREATE NONCLUSTERED INDEX [IX_Pictures] ON [dbo].[Pictures]
(
	[CompanyFk] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationLicenseTypes] ADD  CONSTRAINT [DF_ApplicationLicenseTypes_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ApplicationOwners] ADD  CONSTRAINT [DF_ApplicationOwners_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_Identifier]  DEFAULT (newid()) FOR [ApplicationClient]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_Secret]  DEFAULT (newid()) FOR [ApplicationSecret]
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
ALTER TABLE [dbo].[Endpoints] ADD  CONSTRAINT [DF_PagesModules_IsEnabled]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Entities] ADD  CONSTRAINT [DF_AuditEntities_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Entities] ADD  CONSTRAINT [DF_AuditEntities_IsSystemEntity]  DEFAULT ((0)) FOR [IsSystemEntity]
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
ALTER TABLE [dbo].[ApplicationOwners]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationOwners_ApplicationLicenseTypes] FOREIGN KEY([ApplicationLicenseTypeFk])
REFERENCES [dbo].[ApplicationLicenseTypes] ([Id])
GO
ALTER TABLE [dbo].[ApplicationOwners] CHECK CONSTRAINT [FK_ApplicationOwners_ApplicationLicenseTypes]
GO
ALTER TABLE [dbo].[ApplicationOwners]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationOwners_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[ApplicationOwners] CHECK CONSTRAINT [FK_ApplicationOwners_Applications]
GO
ALTER TABLE [dbo].[ApplicationOwners]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationOwners_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[ApplicationOwners] CHECK CONSTRAINT [FK_ApplicationOwners_Companies]
GO
ALTER TABLE [dbo].[AuditRecords]  WITH CHECK ADD  CONSTRAINT [FK_AuditRecords_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[AuditRecords] CHECK CONSTRAINT [FK_AuditRecords_Companies]
GO
ALTER TABLE [dbo].[AuditRecords]  WITH CHECK ADD  CONSTRAINT [FK_AuditRecords_Entities] FOREIGN KEY([EntityFk])
REFERENCES [dbo].[Entities] ([Id])
GO
ALTER TABLE [dbo].[AuditRecords] CHECK CONSTRAINT [FK_AuditRecords_Entities]
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
ALTER TABLE [dbo].[CompanyEntityAudits]  WITH CHECK ADD  CONSTRAINT [FK_CompanyEntityAudits_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyEntityAudits] CHECK CONSTRAINT [FK_CompanyEntityAudits_Companies]
GO
ALTER TABLE [dbo].[CompanyEntityAudits]  WITH CHECK ADD  CONSTRAINT [FK_CompanyEntityAudits_Entities] FOREIGN KEY([EntityFk])
REFERENCES [dbo].[Entities] ([Id])
GO
ALTER TABLE [dbo].[CompanyEntityAudits] CHECK CONSTRAINT [FK_CompanyEntityAudits_Entities]
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
ALTER TABLE [dbo].[Components]  WITH CHECK ADD  CONSTRAINT [FK_EndpointComponents_ModuleEndpoints] FOREIGN KEY([EndpointFk])
REFERENCES [dbo].[Endpoints] ([Id])
GO
ALTER TABLE [dbo].[Components] CHECK CONSTRAINT [FK_EndpointComponents_ModuleEndpoints]
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
ALTER TABLE [dbo].[Endpoints]  WITH CHECK ADD  CONSTRAINT [FK_Endpoints_Applications] FOREIGN KEY([ApplicationFk])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Endpoints] CHECK CONSTRAINT [FK_Endpoints_Applications]
GO
ALTER TABLE [dbo].[Endpoints]  WITH CHECK ADD  CONSTRAINT [FK_ModuleEndpoint_Modules] FOREIGN KEY([ModuleFk])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[Endpoints] CHECK CONSTRAINT [FK_ModuleEndpoint_Modules]
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
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_Companies] FOREIGN KEY([CompanyFk])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Companies]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_Entities] FOREIGN KEY([EntityFk])
REFERENCES [dbo].[Entities] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Entities]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_GeneralTypeItems] FOREIGN KEY([FileTypeFk])
REFERENCES [dbo].[GeneralTypeItems] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_GeneralTypeItems]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_GeneralTypeItems1] FOREIGN KEY([PictureCategoryFk])
REFERENCES [dbo].[GeneralTypeItems] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_GeneralTypeItems1]
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
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserRolePermissions_Endpoints] FOREIGN KEY([EndpointFk])
REFERENCES [dbo].[Endpoints] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_UserRolePermissions_Endpoints]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserRolePermissions_Modules] FOREIGN KEY([ModuleFk])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_UserRolePermissions_Modules]
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
               Bottom = 339
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
               Bottom = 305
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
         Table = 1176
         Output ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_CompanyUserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 720
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
               Top = 175
               Left = 48
               Bottom = 338
               Right = 316
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
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Begin CriteriaPane = 
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
         Configuration = "(H (1[42] 4[20] 2[24] 3) )"
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
         Table = 1176
         Output' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAuditRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 720
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAuditRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetAuditRecords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[16] 4[57] 2[6] 3) )"
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
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 1124
               Bottom = 170
               Right = 1392
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
         Width = 12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_GetRolePermissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'00
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
