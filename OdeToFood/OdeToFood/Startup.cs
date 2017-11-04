using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            services.AddMvc();
            services.AddSingleton(Configurations);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodContext>(options => options.UseSqlServer(Configurations.GetConnectionString("OdeToFood")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<OdeToFoodContext>();
            services.AddAuthentication();
            services.AddAuthorization();
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
            }else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = (context) => context.Response.WriteAsync("Opa!")
                });
            }
                                  
            app.UseFileServer(); // app.UseDefaultFiles(); +   app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseIdentity();           
            
            app.UseMvc(ConfigureRoutes);
            
            //No caso do request nao achar nenhuma rota compativel
            app.Run((context) => context.Response.WriteAsync("Not Found"));

        }

        //Configuracao de rotas para o MVC
        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default",
                                  "{controller=Home}/{action=index}/{id?}");
        }
    }
}
