using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DoorSensor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<Door>()
                .AddSignalR()
                .AddMessagePackProtocol();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<DoorHub>("/door");
            });
        }
    }
}
