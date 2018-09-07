using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodeChallenge.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CodeChallenge.Data.Repositories;
using CodeChallenge.Core.Interfaces.Repositories;
using CodeChallenge.Core.Automapper;
using CodeChallenge.Core.Interfaces.Services;
using CodeChallenge.Services.Services;
using CodeChallenge.Services.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace CodeChallenge
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
            services.AddDbContext<CodeChallengeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            

            // Repositories:
            services.AddScoped<IMunicipalityTaxesRepository, MunicipalityTaxesRepository>();

            // Services:
            services.AddTransient<IMunicipalityTaxesService, MunicipalityTaxesService>();      

            // Swagger:
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CodingTask API", Version = "v1" });
            });            

            services.AddMvc();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CodeChallengeContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=MunicipalityTaxes}/{action=Index}/{id?}");

            });

            app.UseSwagger();

            // Specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //DbInitializer: (Seed method)
            DbInitializer.Initialize(context);
            // Automapper register profiles
            AutoMapperConfig.SetUpConfiguration();
        }
    }
}
