
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Interfaces.Email;
using API.Login.Domain.Interfaces.Infra;
using API.Login.Domain.Interfaces.Token;
using API.Login.Infra;
using API.Login.Infra.Contexts;
using API.Login.Infra.Users;
using API.Login.Service.Email;
using API.Login.Service.Token;
using API.Login.Service.Users;
using API.Login.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AppConfiguration.Initialize(builder.Configuration);
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddTransient<ControllerMessenger>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<SqlLiteContext>(o =>
 o.UseSqlite(AppConfiguration.GetSqlLiteConnectionString(), b => b.MigrationsAssembly("API.Login.Infra")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

