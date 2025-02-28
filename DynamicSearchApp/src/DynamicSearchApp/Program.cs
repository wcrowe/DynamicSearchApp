using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;
using DynamicSearchApp.State;
using DynamicSearchApp.Services;
using Fluxor;
using Microsoft.Extensions.Http;  // Added for AddHttpClient

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
builder.Services.AddScoped<IState<SearchState>>();
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

// CORS for WebAssembly
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
        policy.WithOrigins("https://localhost:5001")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseStaticFiles();  // Serve wwwroot files (e.g., index.html)
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

app.Run();