using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Core.Services;
using Core.Entities;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System;
using LinkitAir.Helpers;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkitAir
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
            /**
             * @TODO: Think about adding a third-party IoC Container for registering dependencies
             * in order to utilize the support for registry classes to avoid referencing the Infrastructure 
             * project from the UI layer(this feature is not offered by the default IoC container provided by ASP.NET Core
             * */

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IRequestLogRepository, RequestLogRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IRequestLogService, RequestLogService>();

            services.AddCors();

            // services.AddScoped<RequestActionFilter>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddEntityFrameworkSqlServer();

            /**
             * @TODO: This also creates a dependency, makes the UI layer dependent on Infrastructure.
             * Think about a way to handle this and possibly remove this dependency
             * */
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
               )
           );

            services.AddIdentity<ApplicationUser, IdentityRole>(
            opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredLength = 7;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    // standard configuration
                    ValidIssuer = Configuration["Auth:Jwt:Issuer"],
                    ValidAudience = Configuration["Auth:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero,
                    // security switches
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
            });


            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "LinkitAir API",
                    Description = "Web API for LinkitAir, airline services",
                    Contact = new Contact
                    {
                        Name = "Robert Gliguroski",
                        Email = "robert.gliguroski@gmail.com",
                        Url = "https://twitter.com/gliguroskir"
                    },
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors(builder =>
     builder.WithOrigins("http://localhost"));

            //   app.UseRequestResponseLogging

            app.Use(async (context, next) =>
            {
                var sw = new Stopwatch();
                sw.Start();
                await next.Invoke();
                sw.Stop();
                IRequestLogService service = (IRequestLogService)context.RequestServices.GetService(typeof(IRequestLogService));
                var helper = new HttpRequestResponseHelper(service);
                await helper.saveRequestResponseDetails(context, sw);
            }); 
            
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkitAir API V1");
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });


            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext =  serviceScope.ServiceProvider.GetService<AppDbContext>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                
               //  dbContext.Database.Migrate();
               //  DbSeeder.Seed(dbContext, roleManager, userManager);
            }

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });            
        }
    }
}
