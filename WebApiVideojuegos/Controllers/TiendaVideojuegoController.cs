using WebApiVideojuegos.DTOs;
using WebApiVideojuegos.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiVideojuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaVideojuegoController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public TiendaVideojuegoController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

       

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TiendaVideojuegoDTOConVideojuegos>> GetBy(int id)
        {
            var especVideojuego = await dbContext.TiendaVideojuegos
                .Include(videojuegoDB => videojuegoDB.VideojuegoTiendaVideojuego)
                .ThenInclude(consolaDB => consolaDB.Videojuego)
                .Include(reseñaDB => reseñaDB.Reseñas)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (especVideojuego == null)
            {
                return NotFound();
            }

           especVideojuego.VideojuegoTiendaVideojuego = especVideojuego.VideojuegoTiendaVideojuego.OrderBy(x => x.orden).ToList();

             return mapper.Map<TiendaVideojuegoDTOConVideojuegos>(especVideojuego);


        

          
        }

        [HttpPost]
        public async Task<ActionResult> Post(TiendaVideojuegoCreacionDTO especVideojuegoCreacionDTO)
        {

            if (especVideojuegoCreacionDTO.VideojuegosId == null)
            {
                return BadRequest("No se puede crear tienda videojuegos");
            }

            var videojuegosIds = await dbContext.Videojuegos
                .Where(videojuegoBD => especVideojuegoCreacionDTO.VideojuegosId.Contains(videojuegoBD.Id)).Select(x => x.Id).ToListAsync();

            if (especVideojuegoCreacionDTO.VideojuegosId.Count != videojuegosIds.Count)
            {
                return BadRequest("No existe una de los juegos");
            }

            var games = mapper.Map<TiendaVideojuego>(especVideojuegoCreacionDTO);

            if (games.VideojuegoTiendaVideojuego != null)
            {
                for (int i = 0; i < games.VideojuegoTiendaVideojuego.Count; i++)
                {
                    games.VideojuegoTiendaVideojuego[i].tiendaVideojuegoId = i;
                }
            }

            dbContext.Add(games);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
