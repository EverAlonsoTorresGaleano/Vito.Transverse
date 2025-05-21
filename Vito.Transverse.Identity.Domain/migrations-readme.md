**Migrations**

After Set up Data Context using EF



===============
Install Tool
===============


Check Version
>dotnet-ef --version

Menu View /Other Windows/Package Manager Console
Go to Package Manager Console
>cd *domain
>dotnet-ef tool restore


Install Lattest Tool Version
>dotnet tool install --global dotnet-ef

//Updating to the new tool version
//>dotnet tool update --global dotnet-ef

Check if is installed
	>dotnet-ef


Go to Domain Project PAckage Manager and install 
- Microsoft.EntityFrameworkCore.Core
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.SqlServer

Go to Api Project Ans Install
- Microsoft.EntityFrameworkCore.Design


===============

Create Secret File
===============

{
  "ConnectionStrings": {
    "SqlServerDataBaseEF": "",
    "SqlServerDataBase": ""
  }
}

add attribute to domain project and api project
 <InvariantGlobalization>false</InvariantGlobalization>


===============
Autogenerate Classes based on Db Model
===============

Set Vito.Transverse.Identity.Api as startup Project

Go to Package Manager Console
	on Default Project DropDown Select Domain

Gotto phisical Folder
	>cd **domain



Scaffold-DbContext "Server=(local)\MSSQLSERVER2019;Database=Vito.Transverse;Integrated Security=false;TrustServerCertificate=True;Trusted_Connection=True;Persist Security Info=True; User ID=vito.torres;Password=Vito87152+24Oct*" Microsoft.EntityFrameworkCore.SqlServer -force -o "Models"
 Scaffold-DbContext @Server=(local);Database=Vito.Transverse;Integrated Security=true;TrustServerCertificate=True;Persist Security Info=True; Encrypt=Optional;Command Timeout=120;MultipleActiveResultSets=true;Max Pool Size=200;Application Name=Vito.Transverse;" Microsoft.EntityFrameworkCore.SqlServer -force -o "Models"
or

--Local with connection string hardcoded
dotnet ef dbcontext scaffold "Server=(local);Database=Vito.Transverse.DB;Integrated Security=false;TrustServerCertificate=True;Persist Security Info=True; Encrypt=Optional;Command Timeout=120;MultipleActiveResultSets=true;Max Pool Size=200;User ID=sa;Password=VitoLaptop2025+;Application Name=Vito.Transverse;" Microsoft.EntityFrameworkCore.SqlServer --force -o "Models"

--Container 
dotnet ef dbcontext scaffold "Server=127.0.0.1,1401;Database=Vito.Transverse.DB;Integrated Security=false;TrustServerCertificate=True;Persist Security Info=True; Encrypt=Optional;Command Timeout=120;MultipleActiveResultSets=true;Max Pool Size=200;User ID=sa;Password=Vito2025+;Application Name=Vito.Transverse;" Microsoft.EntityFrameworkCore.SqlServer --force -o "Models"


Copy Infor From Context File VitoTransverseContext 
 all DbSet

   public virtual DbSet<Application> Applications { get; set; }
   ...

And Method OnModelCreating

To DataBaseServiceContext

And All  DbSet without virtual

   DbSet<Application> Applications { get; set; }
   ...

   To IDataBaseServiceContext

Start up project
 <PublishAot>false</PublishAot>


===============
REstore Database From MIgrations
===============
Go to Package Manager Console
>dotnet tool install --global dotnet-ef	
>cd *core
>dotnet-ef database update

===============
Add First Migration
===============
	Create Data Base on SQL Server
	Go to Package Manager Console
	on Default Project DropDown Select Vito.ProductTracking.DataAccessLayer
	>Add-Migration "Initial_Migration"

===============
Add Migration
===============
	generate Migration Whem Data Base Model Changes (New Tables New Fields)
	Go to Database and Create or update Db Objects
	Go to Package Manager Console
	on Default Project DropDown Select Vito.ProductTracking.Domain
	>Scaffold-DbContext "Server=localhost;Database=Vito.ProductTracking;Integrated Security=SSPI;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -force -o "Models"
	Solve Issues / Compile App , When App Compile then
	on Default Project DropDown Select Vito.ProductTracking.DataAccessLayer
	>Add-Migration "TableName_AddFieldName" 

	to create DTO Class based on DbModel for New Tables


===============
ADD Script on MIgration 
===============

ON MIgration File Up MEthod

 migrationBuilder.Sql(@"
                Delete from [dbo].[Cities];
                Begin Transaction;
				...
				 INSERT INTO [dbo]....
				...
				End Transaction");


===============
Remove LastMigration
===============	
	Go to Package Manager Console
	on Default Project DropDown Select Vito.ProductTracking.DataAccessLayer
	Remove-Migration

===============
Generate DB Script
===============

dotnet-ef migrations script --context PricingPlatformDbContext --output "Migrations/SQL/vito-transverse-db-migrations.sql" --idempotent
Script-migration


"AutoAddMissingTranslations":true,
       "ApplicationTitle" = "Vito.ProductTracking.Api",
     "ApplicationVersion" = "Versión 1",
   "SwaggerApplicationName" = "v1",
   
   
   
   $(NSwagExe_Net60) run nswag.json /variables:Configuration=$(Configuration)
   Request.Headers.Referer.ToString()
   Request.Headers.UserAgent.ToString()
   Request.Headers.Authorization.ToString()
   
   
   bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiVXNlclJvbGVfU3VwZXJBZG1pbiIsImV4cCI6MTY4ODc5MjY3OSwiaXNzIjoiVml0b0FwaSIsImF1ZCI6IlZpdG8jQXBpKyJ9.SWJfn9q4-dSPRN4l7AjIMlBBPYYFVyU43CLxYe1HrKU

 Fix Store Procedure Migrations

**My findings about Store proceduce/Functions on Migrations File**


- Is mandatory to avoid GO Statements on Store procedure or functions creations, because every go create a migration conditions like (IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230908194006_Create_SP_GetExclusionsTilesList'))
- Is mandatory scape script use '' instead ''
- At begin of script include EXEC('
- Only One Database Object per Block

**Obs**: To confirm if migration will work on dev environment please ON Package Manager use

dotnet-ef migrations script --context PricingPlatformDbContext --output "Migrations/SQL/essex-pricing-platform-db-migrations.sql" --idempotent

execute the generated file on Local and confirm that works fine