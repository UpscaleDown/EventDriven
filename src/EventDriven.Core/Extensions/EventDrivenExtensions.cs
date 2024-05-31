using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Events;

namespace UpscaleDown.EventDriven.Core.Extensions;

public static class EventDrivenExtensions
{
    public static void AddServices(this EventDriven ev, EventDrivenOptions opts)
    {
        var builder = ev.Builder;
        #region Builder
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.PropertyNameCaseInsensitive = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSwaggerGen(o => AddSwaggerDocumentation(o));

        // builder.Services.AddAuthentication(options =>
        // {
        //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(o =>
        // {
        //     o.MapInboundClaims = false;
        //     o.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //         ValidAudience = builder.Configuration["Jwt:Audience"],
        //         IssuerSigningKey = new SymmetricSecurityKey
        //        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        //         ValidateIssuer = true,
        //         ValidateAudience = false,
        //         ValidateLifetime = true,
        //         ValidateIssuerSigningKey = true
        //     };

        // });

        // // force authenticated users by default
        // builder.Services.AddAuthorization(options =>
        // {
        //     options.FallbackPolicy = new AuthorizationPolicyBuilder()
        //                 .RequireAuthenticatedUser()
        //                 .Build();
        //     // Register other policies here
        // });

        // allow cors during dev
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        if (builder.Environment.IsDevelopment())
        {

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
            });
        }
        #endregion

    }

    static void AddSwaggerDocumentation(SwaggerGenOptions o)
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
    public static void Run(this EventDriven ev)
    {
        var app = ev.Build();

        // DataManager.Init(builder.Configuration);
        var basePath = "/api/gatekeeper";
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // app.UseCors(MyAllowSpecificOrigins);
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "swagger/{documentName}/swagger.json";
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}" } };
                });
                });
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UsePathBase(basePath);

        var ct = new CancellationTokenSource();
        var consumersTask = app.StartEventConsumers(ct.Token);
        app.Run();
        ct.Cancel();

    }

    private static Task StartEventConsumers(this WebApplication app, CancellationToken token)
    {
        // retrieve event consumers singleton
        var consumers = app.Services.GetServices<IBaseEventConsumer>();
        var tasks = new List<Task>();

        foreach (var consumer in consumers)
        {
            tasks.Add(consumer.ListenAsync(token));
        }
        Task.WaitAll(tasks.ToArray(), token);
        return Task.CompletedTask;
    }
}