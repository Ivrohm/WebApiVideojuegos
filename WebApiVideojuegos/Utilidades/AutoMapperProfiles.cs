using WebApiVideojuegos.DTOs;
using WebApiVideojuegos.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;

namespace ApiVideoclub.Utilidades
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<VideojuegoDTO, Videojuego>();
            CreateMap<Videojuego, GetVideojuegoDTO>();
            CreateMap<Videojuego, VideojuegoDTOConTiendaVideojuego>()
                .ForMember(VideojuegoDTO => VideojuegoDTO.TiendaVideojuego, opciones => opciones.MapFrom(MapVideojuegoDTOTiendaVideojuego));

            CreateMap<TiendaVideojuegoCreacionDTO, TiendaVideojuego>()
                .ForMember(videojuego => videojuego.VideojuegoTiendaVideojuego, opciones => opciones.MapFrom(MapReseñaConsola));

            CreateMap<TiendaVideojuego, VideojuegoDTO>();
            CreateMap<TiendaVideojuego, TiendaVideojuegoDTOConVideojuegos>()
                .ForMember(videojuegoDTO => videojuegoDTO.Videojuego, opciones => opciones.MapFrom(MapEspcVideojuego));

            CreateMap<ReseñaVideojuegoCreacionDTO, Reseña>();
            CreateMap<Reseña, ReseñaVideojuegoDTO>();
        }

        private List<TiendaVideojuegoDTO> MapVideojuegoDTOTiendaVideojuego(Videojuego videojuego, GetVideojuegoDTO getVideojuegoDTO)
        {
            var resultado = new List<TiendaVideojuegoDTO>();

            if (videojuego.videojuegoTiendaVideojuegos == null) { return resultado; }

            foreach (var videojuegoEspc in videojuego.videojuegoTiendaVideojuegos)
            {
                resultado.Add(new TiendaVideojuegoDTO()
                {
                    Id = videojuegoEspc.tiendaVideojuegoId,
                    Name = videojuegoEspc.tiendaVideojuego.Name
                });
            }

            return resultado;
        }

        private List<GetVideojuegoDTO> MapEspcVideojuego(TiendaVideojuego especVideojuego, TiendaVideojuegoDTO especDTO)
        {
            var resultado = new List<GetVideojuegoDTO>();

            if (especVideojuego.VideojuegoTiendaVideojuego == null) { return resultado; }

            foreach (var game in especVideojuego.VideojuegoTiendaVideojuego)
            {
                resultado.Add(new GetVideojuegoDTO()
                {
                    Id = game.VideojuegoId,
                    Titulo = game.Videojuego.Titulo,
                    añodeLanzamiento = game.Videojuego.AñodeLanzamiento,
                    consola = game.Videojuego.consola
                });
            }

            return resultado;
        }

        private List<VideojuegoTiendaVideojuego> MapReseñaConsola(TiendaVideojuegoCreacionDTO especVideojuegoCreacionDTO, TiendaVideojuego especVideojuego)
        {
            var resultado = new List<VideojuegoTiendaVideojuego>();

            if (especVideojuegoCreacionDTO.VideojuegosId == null) { return resultado; }

            foreach (var gameid in especVideojuegoCreacionDTO.VideojuegosId)
            {
                resultado.Add(new VideojuegoTiendaVideojuego() { VideojuegoId = gameid });
            }

            return resultado;
        }

    }
}

