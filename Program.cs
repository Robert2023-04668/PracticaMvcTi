using Microsoft.EntityFrameworkCore;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Register HttpClient before Build
builder.Services.AddHttpClient<DepartamentosApiClient>();
builder.Services.AddHttpClient<PuestosApiClient>();
builder.Services.AddHttpClient<EstadosApiClient>();
builder.Services.AddHttpClient<EmpleadoApiClient>();
builder.Services.AddHttpClient<ImagenesApiClient>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.MapStaticAssets();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
