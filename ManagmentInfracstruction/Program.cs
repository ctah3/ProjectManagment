using ManagmentInfracstruction;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Npgsql.NameTranslation;
using ProjectManagment_class.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddAuthorization();
//builder.Services.AddDbContext<ProjectContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//);
//var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
//dataSourceBuilder.MapEnum<ProjectStatusEnum>("project_status_enum", nameTranslator: new NpgsqlNullNameTranslator());
//var dataSource = dataSourceBuilder.Build();
//builder.Services.AddDbContext<ProjectContext>(options =>
//    options.UseNpgsql(dataSource)
//);
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));


var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseNpgsql(dataSource));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Project}/{action=Index}/{id?}");

app.Run();
