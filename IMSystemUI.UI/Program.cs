using IMSystemUI.UI.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureService();
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpClient<IHttpClientExtensions, HttpClientExtensions>();

builder.Services.AddHttpContextAccessor();

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

//app.UseMiddleware<TokenBearerMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


var contentRoot = app.Environment.WebRootPath;

Rotativa.AspNetCore.RotativaConfiguration.Setup(contentRoot, "rotativa");

app.Run();
