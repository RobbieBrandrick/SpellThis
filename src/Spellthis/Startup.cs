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
using Spellthis.Services;
using Spellthis.Data;
using Microsoft.EntityFrameworkCore;

namespace Spellthis
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {

            SetUpConfigurations(env.ContentRootPath);

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddOptions();
            services.Configure<TTSConfigurations>(Configuration.GetSection("TextToSpeech"));
            services.AddSingleton<IWordsRepository, WordsRepository>();
            services.AddScoped<ITextToSpeechService, TextToSpeechService>();
            services.AddScoped<ISpellThisService, SpellThisService>();

            services.AddDbContext<SpellThisContext>(options => options.UseSqlite("Filename=./SpellThis.db"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SpellThisContext spellThisContext)
        {

            loggerFactory
                .AddConsole()
                .AddDebug();


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

            SpellThisDbInitializer.Initialize(spellThisContext);

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
