using AmitWebApis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,      //Valid the server and generate token
            ValidateAudience=true,      // validate recepient is authorize to receive
            ValidateLifetime=true,       //check for expire time and authorization valid or not
            ValidIssuer = builder.Configuration[ "Jwt:Issuer" ],
            ValidAudience = builder.Configuration[ "Jwt:Audience"],
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key" ]))  // validate signature of the token
        };
    });

builder.Services.AddControllers();

var connstr=builder.Configuration.GetConnectionString("conn");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connstr), ServiceLifetime.Singleton

);

builder.Services.AddScoped<IEmployeeRepository , EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
