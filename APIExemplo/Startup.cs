using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace APIExemplo
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
            #region Serviço de configuração da aplicação.
            var appSettings = new AppSettings();
            var cfcoAppSettings = new ConfigureFromConfigurationOptions<AppSettings>(Configuration.GetSection("AppSettings"));
            cfcoAppSettings.Configure(appSettings);
            services.AddSingleton<AppSettings>(appSettings);
            #endregion

            services.AddTransient<Service.ProdutoService>();

            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            services.AddSingleton<MongoClient>(client);
            services.AddSingleton<IMongoDatabase>(database);


            #region Configurando como o token será validado

            var signingConfiguration = new SigningConfiguration(appSettings.Key);
            services.AddSingleton(signingConfiguration);


            services.AddAuthentication(authOpt =>
            {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                var validation = bearer.TokenValidationParameters;
                validation.ValidAudience = appSettings.Audencie;
                validation.ValidIssuer = appSettings.Issuer;
                validation.ValidateLifetime = true;
                validation.ValidateIssuerSigningKey = true;
                validation.IssuerSigningKey = signingConfiguration.Key;
            });

            //policial
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ValidaUsuario", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion
            

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
