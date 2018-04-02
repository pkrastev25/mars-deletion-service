using System.Net.Http;
using mars_deletion_svc.DependantResource;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.MarkingService;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.Services;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mars_deletion_svc
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
            services.AddMvc();

            // Services
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IErrorService, ErrorService>();

            // Clients
            services.AddSingleton<HttpClient>();
            services.AddTransient<IMarkingServiceClient, MarkingServiceClient>();

            // Handlers
            services.AddTransient<IDependantResourcesHandler, DependantResourcesHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}