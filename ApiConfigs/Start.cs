using ApiConfigs.EnumSynonyms.Extensions;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Variance;

using CoreDomain.Middlewares;

using CoreResult;

using Domain;

using EntityRepository;

using Jh.Core.Extensions;

using JohaRepository;

using MainInfrastructures.Db;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ApiConfigs
{
    public static class Start
    {
        public static string ConnectionString => "postrgressConnection".Env() ?? "Host=172.16.30.250;Port=45432;Database=nisdata;Username=nisuse;Password=zV6r3dy7CtmbJeEP";
        //Host=postgres;Port=5432;Database=nisdata;Username=nisuse;Password=zV6r3dy7CtmbJeEP
        #region
        public static void RegisterDb(IServiceCollection services, ContainerBuilder builder)
        {
            Console.WriteLine("postrgressConnection".Env());
            services.AddDbContext<DataContext>(option =>
            {
                option.UseNpgsql(ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Scoped);
            services.AddScoped<IDataContext, DataContext>();

        }
        public static void AddJwt(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     // ��������, ����� �� �������������� �������� ��� ��������� ������
                     ValidateIssuer = true,
                     // ������, �������������� ��������
                     ValidIssuer = AuthOptions.ISSUER,

                     // ����� �� �������������� ����������� ������
                     ValidateAudience = true,
                     // ��������� ����������� ������
                     ValidAudience = AuthOptions.AUDIENCE,
                     // ����� �� �������������� ����� �������������
                     ValidateLifetime = true,

                     // ��������� ����� ������������
                     IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                     // ��������� ����� ������������
                     ValidateIssuerSigningKey = true
                 };
             });
        }
        public static void RegisterHandler(ContainerBuilder builder)
        {

            AdminHandler.Start.Builder(builder);
            UserHandler.Start.Builder(builder);
            MonitoringHandler.Start.Builder(builder);

        }
        public static void PartialRegister(IServiceCollection services, ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            MainInfrastructures.Start.Buidler(builder);
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }


        public static void ConfigureServices(this IServiceCollection services, ContainerBuilder builder)
        {

            JohaRepository.BaseConfigState.ProjectName = "nis_system";
            JohaRepository.BaseConfigState.LoginName = "nis_system";
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerSkipProperty>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
            AddJwt(services);
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddDistributedMemoryCache();
            RegisterDb(services, builder);
            PartialRegister(services, builder);
            RegisterHandler(builder);
            services.AddCors();
            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //  services.AddMvc();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.Populate(services);

        }

        #endregion
        #region

        public static void Configure(this IApplicationBuilder app, string serviceName)
        {
            app.ConfigureApp();
            app.UseMiddleware<LanguageMiddleware>();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

            app.InitializeSynonymStorage();
        }
        #endregion
    }
}
