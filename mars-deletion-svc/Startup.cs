using System.Net.Http;
using Hangfire;
using Hangfire.Mongo;
using mars_deletion_svc.BackgroundJobs;
using mars_deletion_svc.BackgroundJobs.Interfaces;
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
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IHostedService, HostedStartupService>();

            // Clients
            services.AddScoped<HttpClient>();
            services.AddScoped<IMarkingServiceClient, MarkingServiceClient>();
            services.AddScoped<IFileClient, FileClient>();
            services.AddScoped<IScenarioClient, ScenarioClient>();
            services.AddScoped<IResultConfigClient, ResultConfigClient>();
            services.AddScoped<ISimPlanClient, SimPlanClient>();
            services.AddScoped<ISimRunClient, SimRunClient>();
            services.AddScoped<IResultDataClient, ResultDataClient>();

            // Handlers
            services.AddScoped<IDeleteControllerHandler, DeleteControllerHandler>();
            services.AddScoped<IMarkSessionHandler, MarkSessionHandler>();
            services.AddScoped<IDependantResourceHandler, DependantResourceHandler>();
            services.AddScoped<IBackgroundJobsHandler, BackgroundJobsHandler>();
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