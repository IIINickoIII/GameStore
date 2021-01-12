using AutoMapper;
using GameStore.Bll.Interfaces;
using GameStore.Bll.Mapper;
using GameStore.Bll.Payment;
using GameStore.Bll.Payment.Interfaces;
using GameStore.Bll.Services;
using GameStore.Dal.Contexts;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using GameStore.Dal.Repositories;
using GameStore.Web.Extensions;
using GameStore.Web.Mapper;
using GameStore.Web.Services;
using GameStore.Web.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GameStore.Web
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MapperProfileBll>();
                mc.AddProfile<MapperProfileWeb>();
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient();

            ConfigureGameStoreServices(services);
            services.AddControllers();
            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1",
                    Title = "API",
                    Description = "GameStore"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerService logger, GameStoreContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            dataContext.Database.Migrate();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US")
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Games}/{action=GetReadOnlyGames}/");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api documentation"); });
        }

        private void ConfigureGameStoreServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            // Dal
            ConfigureDatabases(services);
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrder, Order>();

            // Bll
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPlatformTypeService, PlatformTypesService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentContext, PaymentContext>();
            services.AddScoped<IUserService, UserService>();

            // Web
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IPdfCreator, PdfCreator>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<GameStoreContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("GameStoreConnection"));
            });
        }
    }
}