using AutoMapper;
using Database.Models;
using DTO.Agenda;
using DTO.Auth;
using DTO.Evento;
using DTO.Likes;
using DTO.Tag;
using DTO.Utente;

namespace Repository.profiler;

public class Profilers : Profile
{
    public Profilers()
    {
        #region Agenda

        CreateMap<ReqAgendaDTO, Agenda>().ReverseMap();
        CreateMap<ResAgendaDTO, Agenda>().ReverseMap();

        #endregion

        #region Evento

        CreateMap<ReqEventoDTO, Evento>().ReverseMap();
        CreateMap<ResEventoDTO, Evento>().ReverseMap();

        #endregion

        #region Auth

        CreateMap<ReqRegisterUser, Utente>().ReverseMap();
        CreateMap<ResUtenteDTO, Utente>().ReverseMap();
        CreateMap<ReqLoginUser, Utente>().ReverseMap();

        #endregion

        #region Tags

        CreateMap<ResTagDTO, Tag>().ReverseMap();

        #endregion

        #region Likes

        CreateMap<ReqLikesDTO, Likes>().ReverseMap();
        CreateMap<ResLikesDTO, Likes>().ReverseMap();

        #endregion
    }
}