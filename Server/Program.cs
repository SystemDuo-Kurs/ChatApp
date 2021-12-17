using ChatApp.Server;
using ChatApp.Server.Hubs;
using ChatApp.Server.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Database"))
    );
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapHub<ChatHub>("chathub");
app.MapHub<UserHub>("userhub");


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
