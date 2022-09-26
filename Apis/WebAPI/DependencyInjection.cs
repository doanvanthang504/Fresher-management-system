using Application.Interfaces;
using FluentValidation.AspNetCore;
using Global.Shared.Commons;
using Global.Shared.ExportExcelExtensions;
using Global.Shared.Helpers;
using Global.Shared.JsonConverters;
using Global.Shared.Settings;
using Global.Shared.Settings.Reminder;
using Hangfire;
using Hangfire.MemoryStorage;
using Infrastructures.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WebAPI.Conventions;
using WebAPI.Middlewares;
using WebAPI.ModelBinders.Providers;
using WebAPI.Services;

namespace WebAPI
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(new ControllerNameAttributeConvention());
                    options.ModelBinderProviders.Insert(0, new DateOnlyMoldelBinderProvider());
                })
                .ConfigureApiBehaviorOptions(option =>
                {
                    option.SuppressModelStateInvalidFilter = true;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
                c.MapType<DateOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date-time",
                    Pattern = DateTimeFormat.ToShortDate(Constant.DATE_TIME_FORMAT_MMddyyyy)
                });
            });
            services.AddHealthChecks();
            services.AddScoped<ExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddScoped<IUserMailCredentialService, UserMailCredentialService>();
            services.AddScoped<IImportDataFromExcelFileService, ImportDataFromExcelFileService>();
            services.AddScoped<IExcelExportHistoryService, ExcelExportHistoryService>();
            services.AddScoped<IExcelExportDeliveryService, ExcelExportDeliveryService>();
            services.AddScoped<IExcelExportChartService, ExcelExportChartService>();
            services.AddScoped<IExcelExportScroreService, ExcelExportScroreService>();
            services.AddScoped<ILogger, Logger<ExceptionMiddleware>>();
            services.AddScoped<SaveWorkBook>();
            services.AddScoped<AuthorizedFilter>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddHttpContextAccessor();
            services.AddFluentValidation(p =>
            {
                p.RegisterValidatorsFromAssemblyContaining<Program>(lifetime: ServiceLifetime.Singleton);
                p.DisableDataAnnotationsValidation = false;
            });

            services.AddHttpClient();
            services.AddHangfireServer();
            services.AddHangfire(x =>
            {
                x.UseMemoryStorage();
            });
            return services;
        }

        public static IServiceCollection AddMeetingRequestServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var publicClientApplication = PublicClientApplicationBuilder
                                                .Create(configuration["AzureAd:ClientId"])
                                                .WithTenantId(configuration["AzureAd:TenantId"])
                                                .WithDefaultRedirectUri()
                                                .Build();
            return services.AddSingleton(publicClientApplication);
        }

        public static IServiceCollection AddRootSetting(
            this IServiceCollection services, IConfiguration configuration)
        {
            var rootSetting = new RootSetting();
            configuration.GetSection(nameof(RootSetting)).Bind(rootSetting);
            rootSetting.Validate();

            services.AddSingleton(rootSetting);

            return services;
        }

        public static IServiceCollection AddReminderSetting(
            this IServiceCollection services, IConfiguration configuration)
        {
            var reminderSetting = new ReminderSettings();
            configuration.GetSection(nameof(ReminderSettings)).Bind(reminderSetting);
            services.AddSingleton(reminderSetting);
            return services;
        }

        private static void Validate(this RootSetting rootSetting)
        {
            _ = rootSetting.SmtpClientSetting ?? throw new ArgumentNullException(nameof(rootSetting.SmtpClientSetting));
        }
    }
}
