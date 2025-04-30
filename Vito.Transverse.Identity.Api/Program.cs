using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Vito.Framework.Api.Exceptions;
using Vito.Framework.Api.Extensions;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Api.Endpoints;
using Vito.Transverse.Identity.Api.EndPoints;
using Vito.Transverse.Identity.Api.Validators;
using Vito.Transverse.Identity.BAL.IntegrationServices.Twilio;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;
using Vito.Transverse.Identity.BAL.TransverseServices.Security;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Security;
using Vito.Transverse.Identity.DAL.TransverseRepositories.SocialNetworks;
using Vito.Transverse.Identity.Domain.Constants;
using Vito.Transverse.Identity.Domain.Options;

try
{
    Console.WriteLine(IdentityConstants.Program_StartMessage);
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;


    builder.AddDefaultHsts(false);
    //Contains 
    //ConfigureOpenTelemetry()/AddDefaultHealthChecks()/AddServiceDiscovery()/AddDefaultHttpJsonOptions()
    //AddDefaultApiVersioning() / AddSwaggerInfo() / AddFeatureManagement()/ConfigureHttpClientDefaults / AddOptions();
    builder.AddPreBuildServiceDefaults();

    builder.Services.AddEndpointsApiExplorer();

    //Device Detection Wangkanai
    builder.Services.AddDetection();

    //Add OPtions
    builder.Services.AddOptions<CultureSettingsOptions>().BindConfiguration(CultureSettingsOptions.SectionName);
    builder.Services.AddOptions<DataBaseSettingsOptions>().BindConfiguration(DataBaseSettingsOptions.SectionName);
    builder.Services.AddOptions<EmailSettingsOptions>().BindConfiguration(EmailSettingsOptions.SectionName);

    // builder.Services.AddOptions<IdentityServiceClientSettingsOptions>().BindConfiguration(IdentityServiceClientSettingsOptions.SectionName);
    builder.Services.AddOptions<IdentityServiceServerSettingsOptions>().BindConfiguration(IdentityServiceServerSettingsOptions.SectionName);
    builder.Services.AddOptions<MemoryCacheSettingsOptions>().BindConfiguration(MemoryCacheSettingsOptions.SectionName);
    //builder.Services.AddOptions<RedisCacheSettingsOptions>().BindConfiguration(RedisCacheSettingsOptions.SectionName);
    builder.Services.AddOptions<TwilioSettingsOptions>().BindConfiguration(TwilioSettingsOptions.SectionName);


    //Memory Cache
    var memoryCacheOptions = configuration.GetSection(MemoryCacheSettingsOptions.SectionName).Get<MemoryCacheSettingsOptions>();
    builder.AddServiceForMemoryCache(memoryCacheOptions!.ExpirationScanFrequencyInSeconds);


    //RedisCache



    //Register  Data Context y Factory
    //var sqlDbConnectionName = "TransverseDB";
    //var dataBaseSettingOptions = configuration.GetSection(DataBaseSettingsOptions.SectionName).Get<DataBaseSettingsOptions>();
    //var transverseDBConnectionString = dataBaseSettingOptions!.ConnectionStrings!.First(x => x.ConnectionName.Equals(sqlDbConnectionName));
    builder.Services.AddTransient<IDataBaseServiceContext, DataBaseServiceContext>();
    //builder.AddSQLServerDbContextPool<DataBaseServiceContext>(transverseDBConnectionString);

    builder.Services.AddSingleton<IDataBaseContextFactory, DataBaseContextFactory>();

    builder.Services.AddDbContextFactory<DataBaseServiceContext, DataBaseContextFactory>(options =>
    {

    }, ServiceLifetime.Singleton);

    //builder.Services.AddSingleton<Func<IDataBaseServiceContext>>(implementationFactory: x => x.GetService<DataBaseServiceContext>);


    //Register Transverse Repositories
    builder.Services.AddTransient<ProblemDetailsFactory, CustomProblemDetailsFactory>();
    builder.Services.AddTransient<ICultureRepository, CultureRepository>();
    builder.Services.AddTransient<ILocalizationRepository, LocalizationRepository>();
    builder.Services.AddTransient<ISecurityRepository, SecurityRepository>();

    //Register REpositories
    builder.Services.AddTransient<ISocialNetworksRepository, SocialNetworksRepository>();

    //Register Transverse Services
    builder.Services.AddTransient<ICachingServiceMemoryCache, CachingServiceMemoryCache>();
    builder.Services.AddTransient<ICultureService, CultureService>();
    builder.Services.AddTransient<ILocalizationService, LocalizationService>();
    builder.Services.AddTransient<ISecurityService, SecurityService>();


    //Register Services
    builder.Services.AddTransient<ITwilioService, TwilioService>();

    //Register Fluent Validation
    builder.Services.AddValidatorsFromAssemblyContaining<TokenRequestValidator>();


    //Identity Service Server 
    var identityServiceServerOptions = configuration.GetSection(IdentityServiceServerSettingsOptions.SectionName).Get<IdentityServiceServerSettingsOptions>();
    builder.AddAuthenticationForJwtServer(identityServiceServerOptions);
    builder.Services.AddAuthorization();

    //Identity Service Client (Api server)
    //var identityServiceClientOptions  = configuration.GetSection(IdentityServiceClientSettingsOptions.SectionName).Get<IdentityServiceClientSettingsOptions>();
    //builder.AddAuthenticationForJwtClient(identityServiceClientOptions);
    //builder.Services.AddAuthorization();

    //Cors
    builder.AddCorsOnlyForAuthorizedUrls(identityServiceServerOptions!.AuthorizedUrls!);

    //Nswag
    builder.Services.AddOpenApiDocument();

    Console.WriteLine(IdentityConstants.Program_PreBuildMessage);
    var app = builder.Build();

    //MapperExtension.Configure(app.Services.GetService<ICultureService>()!);

    Console.WriteLine(IdentityConstants.Program_PostBuildMessage);

    app.UseCustomApiExceptionHandling();

    app.UsePostBuildApplicationDefaults();

    var versionSet = app.UseApiVersionSet(1.0, [1.0, 0.9]);

    app.MapDefaultEndpoints();

    //Applciation EndPOints
    app.MapHomeEndPoints(versionSet);
    app.MapSecurityEndpoint(versionSet);
    app.MapLocalizationEndpoint(versionSet);
    app.MapCultureEndpoint(versionSet);
    app.MapTwilioEndPoint(versionSet);

    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Container"))
    {
        app.UsePostBuildApplicationDefaultsDevelopment();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    Console.WriteLine(IdentityConstants.Program_PreRunMessage);
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(IdentityConstants.Program_ErrorMessage + ex.GetErrorStakTrace());
}