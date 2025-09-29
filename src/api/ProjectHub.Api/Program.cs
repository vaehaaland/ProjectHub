using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using ProjectHub.Infrastructure.Data;
using ProjectHub.Application.Projects;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
  configuration
      .ReadFrom.Configuration(context.Configuration)
      .Enrich.FromLogContext();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ProjectMappingProfile));
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProjectDtoValidator>();
builder.Services.AddControllers();
// TODO: Register AutoMapper, DTO validators, and ProblemDetails config once REST APIs are added.
builder.Services.AddDbContext<ProjectHubDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStatusCodePages();
// TODO: Plug in exception handling / ProblemDetails middleware before exposing controllers.
app.MapControllers();

app.Run();
