using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.Data;
using WebApi.DataLayer;

namespace WebApi
{
    public class Startup

    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // For Entity Framework 
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthDb")));
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            })

            .AddGoogle(opts =>
            {
                opts.ClientId = "818495489978-3p0bs1ocr5k76vrr98orb1ojcd7ks0vt.apps.googleusercontent.com";
                opts.ClientSecret = "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";
                opts.SignInScheme = IdentityConstants.ExternalScheme;
            });
            ;
           

            services.AddControllers();
            services.AddScoped<IUserData,SqlUserData>();
            services.AddScoped<IClusterData, SqlClusterData>();
            services.AddScoped<IUrlData, SqlUrlData>();
            services.AddScoped<IStrategyData, SqlStrategyData>();
            services.AddScoped<IUrlStrategyData, SqlUrlStrategyData>();
            services.AddScoped<IExtendedClusterData, SqlExtendedClusterData>();
            //services.AD
            services.AddScoped<IClusterInfo, ClusterDataSql>();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
         
           


            app.UseHttpsRedirection();

     



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
