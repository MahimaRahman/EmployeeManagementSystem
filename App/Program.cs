using BLL.Services;
using DAL.EF;
using DAL.Repos;
using EMS.DAL.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddScoped<EmployeeRepo>();
builder.Services.AddScoped<DepartmentRepo>();
builder.Services.AddScoped<AttendanceRepo>();
builder.Services.AddScoped<LeaveRepo>();
builder.Services.AddScoped<PayrollRepo>();
builder.Services.AddScoped<AuthRepo>();
builder.Services.AddScoped<ReportRepo>();
builder.Services.AddScoped<NotificationRepo>();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<LeaveService>();
builder.Services.AddScoped<PayrollService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<NotificationService>();


builder.Services.AddDbContext<EmployeeDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});




builder.Services.AddDistributedMemoryCache();       //////added later
builder.Services.AddSession();                        ///  added later




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

app.UseSession();

app.UseAuthorization(); ///////////////////////////   add later

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();



