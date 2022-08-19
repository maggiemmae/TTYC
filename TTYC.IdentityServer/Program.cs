using TTYC.Application;
using TTYC.IdentityServer;
using TTYC.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
	.AddDeveloperSigningCredential()
	.AddInMemoryApiScopes(Config.ApiScopes)
	.AddInMemoryClients(Config.Clients)
	.AddInMemoryIdentityResources(Config.IdentityResources)
	.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//builder.Services.Configure<ClientOptions>(builder.Configuration.GetSection(ConfigurationConstants.ClientOptions));

builder.Services.InitializePersistence(builder.Configuration);
builder.Services.InitializeApplication();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseIdentityServer();

app.Run();
