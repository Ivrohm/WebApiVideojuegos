using Microsoft.AspNetCore.Mvc;
using WebApiVideojuegos.Controllers;
using WebApiVideojuegos.Entidades;

namespace WebApiVideojuegos.Services
{
    public class EscribirArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Archivo1.txt";
        private Timer timer;

        public EscribirArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando la cargamos la aplicacion una vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15)); //cada 15 segundos
            Escribir("<--Proceso Iniciado-->");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando detenemos la aplicacion aunque puede no ejecutarse por algun error.
            timer.Dispose();
            Escribir("<--Proceso Finalizado-->");
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            Escribir("Proceso de ejecucion: " + DateTime.Now.ToString("dd/MM/yy hh:mm:ss"));
        }

        public void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(msg);
            }
        }
    }
}