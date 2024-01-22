using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<Students>();
builder.Services.AddSingleton<Courses>();
builder.Services.AddSingleton<Teachers>();
builder.Services.AddSingleton<Categories>();
builder.Services.AddSingleton<UserAccounts>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
