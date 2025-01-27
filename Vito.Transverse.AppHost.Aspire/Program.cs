var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Vito_Transverse_Identity_Api>("vito-transverse-identity-api");

builder.Build().Run();


//var builder = DistributedApplication.CreateBuilder(args);

//var apiService = builder.AddProject<Projects.Vito_Trtansverse_AppHost_Aspire_ApiService>("apiservice");

//builder.AddProject<Projects.Vito_Trtansverse_AppHost_Aspire_Web>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithReference(apiService)
//    .WaitFor(apiService);

//builder.Build().Run();
