using Microsoft.Extensions.DependencyInjection;
using Repository.interfaces;
using Repository.profiler;
using Repository.Services.agenda;
using Repository.services.auth;
using Repository.services.evento;
using Repository.services.likes;
using Repository.services.note;
using Repository.services.social;
using Repository.Services.tag;
using Repository.services.utente;

namespace Repository;

public static class StartUpConfiguratorRepo
{
    public static void AddRepository(this IServiceCollection services)
    {
        #region Agenda

        services.AddScoped<IAgendaRepo, AgendaRepo>();

        #endregion

        #region auth

        services.AddScoped<IAuthRepository, AuthRepository>();

        #endregion

        #region Nota

        services.AddScoped<INotaRepo, NotaRepo>();

        #endregion

        #region Evento

        services.AddScoped<IEventoRepo, EventoRepo>();

        #endregion

        #region Tag

        services.AddScoped<ITagRepo, TagRepo>();

        #endregion

        #region Utente

        services.AddScoped<IUtenteRepo, UtenteRepo>();

        #endregion

        #region Likes

        services.AddScoped<ILikesRepo, LikesRepo>();

        #endregion

        #region Social

        services.AddScoped<ISocialRepo, SocialRepo>();

        #endregion
    }

    public static void AddProfiler(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Profilers));
    }
}