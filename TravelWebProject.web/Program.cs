using System.Security.Claims;
using BusinessObject;
using BusinessObject.Models;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;
using TravelWebProject.repo.TransactionRepo;
using TravelWebProject.repo.Users;
using TravelWebProject.service.Authentication;
using TravelWebProject.service.Bank;
using TravelWebProject.service.BookingService;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.ItineraryServices;
using TravelWebProject.service.Mail;
using TravelWebProject.service.RegionService;
using TravelWebProject.service.RegionServices;
using TravelWebProject.service.TourPlanServices;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.TransactionService;
using TravelWebProject.service.TransportServices;
using TravelWebProject.service.Users;
using TravelWebProject.web;
DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {".env"}));
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
//Seed data
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    try 
    {
        SeedData.Initialize(scope.ServiceProvider);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
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
        options.LogoutPath = "/Logout";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

// Create 2 policies for admin and user
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => 
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimTypes.Role, "ADMIN");
    });
    options.AddPolicy("Customer", policy => 
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimTypes.Role, "CUSTOMER");
    });
    options.AddPolicy("AdminAndCustomer", policy => 
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context => 
        {
            return context.User.HasClaim(ClaimTypes.Role, "ADMIN") || context.User.HasClaim(ClaimTypes.Role, "CUSTOMER");
        });
    });
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourPlanService, TourPlanService>();
builder.Services.AddScoped<ITransportService, TransportService>();
builder.Services.AddScoped<IItineraryService, ItineraryService>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddHttpClient<BankService>();
builder.Services.AddHostedService<PeriodicLoginBackgroundService>();
builder.Services.AddTransient<Booking>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
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
