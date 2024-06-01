using Mousam_App.Controllers.Service;
using Mousam_App.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// **************** Add JSON Resposne Service via DI ***************
builder.Services.AddHttpClient<MyJsonService>(x =>
    x.BaseAddress = new Uri("https://api.openweathermap.org")
);
builder.Services.AddHttpClient<MyCountryService>(x =>
    x.BaseAddress = new Uri("https://visser.io")
);
builder.Services.AddSession();


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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
