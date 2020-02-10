using AutoMapper;
using Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Helpers;
using WebApi.Identity;
using WebApi.Mapping;
using WebApi.Services;
using WebApi.Settings;

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
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Identity
            services.AddDbContext<SecurityContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Identity kurulumu için
            // Tools->NuGet Package Manager -> Package Manager Console
            // add-migration init -Context SecurityContext
            // update-database -Context SecurityContext
            IdentityHelper.ConfigureService(services);

            // Helpers
            AuthenticationHelper.ConfigureService(services, Configuration["JwtSecurityToken:Issuer"], Configuration["JwtSecurityToken:Audience"], Configuration["JwtSecurityToken:Key"]);
            CorsHelper.ConfigureService(services);
           
            // Settings
            services.Configure<JwtSecurityTokenSettings>(Configuration.GetSection("JwtSecurityToken"));

            // Services
            services.AddTransient<IEmailService, EmailService>();

            //Data
            services.AddDbContext<SchoolDbContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School API", Version = "v1" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "School API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
