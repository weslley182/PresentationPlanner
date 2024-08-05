using Microsoft.EntityFrameworkCore;
using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Repository;
using PresentationPlanner.Areas.Contacts.Services;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;
using PresentationPlanner.Shared.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContextPool<DbContext, PlannDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlannDB")));


builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IGenericRepository<Contact>, ContactRepository>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US" };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
    options.SupportedCultures = supportedCultures.Select(c => new System.Globalization.CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new System.Globalization.CultureInfo(c)).ToList();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
