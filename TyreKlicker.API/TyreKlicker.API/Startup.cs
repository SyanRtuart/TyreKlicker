﻿using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using TyreKlicker.Application.Infrastructure;
using TyreKlicker.Application.Infrastructure.AutoMapper;
using TyreKlicker.Application.Interfaces;
using TyreKlicker.Application.Order.Command.CreateOrder;
using TyreKlicker.Application.Order.Queries.GetAllOrders;
using TyreKlicker.API.Middlewear;
using TyreKlicker.API.Services;
using TyreKlicker.API.Services.Token;
using TyreKlicker.API.Services.UserToken;
using TyreKlicker.Infrastructure;
using TyreKlicker.Infrastructure.Identity.Data;
using TyreKlicker.Infrastructure.Identity.Factories;
using TyreKlicker.Infrastructure.Identity.Models;
using TyreKlicker.Infrastructure.Identity.Service.RefreshToken;
using TyreKlicker.Infrastructure.Models;
using TyreKlicker.Infrastructure.Services;
using TyreKlicker.Persistence;

namespace TyreKlicker.API
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
            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetAllOrdersQuery).GetTypeInfo().Assembly);

            //// Add DbContext using SQL Server Provider
            services.AddDbContext<TyreKlickerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("TyreKlicker.Persistence")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            //services.AddDbContext<TyreKlickerDbContext>(options =>
            //    options.UseSqlServer("Server=tyreklicker.cnyuhkntm7cx.eu-west-2.rds.amazonaws.com;Database=TyreKlicker;User Id=TyreKlickerAdmin;Password=Tyre_Klicker1;", b => b.MigrationsAssembly("TyreKlicker.Persistence")));

            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer("Server=tyreklicker.cnyuhkntm7cx.eu-west-2.rds.amazonaws.com;Database=TyreKlicker.Identity;User Id=TyreKlickerAdmin;Password=Tyre_Klicker1;"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = "TyreKlicker.XF",
                        ValidIssuer = "TyreKlicker.API",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"))
                    };
                });

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IUserTokenService, UserTokenService>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IPaymentService, StripeService>();

            // Add Stripe
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderCommandValidator>());

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });

            services.ConfigureSwaggerGen(options => { options.CustomSchemaIds(x => x.FullName); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseHttpStatusCodeExceptionMiddleware();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1"); });
            }
            else
            {
                app.UseHttpStatusCodeExceptionMiddleware();

                app.UseExceptionHandler("/Home/Error");
            }

            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}