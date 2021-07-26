using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TransityEasy.Api.Core.Handlers;
using TransityEasy.Api.Core.Models.Request;
using TransityEasy.Api.Core.Models.Result;
using TransityEasy.Api.Core.Options;
using TransityEasy.Api.Core.Services;

namespace TransityEasy.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TransitEasy API", Version = "v1" }));
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            //options 
            services.Configure<TranslinkOptions>(Configuration.GetSection("TranslinkApi"));

            //http services 
            services.AddHttpClient<ITranslinkApiService, TranslinkApiService>((svp, client) => {
                var options = svp.GetRequiredService<IOptionsMonitor<TranslinkOptions>>();
                client.BaseAddress = new Uri(options.CurrentValue.BaseApiUrl);
                client.Timeout = TimeSpan.FromSeconds(options.CurrentValue.TimeoutInSec);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            });

            //handlers
            services.AddTransient<IRequestHandler<NearbyStopsInfoRequest, NearbyStopsInfoResult>, NearbyStopsRequestHandler>();
            services.AddTransient<IRequestHandler<NextBusesScheduleRequest, NextBusStopInfoResult>, NextBusesScheduleRequestHandler>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.RoutePrefix = string.Empty;
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", env.ApplicationName);
               });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
