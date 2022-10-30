using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiVideojuegos.Filtros
{
    public class FiltroDeAccion
    {
        private readonly ILogger<FiltroDeAccion> log;

        public FiltroDeAccion(ILogger<FiltroDeAccion> log)
        {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("Antes de ejecutarse el filtro.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("Después de ejecutarse el filtro.");
        }
    }
}