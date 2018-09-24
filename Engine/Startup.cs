using Engine.Services.Implementations;
using Engine.Services.Intefaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engine
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services
            //services.AddHttpClient<PollyHttpClient>()
            //        .AddTransientHttpErrorPolicy(
            //            x => x.WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            services.AddScoped<IRTCService, RTCService>();
            ConfigureConfigurationProvider();
            services.AddMvc();
        }


        private void ConfigureConfigurationProvider()
        {
            //string environment = Configuration.GetValue<string>("Environment");
            //string configurationStoreUrl = Configuration.GetValue<string>("ConfigurationStoreUrl");
            //BPC.CoreInteract.Core.ConfigurationProvider.ConfigurationProvider.Configure(environment, configurationStoreUrl);
        }
    }
}
