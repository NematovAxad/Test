using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Variance;
using CoreResult;
using EntityRepository;
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
        public static string ConnectionString => "Host=172.16.60.6;Port=5432;Database=nis_system;Username=postgres;Password=123456789$p";

        #region
        public static void RegisterDb(IServiceCollection services, ContainerBuilder builder)
        {
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

            //DataInfrostructure.Start.Builder(builder);
            //AdminHandler.Start.Builder(builder);
            //ReferenceHandler.Start.Builder(builder);
            //OrganizationHandler.Start.Builder(builder);
            //DataInfrostructure.Start.Builder(builder);
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
            
            services.AddApiSwagger(Assembly.GetExecutingAssembly().GetName().Name);
            AddJwt(services);
            // CoreResult.Start.ConfigureService(services);
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

        public static IServiceCollection AddApiSwagger(this IServiceCollection services, string assemblyName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "E-Gov WMS",
                    Description = "API for WMS"
                });

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description =
                        "Заголовок авторизации JWT с использованием схемы Bearer. Пример: \"Bearer { token }\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] { }
                    }
                });

                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        #endregion
        #region

        public static void Configure(this IApplicationBuilder app, string serviceName)
        {
            app.ConfigureApp();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{serviceName.ToUpper()} SERVICE API V1"); });
        }
        #endregion
    }
}
