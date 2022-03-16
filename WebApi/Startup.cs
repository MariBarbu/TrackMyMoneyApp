using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using File = System.IO.File;
using Microsoft.OpenApi.Models;
using WebApi.Helpers;
using DataLayer;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DataLayer.Entities;
using AutoMapper;
using Services.AutoMapperProfiles;
using Jobs;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            AddAutoMapper(services);
            AddContextAndIdentity(services);
            AddDependencies(services);
            services.AddHttpClient();
            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]))
                };
            });

            services.AddAuthorization();

            var assemblyDate = GetAssemblyDate();
            var buildNo = assemblyDate.DayOfYear + "." + (assemblyDate.Hour * 60 + assemblyDate.Minute);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackMyMoney", Version = $"1.0 build {buildNo} on date " + assemblyDate.ToString("dd/MM hh:mm") });
                options.OperationFilter<SwaggerOperationFilter>();
                options.DocumentFilter<SwaggerDocumentFilter>();
            });

    //        services.AddSwaggerGen(c => {
    //            c.SwaggerDoc("v1", new OpenApiInfo
    //            {
    //                Title = "TrackMyMoney",
    //                Version = "v1"
    //            });
    //            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    //            {
    //                Name = "Authorization",
    //                Type = SecuritySchemeType.ApiKey,
    //                Scheme = "Bearer",
    //                BearerFormat = "JWT",
    //                In = ParameterLocation.Header,
    //                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    //            });
    //            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    //    {
    //        new OpenApiSecurityScheme {
    //            Reference = new OpenApiReference {
    //                Type = ReferenceType.SecurityScheme,
    //                    Id = "Bearer"
    //            }
    //        },
    //        new string[] {}
    //    }
    //});
    //        });

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Items["Exception"] = contextFeature.Error.Message;
                        context.Items["StackTrace"] = contextFeature.Error.StackTrace;
                        switch (contextFeature.Error)
                        {
                            case BadRequestException _:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case UnauthorizedException _:
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                break;
                            default:
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }

                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new List<string>
                            {contextFeature.Error.Message}));
                    }
                });
            });
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "TracKMyMoney");
                c.RoutePrefix = "api/swagger";
            });

            app.UseCors("AllowAll");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityProfiles());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        private void AddDependencies(IServiceCollection services)
        {
            services.AddJobs(); 
            services.AddServices();
            services.AddRepositories();
            services.AddHostedService<JobsRunnerService>();
        }
        private void AddContextAndIdentity(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Poems")));
            services
                .AddIdentity<AppUser, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequiredLength = 8;
            }
            );
        }

        private static DateTime GetAssemblyDate()
        {
            string fileName = Assembly.GetExecutingAssembly().Location;
            return File.GetLastWriteTimeUtc(fileName);
        }
    }
}