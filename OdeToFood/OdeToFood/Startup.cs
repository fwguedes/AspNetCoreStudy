using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace OdeToFood
{
    public class Startup
    {

        //IHostingEnvironment -->Dados do ambiente de desensvolvimento

        public Startup(IHostingEnvironment env)
        {
            //Classe para ler arquivos de configuracao


            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)    //Microsoft.Extensions.Configuration.FileExtensions
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();

            Configurations = builder.Build();
        }

        public IConfiguration Configurations { get; set; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configurations);
            services.AddSingleton<IGreeter, Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(greeter.Greet());
            });
        }
    }
}
