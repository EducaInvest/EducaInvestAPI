using EducaInvestAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var options = new JsonSerializerOptions()
{
    NumberHandling = JsonNumberHandling.AllowReadingFromString |
     JsonNumberHandling.WriteAsString
};


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoAzure"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();