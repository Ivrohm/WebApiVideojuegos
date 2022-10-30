using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

using WebApiVideojuegos.Services;
using WebApiVideojuegos.Controllers;

using WebApiVideojuegos.Middlewares;

using WebApiVideojuegos.Filtros;


namespace WebApiVideojuegos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<ApplicationDbContext>(options =>//se agrego para la conexion de base de datos
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //////////////////Minuto 47 video 03 09
            services.AddTransient<IService, ServiceA>();
            services.AddTransient<ServiceTransient>();
            services.AddScoped<ServiceScoped>();
            services.AddSingleton<ServiceSingleton>();

            ///

            //parte del Ihosted
            services.AddTransient<FiltroDeAccion>();
               
            services.AddHostedService<EscribirArchivo>();
            services.AddResponseCaching();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiVideojuego", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            /*    app.Use(async (context, siguiente) =>
               {
                 using (var ms = new MemoryStream())
                  {
                       var bodyOriginal = context.Response.Body;
                       context.Response.Body = ms;

                         await siguiente.Invoke();

                        ms.Seek(0, SeekOrigin.Begin);
                        string response = new StreamReader(ms).ReadToEnd();
                        ms.Seek(0, SeekOrigin.Begin);

                        await ms.CopyToAsync(bodyOriginal);
                        context.Response.Body = bodyOriginal;

                        logger.LogInformation(response);
                     }
               });*/
            app.UseResponseHttpMiddleware();//no se expone con la creacion de la clase middlewarres las otras clases 


            app.Map("/maping", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Interceptando las peticiones necesarias");//no va permitir entrar a la informacion de la base
                });
            });


            // Configura el http request pipeline
            //importa el oden en que esta

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
