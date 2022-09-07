﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ControleDeVendas.Data;

//Parte do Builder;
var builder = WebApplication.CreateBuilder(args);

//Add database;
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
options.UseMySql("server=localhost;user=root;password=2530;database=saleswebmvcappdb"
, MySqlServerVersion.Parse("8.0.30-mysql")
, builder => builder.MigrationsAssembly("ControleDeVendas")));

/*
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMvcContext"), 
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebMvcContext")),
    builder => builder.MigrationsAssembly("ControleDeVendas")));
*/

// create a SeedingService Scope
builder.Services.AddScoped<SeedingService>(); //can be placed among other "AddScoped" - above: var app = builder.Build();   


// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    // ambiente de des...

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

SeedDatabase();

app.UseStaticFiles();




app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase() //Cria uma função
{
    using (var scope = app.Services.CreateScope()) //Cria um escopo dentro do App
    {

        var dbInitializer = scope.ServiceProvider.GetRequiredService<SeedingService>(); //Carrega o serviço
        dbInitializer.Seed(); //Roda a funcao do seedingService
    }
}