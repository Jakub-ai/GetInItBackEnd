using System.Formats.Asn1;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GetInItBackEnd;
using GetInItBackEnd.Authentication;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Middleware;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Validators;
using GetInItBackEnd.Services.AccountServices;
using GetInItBackEnd.Services.CompanyServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddControllers();
builder.Services.AddDbContext<GetInItDbContext>();
builder.Services.AddScoped<CompanySeeder>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
builder.Services.AddScoped<IValidator<CreateAccountDto>, RegisterAccountDtoValidator>();
//builder.Services.AddScoped<IPasswordHasher<User>>()
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient",
        corsPolicyBuilder  =>
        {
            corsPolicyBuilder.AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:5099");
        });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
/*var seeder = scope.ServiceProvider.GetRequiredService<CompanySeeder>();
await seeder.Seed();*/
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //  app.UseExceptionHandler("Home/Error");

}
/*app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();*/
app.UseCors("FrontEndClient");
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetInIt API");
} );
app.UseAuthorization();

app.MapControllers();

app.Run();
