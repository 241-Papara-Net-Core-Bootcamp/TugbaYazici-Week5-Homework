using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Concretes;
using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PaparaThirdWeek.Services.MappingProfiles;
using System.Reflection;
using PaparaThirdWeek.Api.Filters;
using PaparaThirdWeek.Api.Extensions;
using PaparaThirdWeek.Api.Middlewares;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Hangfire;

namespace PaparaThirdWeek.Api
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
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o=> 
            //{
            //    var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            //    o.SaveToken = true;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["JWT:Issuer"],
            //        ValidAudience = Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(key)
            //    };
            //});
            //services.AddControllers(options =>
            //    options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddControllers();
            //services.AddControllersWithViews(options => options.CacheProfiles.Add("Duration45", new CacheProfile
            //{
            //    Duration = 45,
            //    Location = ResponseCacheLocation.Client,
            //}));
            //services.AddResponseCaching();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaparaThirdWeek.Api", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Please insert JWT with Bearer into field",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey,
            //        BearerFormat = "JWT",
            //        Scheme = "Bearer"
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
            //    {
            //          {
            //                new OpenApiSecurityScheme
            //                {
            //                      Reference = new OpenApiReference
            //                      {
            //                          Type = ReferenceType.SecurityScheme,
            //                          Id = "Bearer"
            //                      }
            //                },
            //                new string[] { }
            //          }
            //    });

            //});

            services.AddDbContext<PaparaAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICompanyService, CompanyServices>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //profile den base alan tüm dosyalarý tarýyor.
            services.AddTransient<IFakeUserService,FakeUserServices>();
            //attribute olarak eklediðim actionda çalýþýr.
            services.AddScoped<ValidationFilterAttribute>();
            services.AddTransient<ICacheService, CacheService>();
            //Tüm actionlar için register yöntemi=>global
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(ValidationFilterAttribute));
            //});
            services.AddMemoryCache();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaparaThirdWeek.Api v1"));
            }

            app.UseExceptionMiddleware();
            
            //app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();//Jwt için gerekli olan middleware
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseResponseCaching();
            app.UseHangfireDashboard("/jobs");
        }
    }
}
