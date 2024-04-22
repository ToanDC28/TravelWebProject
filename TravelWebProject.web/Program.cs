using dotenv.net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;
using TravelWebProject.repo.Users;
using TravelWebProject.service.Authentication;
using TravelWebProject.service.Bank;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.Users;
using TravelWebProject.web;
DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {".env"}));
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Host.ConfigureLogging((context, logging) =>
{
    logging.ClearProviders();
    logging.AddSerilog(logger: new LoggerConfiguration()
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger());
});
//Authen
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MyCookieAuth";
        options.LoginPath = "/SignIn";
        options.AccessDeniedPath = "/AccessDenied";
        options.LogoutPath = "/SignOut";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
builder.Services.AddHttpClient<BankService>();
builder.Services.AddHostedService<PeriodicLoginBackgroundService>();
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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();
