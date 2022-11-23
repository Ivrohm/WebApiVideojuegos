using WebApiVideojuegos.DTOs;
using WebApiVideojuegos.Entidades;
using WebApiVideojuegos.Filtros;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApiVideojuegos.Controllers
{
    [ApiController]
    [Route("videojuegos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class VideojuegosController : ControllerBase
    {
        
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;


        public VideojuegosController(ApplicationDbContext dbContext, IMapper mapper,IConfiguration configuration )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        //[AllowAnonymous]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<GetVideojuegoDTO>>> Get()
        {
            var videojuego = await dbContext.Videojuegos.ToListAsync();
            return mapper.Map<List<GetVideojuegoDTO>>(videojuego);
        }

        [HttpGet("{id:int}", Name = "obetner juegos")]
        public async Task<ActionResult<VideojuegoDTOConTiendaVideojuego>> Get(int id)
        {
            var videojuego = await dbContext.Videojuegos
                .Include(videojuegDB => videojuegDB.videojuegoTiendaVideojuegos)
                .ThenInclude(ConsolaDB=>ConsolaDB.tiendaVideojuego)
                .FirstOrDefaultAsync(videojuegoBD => videojuegoBD.Id == id);

            if(videojuego == null)
            {
                return NotFound();
            }

            return mapper.Map<VideojuegoDTOConTiendaVideojuego>(videojuego);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetVideojuegoDTO>>> Get([FromRoute]string nombre)
        {
            var game = await dbContext.Videojuegos.Where(peliculaBD => peliculaBD.Titulo.Contains(nombre)).ToListAsync();
           
            return mapper.Map<List<GetVideojuegoDTO>>(game);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VideojuegoDTO videojuegoDTO)
        {
            var existePeliculaMismoNombre = await dbContext.Videojuegos.AnyAsync(x => x.Titulo ==videojuegoDTO.Titulo);

            if (existePeliculaMismoNombre)
            {
                return BadRequest("Ya existe un videojuego con el mismo nombre");
            }

            var game = mapper.Map<Videojuego>(videojuegoDTO);

            dbContext.Add(game);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(VideojuegoDTO videojuego, int id)
        {
            var exist = await dbContext.Videojuegos.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound();
            }
            var game = mapper.Map<Videojuego>(videojuego);
            game.Id = id;

            dbContext.Update(game);
            await dbContext.SaveChangesAsync();
            return NoContent();
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
