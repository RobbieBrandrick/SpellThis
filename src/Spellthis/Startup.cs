using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spellthis.Repositories;
using Microsoft.Extensions.Configuration;
using Spellthis.Models;

namespace Spellthis
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddScoped<IWordsRepository, WordsRepository>();
            services.AddOptions();
            services.Configure<TTSConfigurations>(Configuration.GetSection("TextToSpeech"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            SetUpConfigurations(env.ContentRootPath);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=SpellThis}/{action=Index}/{id?}"
                );
            });

        }

        private void SetUpConfigurations(string basePath)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = configurationBuilder.Build();


        }
    }
}
