using  WebAPIWDapper.Services;
using Dapper;
using Prometheus;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPIWDapper.BusinessLogic;



var builder = WebApplication.CreateBuilder(args);
EncryptionBLService encryptionBLService = new EncryptionBLService(builder.Configuration);
//Dapper
DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

//adding authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// create code for encryption and decryption of values


//adding metrics related to HTTP
app.UseHttpMetrics(options =>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//adding the Metric Endpoint
app.UseMetricServer();

//auth
app.UseAuthentication();
await encryptionBLService.LoadAndCacheEncryptionKeys();
app.Run();
