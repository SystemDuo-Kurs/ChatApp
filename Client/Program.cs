using Blazored.LocalStorage;
using ChatApp.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<SignalRService>();
builder.Services.AddTransient<ChatApp.Client.Models.IUserModel, ChatApp.Client.Models.User>();
builder.Services.AddTransient<ChatApp.Client.Models.IMessageModel, ChatApp.Client.Models.Message>();

builder.Services.AddTransient<ChatApp.Client.ViewModels.IRegistration, ChatApp.Client.ViewModels.Registration>();
builder.Services.AddTransient<ChatApp.Client.ViewModels.IMessageSend, ChatApp.Client.ViewModels.MessageSend>();
builder.Services.AddTransient<ChatApp.Client.ViewModels.IMessageList, ChatApp.Client.ViewModels.MessageList>();


builder.Services.AddTransient<AuthService>();

builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();
app.Services.GetService<SignalRService>();

await app.RunAsync();
