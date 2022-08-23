using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TTYC.Application;
using TTYC.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "TTYC"
	});

    options.CustomSchemaIds(type => type.ToString());

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.OAuth2,
		Flows = new OpenApiOAuthFlows
		{
			Password = new OpenApiOAuthFlow
			{
				TokenUrl = new Uri("https://localhost:7294/connect/token"),
				Scopes = new Dictionary<string, string>
				{
					{"ClientAPI", "ClientAPI" }
				}
			}
		}
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Id="oauth2",
					Type=ReferenceType.SecurityScheme
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	options.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
	options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignOutScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
})
	.AddIdentityServerAuthentication(options =>
	{
		options.ApiName = "ClientAPI";
		options.Authority = "https://localhost:7294";
		options.RequireHttpsMetadata = false;
		options.LegacyAudienceValidation = true;
        options.RoleClaimType = "role";
    });

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
    options.AddPolicy("User", policy => policy.RequireClaim("role", "user"));
});

builder.Services.InitializePersistence(builder.Configuration);
builder.Services.InitializeApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientAPI");
		options.DocumentTitle = "ClientAPI";
		options.OAuthClientId("clientAPI");
		options.OAuthClientSecret("secret");
	});
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
