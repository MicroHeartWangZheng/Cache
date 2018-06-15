using Cache.Memory;
using Cache.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //读取配置
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("Cache:MemoryCacheOptions"));
            services.Configure<RedisConfigOptions>(Configuration.GetSection("Cache:RedisConfigOption"));

            //services.ConfigureOptions()
            //services.AddSingleton<MemoryCacheManager>(x =>
            //{
            //    var memoryCacheOptions = x.GetService<MemoryCacheOptions>();
            //    return new MemoryCacheManager(memoryCacheOptions);
            //});

            services.AddSingleton<RedisManager>(x =>
            {
                var redisConfigOption = x.;
                return new RedisManager(redisConfigOption);
            });
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
