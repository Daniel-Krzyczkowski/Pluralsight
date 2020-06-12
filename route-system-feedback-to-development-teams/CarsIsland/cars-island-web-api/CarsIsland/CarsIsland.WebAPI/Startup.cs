using AutoMapper;
using CarsIsland.WebAPI.Core.MappingProfiles;
using CarsIsland.WebAPI.Core.Services;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using CarsIsland.WebAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarsIsland.WebAPI
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
            services.AddDbContext<CarsIslandDbContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("CarsIslandDbContext")));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CarProfile());
                mc.AddProfile(new ContactProfile());
                mc.AddProfile(new LocationsProfile());
            });
            mappingConfig.AssertConfigurationIsValid();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ICarsDataService, CarsDataService>();
            services.AddScoped<IContactsDataService, ContactsDataService>();
            services.AddScoped<ILocationsDataService, LocationsDataService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
