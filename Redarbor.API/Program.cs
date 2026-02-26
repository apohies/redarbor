using System.Text;
using Redarbor.Infrastructure;
using Redarbor.API.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure (EF Core + Dapper + Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// MediatR - scans Application assembly for handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Redarbor.Application.Commands.CreateEmployee.CreateEmployeeCommand).Assembly));

builder.Services.AddValidatorsFromAssembly(
    typeof(Redarbor.Application.Commands.CreateEmployee.CreateEmployeeCommand).Assembly);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();