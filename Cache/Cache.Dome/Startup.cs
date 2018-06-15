using Cache.Memory;
using Cache.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            //手动配置
            services.Configure<RedisConfigOptions>(r =>
            {
                r.AutoStart = true;
                r.MaxReadPoolSize = 10;
            });
            //读取配置文件配置
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("Cache:MemoryCacheOptions"));
            services.Configure<RedisConfigOptions>(Configuration.GetSection("Cache:RedisConfigOption"));

            //services.ConfigureOptions()
            //services.AddSingleton<MemoryCacheManager>(x =>
            //{
            //    var memoryCacheOptions = x.GetService<MemoryCacheOptions>();
            //    return new MemoryCacheManager(memoryCacheOptions);
            //});

            var a = services.AddOptions<MemoryCacheOptions>().Name;
            //services.AddSingleton<RedisManager>(x =>
            //{
            //    services.Con
            //    return new RedisManager();
            //});
        }

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
