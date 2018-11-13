using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache.Infrastructure;
using Cache.Memory;
using Cache.Redis;
using Cache.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Cache.Dome
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCacheOptions"));

            services.AddSingleton<ISerializationService, SerializationService>();
            services.Configure<ConfigurationOptions>(Configuration.GetSection("ConfigurationOptions"));
            services.Configure<RedisKeyOptions>(Configuration.GetSection("RedisKeyOptions"));

            services.AddSingleton<ICacheManager, RedisManager>();
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
