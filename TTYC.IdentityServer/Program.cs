using TTYC.Application;
using TTYC.Constants;
using TTYC.IdentityServer;
using TTYC.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection(ConfigurationConstants.ClientOptions).Bind(Config.ClientOptions);

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddProfileService<CustomProfileService>();

builder.Services.AddCors(options =>	
    options.AddPolicy(ConfigurationConstants.CorsPolicy, x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.InitializePersistence(builder.Configuration);
builder.Services.InitializeApplication(builder.Configuration);

var app = builder.Build();

app.UseCors(ConfigurationConstants.CorsPolicy);

app.UseIdentityServer();

app.Run();
