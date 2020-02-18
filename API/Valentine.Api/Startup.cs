using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Valentine.Api.Extensions;
using Valentine.Api.Interfaces;
using Valentine.Api.Services;
using Valentine.Application.Interfaces;
using Valentine.Application.Repositories;
using Valentine.Persistence;

namespace Valentine.Api
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
            services.RegisterAzureBlobStorage(Configuration);

            services.AddScoped<IValentineDbContext, ValentineDbContext>();
            services.AddScoped<IFileProcessor, FileProcessor>();
            services.AddScoped<IFileCloudUploader, FileCloudUploader>();
            services.AddScoped<IMapsRepository, MapsRepository>();
            services.AddScoped<IAreasRepository, AreasRepository>();
            services.AddScoped<IGeoPointsRepository, GeoPointsRepository>();
            services.AddScoped<IFilesRepository, FilesRepository>();
            
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(Configuration.GetValue<string>("CorsPolicy"),
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(Configuration.GetValue<string>("CorsPolicy"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
