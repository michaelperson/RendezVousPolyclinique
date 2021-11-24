using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PolyDB.DAL;
using PolyDB.DAL.Entities;
using PolyDB.DAL.Repositories;
using PolyDB.DAL.Repositories.Interfaces;

namespace ApiMiddleware
{
    public static class ServiceExtension
    {
        #region Swagger
        public static void AddCustomSwagger(this IServiceCollection services, string Title, string Version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RendezVousPolyclinique", Version = "v1" });
            });
        }
        #endregion

        #region Cors
        public static void AddCustomCorsPolicy(this IServiceCollection services, string policyName, List<string> Origins=null )
        {
            services.AddCors(
               options =>
               {
                   if (Origins != null)
                   {
                       options.AddPolicy(policyName, poli =>
                                          poli.WithOrigins(Origins.ToArray())
                                              .AllowAnyMethod()
                                              .AllowAnyHeader()
                                          );
                   }
                   else
                   {
                       options.AddPolicy(policyName, poli =>
                                          poli.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader()
                                          );
                   }
               }
               );
        }
        #endregion

         
        #region DI-Repositories
        public static void AddToDI(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddScoped<IDBConnect, DBConnect>(m => new DBConnect(Configuration.GetConnectionString("DevPolyDbConnectionString")));
            services.AddScoped<IRepository<PatientEntity, int>, PatientRepository>();
        }
        #endregion

        #region Security
        public static void AddIdenityServerSecurityBearer(this IServiceCollection services, string ApiName, string ServerAdress)
        {
            services.AddAuthentication("Bearer")
                       .AddIdentityServerAuthentication("Bearer", options =>
                       {
                           options.ApiName = ApiName; //Nom de l'api configurée dans Identity
                           options.Authority = ServerAdress; //Adresse identity server
                       });
        }
        #endregion
    }
}
