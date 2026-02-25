using Microsoft.EntityFrameworkCore;
using MvcCoreSessionEmpleados.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();
string connection = builder.Configuration.GetConnectionString("sqlHospital");

// Register DbContext
builder.Services.AddDbContext<MvcCoreSessionEmpleados.Data.HospitalContext>(options => options.UseSqlServer(connection));
// Register Repository
builder.Services.AddTransient<RepositoryEmpleado>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Empleados}/{action=IndexSessionSalarios}/{id?}")
    .WithStaticAssets();


app.Run();
