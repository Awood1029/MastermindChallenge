using MastermindChallenge.Blazor.Server.Services;
using MastermindChallenge.Blazor.Server.Services.Authentication;
using MastermindChallenge.Blazor.Server.Services.Game;
using MastermindChallenge.Blazor.Server.Services.Multiplayer;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using MastermindChallenge.Blazor.Server.Providers;
using System.IdentityModel.Tokens.Jwt;
using MastermindChallenge.Blazor.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri(builder.Configuration["BaseAddress"]));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IMultiplayerService, MultiplayerService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => 
                p.GetRequiredService<ApiAuthenticationStateProvider>());
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

app.UseEndpoints( endpoints =>
{
    endpoints.MapHub<MultiplayerHub>("/connect");
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
