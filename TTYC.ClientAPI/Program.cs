using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Reflection;
using TTYC.Application;
using TTYC.Constants;
using TTYC.Persistence;

var builder = WebApplication.CreateBuilder(args);

var authenticationOptions = new AuthOptions();
builder.Configuration.GetSection(ConfigurationConstants.AuthenticationOptions).Bind(authenticationOptions);
builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection(ConfigurationConstants.StripeOptions));

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
                TokenUrl = new Uri(authenticationOptions.TokenUrl),
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
        options.ApiName = authenticationOptions.ApiName;
        options.Authority = authenticationOptions.Authority;
        options.RequireHttpsMetadata = false;
        options.LegacyAudienceValidation = true;
        options.RoleClaimType = "role";
    });

builder.Services.AddAuthorization();

builder.Services.InitializePersistence(builder.Configuration);
builder.Services.InitializeApplication(builder.Configuration);

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
