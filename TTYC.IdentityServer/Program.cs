using TTYC.Application;
using TTYC.IdentityServer;
using TTYC.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection("ClientOptions").Bind(Config.ClientOptions);

builder.Services.AddIdentityServer()
	.AddDeveloperSigningCredential()
	.AddInMemoryApiScopes(Config.ApiScopes)
	.AddInMemoryClients(Config.Clients)
	.AddInMemoryIdentityResources(Config.IdentityResources)
	.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
	.AddProfileService<CustomProfileService>();

builder.Services.AddCors(options =>	
	options.AddPolicy("CorsPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.InitializePersistence(builder.Configuration);
builder.Services.InitializeApplication();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseIdentityServer();

app.Run();
