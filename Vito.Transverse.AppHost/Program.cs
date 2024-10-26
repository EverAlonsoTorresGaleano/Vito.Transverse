var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Vito_Transverse_Identity_Api>("vito-transverse-identity-api");

builder.Build().Run();
