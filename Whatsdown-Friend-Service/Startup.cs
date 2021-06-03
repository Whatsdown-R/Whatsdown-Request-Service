using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Whatsdown_Friend_Service.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Whatsdown_Friend_Service
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
            services.AddControllers();

            services.AddDbContext<FriendContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("MvcMovieContext")));
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            var authenticationProviderKey = "TestKey";

            services.AddAuthentication()
                .AddOAuth(authenticationProviderKey, x =>
                {
                    x.ClientId = Configuration["Provider:ClientId"];
                    x.ClientSecret = Configuration["Provider:ClientSecret"];
                    x.CallbackPath = new PathString("/callback");

                    x.AuthorizationEndpoint = "https://api.provider.net/auth/code";
                    x.TokenEndpoint = "https://api.provider.net/auth/token";
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
