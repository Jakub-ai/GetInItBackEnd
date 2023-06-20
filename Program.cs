using System.Formats.Asn1;
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GetInItBackEnd;
using GetInItBackEnd.Authentication;
using GetInItBackEnd.Authorization;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Middleware;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.JobApplicationDto;
using GetInItBackEnd.Models.Validators;
using GetInItBackEnd.Seeders;
using GetInItBackEnd.Services.AccountServices;
using GetInItBackEnd.Services.ApplicationServices;
using GetInItBackEnd.Services.CompanyServices;
using GetInItBackEnd.Services.GptServices;
using GetInItBackEnd.Services.OfferServices;
using GetInItBackEnd.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Stripe;
using Account = GetInItBackEnd.Entities.Account;
using AccountService = GetInItBackEnd.Services.AccountServices.AccountService;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // for IFormFile
    c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
} );
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
builder.Services.AddDbContext<GetInItDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("GetInIt")));
builder.Services.AddScoped<TechnologySeeder>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
/*builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();*/
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddHttpClient<ChatGptService>();
builder.Services.AddScoped<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
builder.Services.AddScoped<IValidator<CreateAccountDto>, RegisterAccountDtoValidator>();
builder.Services.AddScoped<IValidator<CreateEmployeeDto>, RegisterEmployeeValidator>();
builder.Services.AddScoped<IValidator<UpdatePasswordDto>, UpdatePasswordValidator>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateEmailDto>, UpdateEmailValidator>();
builder.Services.AddScoped<IValidator<CreateJobApplicationDto>, CreateJobApplicationDtoValidator>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RoleManagerRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RoleEmployeeRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RoleUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AdminRoleRequirementHandler>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerRole", b => b.AddRequirements(new RoleRequirement(Role.ManagerCompanyAccount)));
    options.AddPolicy("EmployeeRole", b => b.AddRequirements(new RoleRequirement(Role.EmployeeAccount)));
    options.AddPolicy("UserRole", b => b.AddRequirements(new RoleRequirement(Role.UserAccount)));
    options.AddPolicy("AdminRole", b => b.AddRequirements(new RoleRequirement(Role.Admin)));
});

//builder.Services.AddScoped<IPasswordHasher<User>>()
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient",
        corsPolicyBuilder  =>
        {
            corsPolicyBuilder.AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000");
        });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
var app = builder.Build();
app.UseStaticFiles();
StripeConfiguration.ApiKey =
    "sk_test_51NECXpLpkQnyrIfCXAF91dHQTHQBIj4PCtBXiRTMumckFBOUezK7spzO8PhAStUZPiAGRarv1Su2HU9YERZz6oe600GEU7tbYo";
// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<TechnologySeeder>();
await seeder.Seed();
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

public partial class Program
{
    
}