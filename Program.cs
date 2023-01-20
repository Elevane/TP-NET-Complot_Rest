using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;
using TP_Complot_Rest.Common;
using TP_Complot_Rest.Common.Helpers;
using TP_Complot_Rest.Managers;
using TP_Complot_Rest.Managers.Interfaces;
using TP_Complot_Rest.Persistence;
using TP_Complot_Rest.Repositories;
using TP_Complot_Rest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
const string appCorsPolicy = "_myAllowSpecificOrigins";
builder.Services.AddDbContext<UserContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            ));
});
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: appCorsPolicy, policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<SourceSettings>(builder.Configuration.GetSection("SourceSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IComplotManager, WikipediaManager>();
builder.Services.AddScoped<IPersistenceManager, SqlManager>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
var app = builder.Build();

//Logger
app.Use(async (context, next) =>
{
    var method = context.Request.Method;
    var route = context.Request.Path;
    var time = DateTime.Now;
    Console.WriteLine("                                                   ");
    Console.WriteLine($"---- Request == > [{method.ToUpper()}] '{route}'  at  {time}");
    Console.WriteLine("                                                   ");
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(appCorsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();