using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using WebApiVideojuegos.Entidades;

namespace WebApiVideojuegos.Controllers
{
    [ApiController]
    [Route("videojuegos")]

    public class VideojuegosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public VideojuegosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [HttpGet("listado")]//api/videojuegos/listado
        [HttpGet("/listado")]// listado
        public async Task<ActionResult<List<Videojuego>>> Get()
        {
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

        [HttpGet("{nombre}")]
        public async Task<ActionResult<EspecVideojuego>> Get([FromRoute] string nombre)
        {
            var juego = await dbContext.EspecVideojuegos.FirstOrDefaultAsync(x => x.name.Contains(nombre));
            if (juego == null)
            {
                return NotFound();
            }
            return juego;
        }



        [HttpPost]

        public async Task<ActionResult> Post (Videojuego videojuego)
        {
            dbContext.Add(videojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

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
