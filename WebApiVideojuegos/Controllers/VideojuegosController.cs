using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiVideojuegos.Entidades;
using WebApiVideojuegos.Services;
using WebApiVideojuegos.Filtros;


namespace WebApiVideojuegos.Controllers
{
    [ApiController]
    [Route("videojuegos")]
    [Authorize]

    public class VideojuegosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;//filtros 
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<VideojuegosController> logger;

        public VideojuegosController(ApplicationDbContext dbContext, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<VideojuegosController> logger
          )
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]

        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
   
        public ActionResult ObtenerGuid()
        {
      
            return Ok(new
            {

                VideojuegosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                VideojuegosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                VideojuegosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet]
        [HttpGet("listado")]//api/videojuegos/listado
        [HttpGet("/listado")]// listado
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<List<Videojuego>>> Get()

        {
            //existen varios niveles de loog puede mostrar cosas criticas
            //throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de juegos");
            logger.LogWarning("Mensaje de prueba warning");
            service.ejecutarJob();
            return await dbContext.Videojuegos.Include(x => x.EspecVideojuegos).ToListAsync();
        }

        [HttpGet("primero")]//api/videojuegos/primero
        public async Task<ActionResult<EspecVideojuego>> Primerjuego([FromHeader] int Id, [FromQuery] string name,
               [FromQuery] int VideojuegoId)
        {
            return await dbContext.EspecVideojuegos.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]//api/videojuegos/primero2
        public ActionResult<EspecVideojuego> PrimerPeliculaD()
        {
            return new EspecVideojuego() { name = "DOS" };
        }

        [HttpGet("{id:int}/{param=Metroid}")]
        public async Task<ActionResult<EspecVideojuego>> Get(int id, string param)
        {
            var juego = await dbContext.EspecVideojuegos.FirstOrDefaultAsync(x => x.Id == id);
            if (juego == null)
            {

                return NotFound();
            }
            return juego;
        }
        [HttpGet("obtenerJuego/{nombre}")]
        public async Task<ActionResult<EspecVideojuego>> Get([FromRoute] string nombre)
        {
            var juego = await dbContext.EspecVideojuegos.FirstOrDefaultAsync(x => x.name.Contains(nombre));
            if (juego == null)
            {
                logger.LogError("No se encuentra el juego");
                return NotFound();
            }
            return juego;
        }

     
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Videojuego videojuego)
        {
            var existeAlumnoMismoNombre = await dbContext.Videojuegos.AnyAsync(x => x.name == videojuego.name);

            if (existeAlumnoMismoNombre)
            {
                return BadRequest("Ya existe un titulo con este nombre");
            }

            dbContext.Add(videojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        /*
        [HttpPost]

        public async Task<ActionResult> Post (Videojuego videojuego)
        {
            dbContext.Add(videojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        */
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Videojuego videojuego, int id)
        {
            if(videojuego.Id != id)
            {
                return BadRequest("El juego con la id no esta disponible");
            }
            dbContext.Update(videojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Videojuegos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Videojuego()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
