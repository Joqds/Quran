using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using FluentValidation.AspNetCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using Quran.Server.Api.Filters;
using Quran.Server.Api.Services;
using Quran.Server.Application;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Infrastructure;
using Quran.Server.Infrastructure.Persistence;

namespace Quran.Server.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string AllowedOrigin = "allowedOrigin";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

//            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();


            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddCors(option =>
            {
                option.AddPolicy(AllowedOrigin,
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
            services.AddOpenApiDocument(settings =>
            {
                settings.Title = "Joqds Quran Api";
                settings.AddSecurity("JWT", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.OAuth2,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Type into the textbox: Bearer {your JWT token}."
                    });

                settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(AllowedOrigin);
            app.UseHealthChecks("/health");
            app.UseOpenApi();

//            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
