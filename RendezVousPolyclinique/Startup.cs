using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using NLog;
using PolyDB.DAL;
using PolyDB.DAL.Entities;
using PolyDB.DAL.Repositories;
using PolyDB.DAL.Repositories.Interfaces;
using RendezVousPolyclinique.Infra.Formatters;
using RendezVousPolyclinique.Infra.Logging;
using RendezVousPolyclinique.Infra.Logging.Interfaces;
using RendezVousPolyclinique.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private string _policyName="MyPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCors(
                options => 
                {
                    options.AddPolicy(_policyName, poli =>
                                       poli.WithOrigins(new string[] { "https://www.isocl.be" })
                                       .WithMethods("DELETE")
                                       .AllowAnyHeader()
                                       );
                }
                );

            #region DI
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IDBConnect, DBConnect>(m => new DBConnect(Configuration.GetConnectionString("DevPolyDbConnectionString")));
            services.AddScoped<IRepository<PatientEntity, int>, PatientRepository>();


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
            #endregion
            #region Swagger
            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "RendezVousPolyclinique", Version = "v1" });
               });
            #endregion
            #region Security
            services.AddAuthentication("Bearer")
                       .AddIdentityServerAuthentication("Bearer", options =>
                       {
                           options.ApiName = "RendezvousPolyCliniqueApi"; //Nom de l'api configurée dans Identity
                        options.Authority = "https://localhost:44336"; //Adresse identity server
                    }); 
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

            app.UseExceptionHandler(
              appError =>
              {
                  LoggerManager manager = new LoggerManager();
                  appError.Run(async context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                      context.Response.ContentType = "application/json";
                      var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                      if (contextFeature != null)
                      {
                          manager.LogError($"Something went wrong: {contextFeature.Error}");
                          await context.Response.WriteAsync(new ErrorDetails() { StatusCode = context.Response.StatusCode, Message = "Internal Server Error." }.ToString());
                      }
                  });
              });

            app.UseHttpsRedirection();

            app.UseRouting();
     app.UseCors(_policyName);
           // app.UseAuthentication();

           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
