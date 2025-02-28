using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;
using DynamicSearchApp.State;
using DynamicSearchApp.Services;
using Fluxor;
using Microsoft.AspNetCore.Components; // Ensure this is included

var builder = WebApplication.CreateBuilder(args);

// Enable Windows Authentication
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();
builder.Services.AddAuthorization();

// Add Blazor services with Auto support
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Telerik UI for Blazor
builder.Services.AddTelerikBlazor();

// Fluxor state management
builder.Services.AddFluxor(o => o
    .ScanTypes(typeof(Program)));

// Services
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DataService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddControllers();

// HttpClient for WebAssembly mode with Windows Authentication
builder.Services.AddHttpClient("BlazorClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseUrl") ?? "https://localhost:5001/");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseDefaultCredentials = true
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
        policy.WithOrigins("https://localhost:5001")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowBlazorClient");
app.UseAntiforgery();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapRazorComponents<DynamicSearchApp.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Program).Assembly);

// Define static render modes for use in Razor components
app.Run();

// Add this static class at the bottom of Program.cs
public static class RenderModes
{
    public static readonly IComponentRenderMode InteractiveAuto = RenderMode.InteractiveAuto;
}