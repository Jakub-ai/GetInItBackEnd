using System.Formats.Asn1;
using GetInItBackEnd;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Middleware;
using GetInItBackEnd.Services.AccountServices;
using GetInItBackEnd.Services.CompanyServices;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
var app = builder.Build();

// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<CompanySeeder>();
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
