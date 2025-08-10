using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data;
using Travel_Info.Data.Configuration;
using Travel_Info.Data.Models;
using Travel_Info.Services.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
    options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedEmail");
    options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:RequiredLength");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddFacebook(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"];
    });

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddRazorPages();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.Use((context, next) =>
{
	if (context.User.Identity?.IsAuthenticated == true && context.Request.Path == "/")
	{
		if (context.User.IsInRole("Admin"))
		{
			context.Response.Redirect("/Admin/Home/Index");
			return Task.CompletedTask;
		}
	}
	return next();
});
app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Errors",
    pattern: "{controller=Home}/{action=Index}/{statusCode?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	RolesSeeder.SeedRoles(services);
	RolesSeeder.AssignAdminRole(services);
}

app.Run();
