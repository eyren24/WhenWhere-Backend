using AutoMapper;
using Database.Models;
using DTO.Agenda;
using DTO.Auth;

namespace Repository.profiler;

public class Profilers : Profile
{
    public Profilers()
    {
        #region Agenda

        CreateMap<ReqAgendaDTO, Agenda>().ReverseMap();
        CreateMap<ResAgendaDTO, Agenda>().ReverseMap();

        #endregion

        #region Auth

        CreateMap<ReqRegisterUser, Utente>().ReverseMap();
        CreateMap<ReqLoginUser, Utente>().ReverseMap();

        #endregion
    }
}