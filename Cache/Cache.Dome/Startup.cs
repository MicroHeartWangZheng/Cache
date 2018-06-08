using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cache.Dome
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
            this.HostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCacheOptions")).AddSingleton<MemoryCacheOptions>();

            services.AddSingleton<MemoryCache>(x =>
            {
                var memoryCacheOption = x.GetService<MemoryCacheOptions>();
                return new MemoryCache(memoryCacheOption);
            });

            services.Configure<TestOptions>(Configuration.GetSection("TestOptions")).AddSingleton<TestOptions>();
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
