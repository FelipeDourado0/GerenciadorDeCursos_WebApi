using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using GerenciadorCursos.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using GerenciadorCursos.Repository;
using GerenciadorCursos.Settings;

namespace GerenciadorCursos
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

        var configuracoesSection = Configuration.GetSection("ConfiguracoesJWT");
        var configuracoesJWT = configuracoesSection.Get<ConfiguracoesJWT>();

        services.Configure<ConfiguracoesJWT>(configuracoesSection);

        //Injeção de dependencia repository  
        services.AddScoped<ICursosRepository, CursosRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();

        //Transforma os inteiros dos arquivos ENUM em String.
        services.AddControllers().AddJsonOptions(x => 
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddDbContext<ApplicationContext>(options => {
            options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
        });
        
        services.AddAuthentication(opcoes => {
            opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opcoes => {
                //Validando usuario ADM
                opcoes.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuracoesJWT.Segredo)),
                    ValidAudience = "https://localhost:5001",
                    ValidIssuer = "User",
                };
            });

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GerenciadorCursos", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            }); 
        
        });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GerenciadorCursos v1"));
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthentication();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
