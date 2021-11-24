using ApiMiddleware;
using ApiTools.Logging;
using ApiTools.Logging.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using RendezVousPolyclinique.Infra.Formatters;
using System.Collections.Generic;
using System.IO;

namespace RendezVousPolyclinique
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}/Nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string _policyName="MyPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCustomCorsPolicy(_policyName, new List<string>() { "https://www.isocl.be" });
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddToDI(Configuration);
            services.AddCustomSwagger("RendezVousPolyclinique", "v1");

            services.AddControllers(
                options =>
                {
                    // J'accepte qu'on me demande un type
                    options.RespectBrowserAcceptHeader = true;
                    //Je n'accepte que des types connus
                    // renvoie un 206 si type non "supporté"
                    //options.ReturnHttpNotAcceptable = true;
                    // ajoute mon formater
                    options.OutputFormatters.Add(new HL7Formatter());
                }

                );
            #region Security
            services.AddIdenityServerSecurityBearer("RendezvousPolyCliniqueApi", "https://localhost:44336");
            
            #endregion




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RendezVousPolyclinique v1"));
            }

            app.AddGlobalErrorHandlerWithLog(new LoggerManager());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(_policyName);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
