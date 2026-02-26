using Redarbor.Infrastructure;
using Redarbor.API.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure (EF Core + Dapper + Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// MediatR - scans Application assembly for handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Redarbor.Application.Commands.CreateEmployee.CreateEmployeeCommand).Assembly));

builder.Services.AddValidatorsFromAssembly(
    typeof(Redarbor.Application.Commands.CreateEmployee.CreateEmployeeCommand).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();