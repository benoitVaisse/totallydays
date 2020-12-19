using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Totallydays.BackGrounService;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;
using Totallydays.ViewsModel;

namespace Totallydays
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
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<TotallydaysContext>().AddDefaultTokenProviders();

            // redirection sur la page de connextion si je dois etre connecter pour accéder à une page
            services.ConfigureApplicationCookie(options => options.LoginPath = "/login");

            services.AddDbContext<TotallydaysContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("Totallydays"));
            });



            MailKitOptions mailKitOption = Configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config => config.UseMailKit(mailKitOption));

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = this.Configuration["apis:google:id"];
                options.ClientSecret = this.Configuration["apis:google:secret"];
            });
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // csrf
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "__RequestVerificationToken";
            });

            // recaptcha
            services.Configure<ReCaptchaSetting>(this.Configuration.GetSection("googleReCaptcha"));
            // cron job
            services.AddHostedService<TestBackgroundService>();
            services.AddHostedService<SendMailBookingFinichBackgroundService>();
            services.AddHostedService<SendMailHostingBookingPendingBackgroundService>();
            services.AddHostedService<SendMailBookingBeginBackgroundService>();

            services.AddControllersWithViews();

            // services
            services.AddScoped<SendMailService, SendMailService>();
            services.AddScoped<ReCaptchaService, ReCaptchaService>();
            services.AddScoped<HostingService, HostingService>();
            services.AddScoped<UploadService, UploadService>();
            services.AddScoped<BookingService, BookingService>();
            services.AddScoped<GoogleMapService, GoogleMapService>();
            services.AddScoped<BedRoomService, BedRoomService>();
            services.AddScoped<CommentService, CommentService>();
            services.AddScoped<ControllerExtenstionServiceRazor, ControllerExtenstionServiceRazor>();
            // repository 
            services.AddScoped<EquipmentRepository, EquipmentRepository>();
            services.AddScoped<EquipmentTypeRepository, EquipmentTypeRepository>();
            services.AddScoped<BedRepository, BedRepository>();
            services.AddScoped<HostingTypeRepository, HostingTypeRepository>();
            services.AddScoped<HostingRepository, HostingRepository>();
            services.AddScoped<ImageRepository, ImageRepository>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<BookingRepository, BookingRepository>();
            services.AddScoped<UnavailableDateRepository, UnavailableDateRepository>();
            services.AddScoped<BedRoomRepository, BedRoomRepository>();
            services.AddScoped<CommentRepository, CommentRepository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseStatusCodePagesWithReExecute("/error/{0}");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
