using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiVideojuegos.Entidades;
using Microsoft.AspNetCore.Http;
namespace WebApiVideojuegos.Controllers
{
    [ApiController]
    [Route("api/EspecVideojuego")]
    public class EspecVideojuegosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EspecVideojuegosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<EspecVideojuego>>> GetAll()
        {
            return await dbContext.EspecVideojuegos.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<EspecVideojuego>> GetById(int id)
        {
            return await dbContext.EspecVideojuegos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EspecVideojuego especVideojuego)
        {
            var existeEspecVidejuego = await dbContext.Videojuegos.AnyAsync(x => x.Id == especVideojuego.VideojuegoId);
            if (!existeEspecVidejuego)
            {
                return BadRequest($"No existe el juego con esta id: {especVideojuego.VideojuegoId}");
            }
            dbContext.Add(especVideojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(EspecVideojuego especVideojuego, int id)
        {
            var exist = await dbContext.EspecVideojuegos.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("no existe ");
            }
            if (especVideojuego.Id != id)
            {
                return BadRequest("El id de la especificacion no coincide con el url.");
            }
            dbContext.Update(especVideojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.EspecVideojuegos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

           

            dbContext.Remove(new EspecVideojuego
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }




    }
}
