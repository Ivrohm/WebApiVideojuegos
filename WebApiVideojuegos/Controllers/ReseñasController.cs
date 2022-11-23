using WebApiVideojuegos.DTOs;
using WebApiVideojuegos.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace WebApiVideojuegos.Controllers
{
    [ApiController]
    [Route("api/videojuegos/{videojuegoId:int}/reseñas")]
    public class ReseñasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        public ReseñasController(ApplicationDbContext context, IMapper mapper,
                                                        UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult<List<ReseñaVideojuegoDTO>>> Get(int videojuegoId)
        {
            var existeVideojuego = await context.TiendaVideojuegos.AnyAsync(videojuegoDB => videojuegoDB.Id == videojuegoId);
            if (!existeVideojuego)
            {
                return NotFound();
            }

            var servicio = await context.Reseñas.Where(servicioDB => servicioDB.tiendaVideojuegoId == videojuegoId).ToListAsync();

            return mapper.Map<List<ReseñaVideojuegoDTO>>(servicio);
        }

        [HttpGet("{id:int}", Name = "obtenerReseña")]
        public async Task<ActionResult<ReseñaVideojuegoDTO>> GetById(int id)
        {
            var servicio = await context.Reseñas.FirstOrDefaultAsync(servicioDB => servicioDB.Id == id);

            if (servicio == null)
            {
                return NotFound();
            }

            return mapper.Map<ReseñaVideojuegoDTO>(servicio);
        }

        [HttpPost]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int EspecId, ReseñaVideojuegoCreacionDTO reseñaCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;
            var existeVideojuego = await context.TiendaVideojuegos.AnyAsync(videoDB => videoDB.Id == EspecId);

            if (!existeVideojuego)
            {
                return NotFound();
            }

            var reseña = mapper.Map<Reseña>(reseñaCreacionDTO);
            reseña.tiendaVideojuegoId = EspecId;
            reseña.UsuarioId = usuarioId;
            context.Add(reseña);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}
