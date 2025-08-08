using AutoMapper;
using Database.Models;
using DTO.Agenda;
using DTO.Auth;
using DTO.Evento;
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
    }
}