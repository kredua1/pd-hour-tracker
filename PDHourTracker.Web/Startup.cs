using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Infrastructure.Data;
using PDHourTracker.Infrastructure.Identity;
using PDHourTracker.Infrastructure.Providers;
using PDHourTracker.Web.Configs;
using PDHourTracker.Web.Health;
using PDHourTracker.Web.Helpers;
using PDHourTracker.Web.Middleware;
using PDHourTracker.Web.Services;
using PDHourTracker.Web.Services.Interfaces;
using System.IO;

namespace PDHourTracker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddRazorPages();

            // Adding forwarding to ensure https is maintained for external auth providers
            // Per MS Docs: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-2.2
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // Persist data protection keys to file system
            // data-protection-keys is volume claim name on openshift
            // Reference: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-2.2
            // This is for ValidateAntiForgeryToken in logins
            services.AddDataProtection()
                .SetApplicationName("pd_hour_tracker")
                .PersistKeysToFileSystem(new DirectoryInfo("data-protection-keys"));

            // Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("PDHourTrackerConnection")));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                // Require user to confirm email to sign in
                config.SignIn.RequireConfirmedEmail = false;
                config.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Register claims principal factory
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                    AppClaimsPrincipalFactory>();

            // Register custom claims principal
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                    CustomUserClaimsPrincipalFactory>();

            // Add external authentication
            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Auth_Google_Id"];
                    googleOptions.ClientSecret = Configuration["Auth_Google_Secret"];
                })
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = Configuration["Auth_Microsoft_Id"];
                    microsoftOptions.ClientSecret = Configuration["Auth_Microsoft_Secret"];
                })
                .AddTwitter(twitterOptions =>
                {
                    twitterOptions.ConsumerKey = Configuration["Auth_Twitter_Id"];
                    twitterOptions.ConsumerSecret = Configuration["Auth_Twitter_Secret"];
                })
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Error/AccessDenied";
                    options.LoginPath = "/Account/Login";
                });

            //Add Repos for dependency injection
            services.AddScoped(typeof(IAgencyRepo<Agency>), typeof(AgencyRepo<Agency>));
            services.AddScoped(typeof(IAttendeeHourRepo<AttendeeHour>), typeof(AttendeeHourRepo<AttendeeHour>));
            services.AddScoped(typeof(IAttendeeRepo<Attendee>), typeof(AttendeeRepo<Attendee>));
            services.AddScoped(typeof(IEmployeeRepo<Employee>), typeof(EmployeeRepo<Employee>));
            services.AddScoped(typeof(IProjectRepo<Project>), typeof(ProjectRepo<Project>));
            services.AddScoped(typeof(IProviderCodeRepo<ProviderCode>), typeof(ProviderCodeRepo<ProviderCode>));
            services.AddScoped(typeof(ISignOutSheetUploadRepo<SignOutSheetUpload>),
                typeof(SignOutSheetUploadRepo<SignOutSheetUpload>));
            services.AddScoped(typeof(IWorkshopRepo<Workshop>), typeof(WorkshopRepo<Workshop>));

            // Add Providers. Custom return types of queries returning DTO's
            services.AddScoped(typeof(IAttendeeHourProvider), typeof(AttendeeHourProvider));
            services.AddScoped(typeof(IAttendeeReportProvider), typeof(AttendeeReportProvider));

            // Local Services - Repo helpers
            services.AddScoped(typeof(IAgencyService), typeof(AgencyService));
            services.AddScoped(typeof(IAttendeeService), typeof(AttendeeService));
            services.AddScoped(typeof(IAttendeeHourService), typeof(AttendeeHourService));
            services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            services.AddScoped(typeof(IProjectService), typeof(ProjectService));
            services.AddScoped(typeof(IProviderCodeService), typeof(ProviderCodeService));
            services.AddScoped(typeof(ISignOutSheetUploadService), typeof(SignOutSheetUploadService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IWorkshopService), typeof(WorkshopService));
            services.AddScoped(typeof(IExcelService), typeof(ExcelService));


            // AutoMapper Configs
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Kubernetes Health Checks : Liveness and Readiness
            services.AddHealthChecks()
                .AddCheck<LivenessHealthCheck>("Liveness", failureStatus: null)
                .AddCheck<ReadinessHealthCheck>("Readiness", failureStatus: null);
            services.AddSingleton<HealthStatusData>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            // Custom exception handler to log all errors
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // Tell app to use authentication
            app.UseAuthentication();
            app.UseAuthorization();

            // .Net Core 3.1 update
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            // Kubernetes Health Checks : Liveness and Readiness
            app.UseHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = check => check.Name == "Liveness"
            });

            app.UseHealthChecks("/health/ready", new HealthCheckOptions()
            {
                Predicate = check => check.Name == "Readiness"
            });
        }
    }
}
