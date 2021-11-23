using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.OpenApi.Models;
using NLog;
using PolyDB.DAL;
using PolyDB.DAL.Entities;
using PolyDB.DAL.Repositories;
using PolyDB.DAL.Repositories.Interfaces;
using RendezVousPolyclinique.Infra.Formatters;
using RendezVousPolyclinique.Infra.Logging;
using RendezVousPolyclinique.Infra.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IDBConnect, DBConnect>(m=> new DBConnect(Configuration.GetConnectionString("DevPolyDbConnectionString")));
            services.AddScoped<IRepository<PatientEntity, int>, PatientRepository>();


            services.AddControllers(
                options=> {
                    // J'accepte qu'on me demande un type
                    options.RespectBrowserAcceptHeader = true;
                    //Je n'accepte que des types connus
                    // renvoie un 206 si type non "support�"
                    //options.ReturnHttpNotAcceptable = true;
                    // ajoute mon formater
                    options.OutputFormatters.Add(new HL7Formatter());
                }
                
                );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RendezVousPolyclinique", Version = "v1" });
            });

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication("Bearer", options =>
                    {
                        options.ApiName = "RendezvousPolyCliniqueApi"; //Nom de l'api configur�e dans Identity
                        options.Authority = "https://localhost:44336"; //Adresse identity server
                    });

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

            app.UseHttpsRedirection();

            app.UseRouting();

           // app.UseAuthentication();

           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
