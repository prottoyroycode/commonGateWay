using Bkash_Service_API.Core;
using Bkash_Service_API.Core.User;
using Bkash_Service_API.Data;
using Bkash_Service_API.Extentions;
using Bkash_Service_API.Helpers;
using Bkash_Service_API.Infrastructure;
using Bkash_Service_API.Infrastructure.UserAccount;
using Bkash_Service_API.Services.BkashRecurringPaymentService;
using Bkash_Service_API.Services.BkashService;
using Bkash_Service_API.Services.BLChargeGakkService;
using Bkash_Service_API.Services.BLTouchAndPlayService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API
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
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHttpClient();

            #region di container
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IBkashService, BkashService>();
            services.AddTransient<IBkashRecurringService, BkashRecurringService>();
            services.AddTransient<IBLTouchAndPlayService, BLTouchAndPlayService>();
            services.AddTransient<IBLChargeGakkServiceProvider, BLChargeGakkServiceProvider>();
            //  services.AddApiVersioning();
            // services.AddApplicationServices();


            #endregion di container
            #region jwt
            services.AddJwtServices(Configuration);
            #endregion jwt
            #region basic auth
            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")

                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
                
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bkash_Service_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bkash_Service_API v1"));
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                await context.Response.WriteAsJsonAsync(new { error = exception.Message });
            }));
           
            //  app.UseMvc(); // or .UseRouting() or .UseEndpoints()
            app.UseRouting();
            //for jwt middleware
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
