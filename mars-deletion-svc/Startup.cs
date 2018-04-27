using System.Net.Http;
using Hangfire;
using Hangfire.Mongo;
using mars_deletion_svc.Controllers;
using mars_deletion_svc.Controllers.Interfaces;
using mars_deletion_svc.DependantResource;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.MarkingService;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession;
using mars_deletion_svc.MarkSession.Interfaces;
using mars_deletion_svc.Middlewares;
using mars_deletion_svc.ResourceTypes.Metadata;
using mars_deletion_svc.ResourceTypes.Metadata.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultConfig;
using mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultData;
using mars_deletion_svc.ResourceTypes.ResultData.Interfaces;
using mars_deletion_svc.ResourceTypes.Scenario;
using mars_deletion_svc.ResourceTypes.Scenario.Interfaces;
using mars_deletion_svc.ResourceTypes.SimPlan;
using mars_deletion_svc.ResourceTypes.SimPlan.Interfaces;
using mars_deletion_svc.ResourceTypes.SimRun;
using mars_deletion_svc.ResourceTypes.SimRun.Interfaces;
using mars_deletion_svc.Services;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            // Hangfire config
            services.AddHangfire(configuration =>
            {
                configuration.UseMongoStorage(
                    Constants.Constants.MongoDbConnection,
                    Constants.Constants.MongoDbHangfireName
                );
            });

            // Services
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddSingleton<IHostedService, HostedStartupService>();

            // Clients
            services.AddSingleton<HttpClient>();
            services.AddTransient<IMarkingServiceClient, MarkingServiceClient>();
            services.AddTransient<IMetadataClient, MetadataClient>();
            services.AddTransient<IScenarioClient, ScenarioClient>();
            services.AddTransient<IResultConfigClient, ResultConfigClient>();
            services.AddTransient<ISimPlanClient, SimPlanClient>();
            services.AddTransient<ISimRunClient, SimRunClient>();
            services.AddTransient<IResultDataClient, ResultDataClient>();

            // Handlers
            services.AddTransient<IDeleteControllerHandler, DeleteControllerHandler>();
            services.AddTransient<IMarkSessionHandler, MarkSessionHandler>();
            services.AddTransient<IDependantResourceHandler, DependantResourceHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Must always be on top !!!
            app.UseMiddleware<ErrorHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Hangfire config
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}